import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticatedUser } from '@app/authentication/authentication-models';
import { AuthenticationService } from '@app/authentication/authentication.service';

@Component({
    selector: 'app-nav-bar-login',
    templateUrl: './nav-bar-login.component.html',
    styleUrls: ['./nav-bar-login.component.scss']
})
export class NavBarLoginComponent implements OnInit, OnDestroy {
    public userLogged: AuthenticatedUser;

    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) { }

    public ngOnInit(): void {
        this.authenticationService
            .user
            .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe((user: AuthenticatedUser) => {
                this.userLogged = user;
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    public logout(): void {
        this.authenticationService.logout();
        this.router.navigate(['login']);
    }

    public editLogin(): void {
        this.router.navigate(['edit-login']);
    }
}
