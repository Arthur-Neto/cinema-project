import { Component, OnInit } from '@angular/core';
import { AuthenticatedUser, Role } from '@app/authentication/authentication-models';
import { AuthenticationService } from '@app/authentication/authentication.service';

@Component({
    selector: 'app-side-bar',
    templateUrl: './side-bar.component.html',
    styleUrls: ['./side-bar.component.scss']
})
export class SideBarComponent implements OnInit {
    public isUserLoggedManager: boolean;

    constructor(
        private authenticationService: AuthenticationService
    ) { }

    public ngOnInit(): void {
        this.authenticationService
            .user
            .subscribe((user: AuthenticatedUser) => {
                this.isUserLoggedManager = user?.role === Role.Manager ? true : false;
            });
    }
}
