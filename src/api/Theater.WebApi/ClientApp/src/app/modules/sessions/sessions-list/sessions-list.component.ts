import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { IActionModel } from '@shared/components/grid/shared/grid.model';

import { finalize, take } from 'rxjs/operators';

import { ISessionModel } from '../shared/sessions.model';
import { SessionsApiService, SessionsODataService } from '../shared/sessions.service';

@Component({
    templateUrl: './sessions-list.component.html',
    styleUrls: ['./sessions-list.component.scss']
})
export class SessionsListComponent implements OnInit {
    public dataSource: MatTableDataSource<ISessionModel>;
    public selectedSession: ISessionModel;
    public isLoading = true;

    public headerNames: string[] = ['Date', 'Movie', 'Room'];
    public displayedColumns: string[] = ['id', 'date', 'movieTitle', 'roomName'];
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
                if (!this.selectedSession) {
                    this.snackBar.open('Select a session');

                    return;
                }

                this.router.navigate(
                    ['../edit', this.selectedSession.id],
                    { relativeTo: this.route, state: { session: this.selectedSession } }
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
        private sessionsODataService: SessionsODataService,
        private sessionsApiService: SessionsApiService,
        private snackBar: MatSnackBar,
        private router: Router,
        private route: ActivatedRoute,
    ) { }

    public ngOnInit(): void {
        this.sessionsODataService
            .getAll()
            .pipe(
                take(1),
                finalize(() => this.isLoading = false))
            .subscribe((Sessions: ISessionModel[]) => {
                this.dataSource = new MatTableDataSource(Sessions);
            });
    }

    public onSelectionChange(session: ISessionModel): void {
        this.selectedSession = session;
    }

    private onDelete(): void {
        if (!this.selectedSession) {
            this.snackBar.open('Select a session');

            return;
        }

        this.isLoading = true;
        this.sessionsApiService
            .delete(this.selectedSession.id)
            .pipe(take(1))
            .subscribe({
                next: () => {
                    this.snackBar.open('Delete success');

                    this.sessionsODataService
                        .getAll()
                        .pipe(
                            take(1),
                            finalize(() => this.isLoading = false))
                        .subscribe((Sessions: ISessionModel[]) => {
                            this.dataSource.data = Sessions;
                        });
                }
            });
    }
}
