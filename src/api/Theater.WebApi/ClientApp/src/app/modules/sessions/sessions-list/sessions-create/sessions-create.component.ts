import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';

import * as moment from 'moment';
import { finalize, take } from 'rxjs/operators';

import { IMovieModel } from '../../../movies/shared/movies.model';
import { MoviesODataService } from '../../../movies/shared/movies.service';
import { IAvailableRoomsCommand, IRoomsModel } from '../../../rooms/shared/rooms.model';
import { RoomsApiService } from '../../../rooms/shared/rooms.service';
import { ISessionCreateCommand } from '../../shared/sessions.model';
import { SessionsApiService } from '../../shared/sessions.service';

@Component({
    templateUrl: './sessions-create.component.html',
    styleUrls: ['./sessions-create.component.scss']
})
export class SessionsCreateComponent implements OnInit {
    public form: FormGroup;
    public isLoading = true;
    public movies: IMovieModel[] = [];
    public rooms: IRoomsModel[];
    public endingTime = 'N/A';
    public movieSelected: IMovieModel;
    public roomSelected: IRoomsModel;

    public get showMovieRequiredError(): boolean {
        return this.form.controls['movie'].hasError('required');
    }
    public get showRoomRequiredError(): boolean {
        return this.form.controls['room'].hasError('required');
    }

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private snackBar: MatSnackBar,
        private sessionApiService: SessionsApiService,
        private roomsApiService: RoomsApiService,
        private movieODataService: MoviesODataService
    ) { }

    public ngOnInit(): void {
        this.form = this.fb.group({
            dateTime: [new Date(), [Validators.required]],
            movie: [null, [Validators.required]],
            room: [null, [Validators.required]],
        });

        this.movieODataService
            .getAll()
            .pipe(
                take(1),
                finalize(() => this.isLoading = false))
            .subscribe((movies: IMovieModel[]) => {
                this.movies = movies;
            });
    }

    public onSubmit(): void {
        if (this.form.valid) {
            this.isLoading = true;

            const command = <ISessionCreateCommand>{
                date: this.form.controls['dateTime'].value,
                movieId: this.movieSelected.id,
                roomId: this.roomSelected.id,
            };

            this.sessionApiService
                .create(command)
                .pipe(take(1))
                .subscribe({
                    next: this.onSuccessCallback.bind(this),
                });
        }
    }

    public onMovieChanges(): void {
        this.isLoading = true;

        this.roomSelected = undefined;

        this.updateEndingTime();

        const command = <IAvailableRoomsCommand>{
            date: this.form.controls['dateTime'].value,
            movieDuration: this.movieSelected.duration
        };

        this.roomsApiService
            .availableRooms(command)
            .pipe(
                take(1),
                finalize(() => this.isLoading = false))
            .subscribe((rooms: IRoomsModel[]) => {
                this.rooms = rooms;
            });
    }

    public onDateSelectedChanges(): void {
        this.movieSelected = undefined;
        this.roomSelected = undefined;
    }

    public onCanceClick(): void {
        this.backRoute();
    }

    private onSuccessCallback(): void {
        this.isLoading = false;
        this.snackBar.open('Create success');
        this.backRoute();
    }

    private backRoute(): void {
        this.router.navigate(['../list'], { relativeTo: this.route });
    }

    private updateEndingTime(): void {
        const selectedDate = this.form.controls['dateTime'].value as Date;
        const splittedDuration = this.movieSelected.duration.split(':');
        const movieSelectedDurationOnMinutes = +splittedDuration[0] * 60 + +splittedDuration[1];

        this.endingTime = moment(selectedDate).add(movieSelectedDurationOnMinutes, 'm').format('HH:mm');
    }
}
