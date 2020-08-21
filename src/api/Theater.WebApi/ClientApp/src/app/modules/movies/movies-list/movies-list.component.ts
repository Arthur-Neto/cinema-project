import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { IActionModel } from '@shared/components/grid/shared/grid.model';

import { finalize, take } from 'rxjs/operators';

import { IMovieModel } from '../shared/movies.model';
import { MoviesApiService, MoviesODataService } from '../shared/movies.service';

@Component({
    templateUrl: './movies-list.component.html',
    styleUrls: ['./movies-list.component.scss']
})
export class MoviesListComponent implements OnInit {
    public dataSource: MatTableDataSource<IMovieModel>;
    public selectedMovie: IMovieModel;
    public isLoading = true;

    public headerNames: string[] = ['Title', 'Description', 'Duration', 'Screen type', 'Audio Type'];
    public displayedColumns: string[] = ['id', 'title', 'description', 'duration', 'screenName', 'audioName'];
    public actions: IActionModel[] = [
        {
            icon: 'add',
            name: 'Add',
            function: () => {
                this.router.navigate(['../create'], { relativeTo: this.route });
            }
        },
        {
            icon: 'edit',
            name: 'Edit',
            function: () => {
                if (!this.selectedMovie) {
                    this.snackBar.open('Select a movie');

                    return;
                }

                this.router.navigate(
                    ['../edit', this.selectedMovie.id],
                    { relativeTo: this.route, state: { movie: this.selectedMovie } }
                );
            }
        },
        {
            icon: 'delete',
            name: 'Delete',
            function: () => {
                this.onDelete();
            }
        }
    ];

    constructor(
        private moviesODataService: MoviesODataService,
        private moviesApiService: MoviesApiService,
        private snackBar: MatSnackBar,
        private router: Router,
        private route: ActivatedRoute,
    ) { }

    public ngOnInit(): void {
        this.moviesODataService
            .getAll()
            .pipe(
                take(1),
                finalize(() => this.isLoading = false))
            .subscribe((movies: IMovieModel[]) => {
                this.dataSource = new MatTableDataSource(movies);
            });
    }

    public onSelectionChange(movie: IMovieModel): void {
        this.selectedMovie = movie;
    }

    private onDelete(): void {
        if (!this.selectedMovie) {
            this.snackBar.open('Select a movie');

            return;
        }

        this.isLoading = true;
        this.moviesApiService
            .delete(this.selectedMovie.id)
            .pipe(take(1))
            .subscribe({
                next: () => {
                    this.snackBar.open('Delete success');

                    this.moviesODataService
                        .getAll()
                        .pipe(
                            take(1),
                            finalize(() => this.isLoading = false))
                        .subscribe((movies: IMovieModel[]) => {
                            this.dataSource.data = movies;
                        });
                }
            });
    }
}
