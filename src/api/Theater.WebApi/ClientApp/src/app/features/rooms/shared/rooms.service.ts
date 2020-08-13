import { Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../../../../environments/environment';
import { IRoomCreateCommand, IRoomsModel, IRoomUpdateCommand } from './rooms.model';

@Injectable()
export class RoomsODataService {
    private apiUrl: string;

    constructor(
        private http: HttpClient
    ) {
        this.apiUrl = `${ environment.apiUrl }odata/rooms`;
    }

    public getAll(): Observable<IRoomsModel[]> {
        return this.http.get<IRoomsModel[]>(`${ this.apiUrl }`);
    }
}

@Injectable()
export class RoomsApiService {
    private apiUrl: string;

    constructor(
        private http: HttpClient
    ) {
        this.apiUrl = `${ environment.apiUrl }api/rooms`;
    }

    public create(command: IRoomCreateCommand): Observable<number> {
        return this.http.post<number>(`${ this.apiUrl }`, command);
    }

    public update(command: IRoomUpdateCommand): Observable<boolean> {
        return this.http.put<boolean>(`${ this.apiUrl }`, command);
    }

    public delete(id: number): Observable<boolean> {
        return this.http.delete<boolean>(`${ this.apiUrl }\\${ id }`);
    }
}
