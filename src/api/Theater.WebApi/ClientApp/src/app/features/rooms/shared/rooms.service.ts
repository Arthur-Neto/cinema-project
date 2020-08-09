import { Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../../../../environments/environment';
import { IRoomsModel } from './rooms.model';

@Injectable()
export class RoomsService {
    private apiUrl: string;

    constructor(
        private http: HttpClient
    ) {
        this.apiUrl = `${ environment.apiUrl }odata/rooms`;
    }

    public getAll(): Observable<IRoomsModel[]> {
        return this.http.get<IRoomsModel[]>(`${ this.apiUrl }`).pipe();
    }
}
