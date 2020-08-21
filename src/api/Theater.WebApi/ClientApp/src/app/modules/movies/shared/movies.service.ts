import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env';

import { Observable } from 'rxjs';

import { IMovieCreateCommand, IMovieModel, IMovieUpdateCommand } from './movies.model';

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
export class MoviesApiService {
    private apiUrl: string;

    constructor(
        private http: HttpClient
    ) {
        this.apiUrl = `${ environment.apiUrl }api/movies`;
    }

    public create(command: IMovieCreateCommand): Observable<number> {
        return this.http.post<number>(`${ this.apiUrl }`, command);
    }

    public update(command: IMovieUpdateCommand): Observable<boolean> {
        return this.http.put<boolean>(`${ this.apiUrl }`, command);
    }

    public delete(id: number): Observable<boolean> {
        return this.http.delete<boolean>(`${ this.apiUrl }\\${ id }`);
    }

    public getById(id: number): Observable<IMovieModel> {
        return this.http.get<IMovieModel>(`${ this.apiUrl }\\${ id }`);
    }
}
