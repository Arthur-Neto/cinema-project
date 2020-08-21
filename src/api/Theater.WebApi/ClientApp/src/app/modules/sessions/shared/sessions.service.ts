import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env';

import { Observable } from 'rxjs';

import { ISessionCreateCommand, ISessionModel, ISessionUpdateCommand } from './sessions.model';

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

    public create(command: ISessionCreateCommand): Observable<number> {
        return this.http.post<number>(`${ this.apiUrl }`, command);
    }

    public update(command: ISessionUpdateCommand): Observable<boolean> {
        return this.http.put<boolean>(`${ this.apiUrl }`, command);
    }

    public delete(id: number): Observable<boolean> {
        return this.http.delete<boolean>(`${ this.apiUrl }\\${ id }`);
    }

    public getById(id: number): Observable<ISessionModel> {
        return this.http.get<ISessionModel>(`${ this.apiUrl }\\${ id }`);
    }
}
