import { Component } from '@angular/core';

import { finalize, take } from 'rxjs/operators';

import { IDayAndMonth } from '../../shared/components/carousel-daypicker/carousel-daypicker.component';
import { AudioType, IMovieDashboardModel, ScreenType } from '../movies/shared/movies.model';
import { MoviesDashboardODataService } from '../movies/shared/movies.service';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
    public isLoading = true;
    public movies: IMovieDashboardModel[];
    public dayAndMonth: IDayAndMonth;
    public noMovies: boolean;
    public sessionsGroupByRoomName: any[];
    public audioType = AudioType;
    public screenType = ScreenType;

    constructor(
        private moviesDashboardODataService: MoviesDashboardODataService
    ) { }

    public onDayChanged(dayAndMonth: IDayAndMonth) {
        this.isLoading = true;
        this.dayAndMonth = dayAndMonth;

        this.moviesDashboardODataService
            .getDashboardMovies(dayAndMonth)
            .pipe(
                take(1),
                finalize(() => this.isLoading = false))
            .subscribe((movies: IMovieDashboardModel[]) => {
                this.movies = movies;

                this.noMovies = movies.length === 0;
            });
    }

    public onScreenOrAudioTypeChanged(screenType: ScreenType = null, audioType: AudioType = null) {
        this.isLoading = true;

        this.moviesDashboardODataService
            .getDashboardMovies(this.dayAndMonth, screenType, audioType)
            .pipe(
                take(1),
                finalize(() => this.isLoading = false))
            .subscribe((movies: IMovieDashboardModel[]) => {
                this.movies = movies;

                this.noMovies = movies.length === 0;
            });
    }
}
