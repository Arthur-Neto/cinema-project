import { HttpClient, HttpEvent, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env';

import { Observable } from 'rxjs';

import { IDayAndMonth } from '../../../shared/components/carousel-daypicker/carousel-daypicker.component';
import { AudioType, IMovieCoverUploadCommand, IMovieCreateCommand, IMovieDashboardModel, IMovieModel, ScreenType } from './movies.model';

@Injectable()
export class MoviesODataService {
    private apiUrl: string;

    constructor(
        private http: HttpClient
    ) {
        this.apiUrl = `${ environment.apiUrl }odata/movies`;
    }

    public getAll(): Observable<IMovieModel[]> {
        return this.http.get<IMovieModel[]>(`${ this.apiUrl }`);
    }
}

@Injectable()
export class MoviesDashboardODataService {
    private apiUrl: string;

    constructor(
        private http: HttpClient
    ) {
        this.apiUrl = `${ environment.apiUrl }odata/movies-dashboard`;
    }

    public getDashboardMovies(
        dayAndMonth: IDayAndMonth,
        screenType: ScreenType = null,
        audioType: AudioType = null
    ): Observable<IMovieDashboardModel[]> {
        let uri = `${ this.apiUrl }?Date=${ new Date().getFullYear() }-${ dayAndMonth.month }-${ dayAndMonth.day }`;
        if (screenType) {
            uri = uri + `&$filter=ScreenType eq '${ screenType }'`;
        } else if (audioType) {
            uri = uri + `&$filter=AudioType eq '${ audioType }'`;
        }

        return this.http.get<IMovieDashboardModel[]>(uri);
    }
}

@Injectable()
export class MoviesApiService {
    private apiUrl: string;

    constructor(
        private http: HttpClient
    ) {
        this.apiUrl = `${ environment.apiUrl }api/movies`;
    }

    public delete(id: number): Observable<boolean> {
        return this.http.delete<boolean>(`${ this.apiUrl }/${ id }`);
    }

    public getById(id: number): Observable<IMovieModel> {
        return this.http.get<IMovieModel>(`${ this.apiUrl }/${ id }`);
    }

    public create(command: IMovieCreateCommand): Observable<number> {
        return this.http.post<number>(`${ this.apiUrl }`, command);
    }

    public updateCover(command: IMovieCoverUploadCommand): Observable<HttpEvent<any>> {
        const formData: FormData = new FormData();
        formData.append('id', command.movieId.toString());
        formData.append('image', command.imgFile);

        const request = new HttpRequest('POST', `${ this.apiUrl }/update-cover`, formData, {
            reportProgress: true,
            responseType: 'json'
        });

        request.headers.append('Content-type', 'application/octet-stream');

        return this.http.request(request);
    }
}
