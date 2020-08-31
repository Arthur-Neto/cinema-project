import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env';

import { Observable } from 'rxjs';

import { ISessionModel } from './sessions.model';

@Injectable()
export class SessionsODataService {
    private apiUrl: string;

    constructor(
        private http: HttpClient
    ) {
        this.apiUrl = `${ environment.apiUrl }odata/sessions`;
    }

    public getAll(): Observable<ISessionModel[]> {
        return this.http.get<ISessionModel[]>(`${ this.apiUrl }`);
    }
}

@Injectable()
export class SessionsApiService {
    private apiUrl: string;

    constructor(
        private http: HttpClient
    ) {
        this.apiUrl = `${ environment.apiUrl }api/sessions`;
    }

    public delete(id: number): Observable<boolean> {
        return this.http.delete<boolean>(`${ this.apiUrl }\\${ id }`);
    }

    public getById(id: number): Observable<ISessionModel> {
        return this.http.get<ISessionModel>(`${ this.apiUrl }\\${ id }`);
    }
}
