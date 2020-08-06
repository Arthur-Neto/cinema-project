import { Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../../../../environments/environment';
import { UserUpdateCommand } from './users.model';

@Injectable()
export class UsersService {
    constructor(
        private http: HttpClient
    ) { }

    public update(command: UserUpdateCommand): Observable<boolean> {
        return this.http.put<boolean>(`${ environment.apiUrl }api/users`, command).pipe();
    }
}
