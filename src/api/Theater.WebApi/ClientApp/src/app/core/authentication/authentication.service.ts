import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env';

import { AuthenticateCommand, AuthenticatedUser } from './authentication-models';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    public user: Observable<AuthenticatedUser>;

    public get userValue(): AuthenticatedUser {
        return this.userSubject.value;
    }

    private userSubject: BehaviorSubject<AuthenticatedUser>;

    constructor(
        private http: HttpClient
    ) {
        this.userSubject = new BehaviorSubject<AuthenticatedUser>(JSON.parse(localStorage.getItem('user')));
        this.user = this.userSubject.asObservable();
    }

    public login(command: AuthenticateCommand): Observable<AuthenticatedUser> {
        return this.http.post<AuthenticatedUser>(`${ environment.apiUrl }api/users/login`, command)
            .pipe(map((user: AuthenticatedUser) => {
                localStorage.setItem('user', JSON.stringify(user));
                this.userSubject.next(user);

                return user;
            }));
    }

    public logout(): void {
        localStorage.removeItem('user');
        this.userSubject.next(null);
    }
}
