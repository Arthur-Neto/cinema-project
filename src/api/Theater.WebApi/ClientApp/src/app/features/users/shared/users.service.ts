import { Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../../../../environments/environment';
import { UserUpdateCommand } from './users.model';

@Injectable()
export class UsersService {
    private apiUrl: string;

    constructor(
        private http: HttpClient
    ) {
        this.apiUrl = `${ environment.apiUrl }api/users`;
    }

    public update(command: UserUpdateCommand): Observable<boolean> {
        return this.http.put<boolean>(`${ this.apiUrl }`, command).pipe();
    }
}