import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Subject } from 'rxjs';
import { finalize, take, takeUntil } from 'rxjs/operators';

import { AuthenticatedUser } from '../../core/authentication/authentication-models';
import { AuthenticationService } from '../../core/authentication/authentication.service';
import { IDayAndMonth } from '../../shared/components/carousel-daypicker/carousel-daypicker.component';
import { AudioType, IMovieDashboardModel, ScreenType } from '../movies/shared/movies.model';
import { MoviesDashboardODataService } from '../movies/shared/movies.service';
import { IRoomDashboardModel } from '../rooms/shared/rooms.model';
import { ISessionDashboardModel } from '../sessions/shared/sessions.model';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, OnDestroy {
    public isLoading = true;
    public movies: IMovieDashboardModel[] = [];
    public dayAndMonth: IDayAndMonth;
    public userLogged: AuthenticatedUser;

    public audioType = AudioType;
    public screenType = ScreenType;

    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(
        private router: Router,
        private moviesDashboardODataService: MoviesDashboardODataService,
        private authenticationService: AuthenticationService
    ) { }

    public ngOnInit(): void {
        this.authenticationService
            .user
            .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe((user: AuthenticatedUser) => {
                this.userLogged = user;
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    public onDayChanged(dayAndMonth: IDayAndMonth): void {
        this.isLoading = true;
        this.dayAndMonth = dayAndMonth;

        this.moviesDashboardODataService
            .getDashboardMovies(dayAndMonth)
            .pipe(
                take(1),
                finalize(() => this.isLoading = false))
            .subscribe((movies: IMovieDashboardModel[]) => {
                this.movies = movies;
            });
    }

    public onScreenOrAudioTypeChanged(screenType: ScreenType = null, audioType: AudioType = null): void {
        this.isLoading = true;

        this.moviesDashboardODataService
            .getDashboardMovies(this.dayAndMonth, screenType, audioType)
            .pipe(
                take(1),
                finalize(() => this.isLoading = false))
            .subscribe((movies: IMovieDashboardModel[]) => {
                this.movies = movies;
            });
    }

    public onSessionDateSelect(
        movie: IMovieDashboardModel,
        session: ISessionDashboardModel,
        room: IRoomDashboardModel,
        selectedDate: Date
    ): void {
        this.router.navigate(['/sessions/buy-tickets'], { state: { movie, session, room, selectedDate } });
    }
}
