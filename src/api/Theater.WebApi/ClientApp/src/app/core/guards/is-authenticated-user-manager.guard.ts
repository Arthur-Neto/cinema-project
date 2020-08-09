import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

import { Role } from '../authentication/authentication-models';
import { AuthenticationService } from '../authentication/authentication.service';

@Injectable({ providedIn: 'root' })
export class IsAuthenticatedUserManagerGuard implements CanActivate {
    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) { }

    public canActivate(): boolean {
        if (this.authenticationService.userValue.role === Role.Manager) {
            return true;
        }

        this.authenticationService.logout();

        this.router.navigate(['/login']);
        return false;
    }
}
