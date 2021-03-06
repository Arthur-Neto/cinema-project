import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AuthenticatedUser } from '@app/authentication/authentication-models';
import { AuthenticationService } from '@app/authentication/authentication.service';

import { take } from 'rxjs/operators';

import { UserUpdateCommand } from '../../users/shared/users.model';
import { UsersApiService } from '../../users/shared/users.service';

@Component({
    templateUrl: './login-edit.component.html',
    styleUrls: ['./login-edit.component.scss']
})
export class LoginEditComponent implements OnInit {
    public form: FormGroup;
    public userLogged: AuthenticatedUser;

    public get showUsernameRequiredError(): boolean {
        return this.form.controls['username'].hasError('required');
    }
    public get showUsernameDuplicatedError(): boolean {
        return this.form.controls['username'].hasError('duplicated');
    }
    public get showPasswordRequiredError(): boolean {
        return this.form.controls['password'].hasError('required');
    }
    public get showConfirmPasswordRequiredError(): boolean {
        return this.form.controls['confirmPassword'].hasError('required');
    }
    public get showPasswordDoesntMatchError(): boolean {
        return this.form.controls['confirmPassword'].hasError('pattern');
    }

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private authenticationService: AuthenticationService,
        private usersApiService: UsersApiService,
        private snackBar: MatSnackBar,
    ) { }

    public ngOnInit(): void {
        this.authenticationService
            .user
            .pipe(take(1))
            .subscribe((user: AuthenticatedUser) => {
                this.userLogged = user;

                this.form = this.fb.group({
                    username: [user.username, Validators.required],
                    password: [user.password, Validators.required],
                    confirmPassword: [user.password, Validators.required],
                });
            });
    }

    public onSubmit() {
        if (this.form.valid) {
            const command = <UserUpdateCommand>{
                id: this.userLogged.id,
                username: this.form.controls['username'].value,
                password: this.form.controls['password'].value
            };

            this.usersApiService
                .update(command)
                .pipe(take(1))
                .subscribe({
                    next: this.onSuccessCallback.bind(this),
                    error: this.onErrorCallback.bind(this)
                });
        }
    }

    private onSuccessCallback(): void {
        this.snackBar.open('Edit success');
        this.router.navigate(['dashboard']);
    }

    private onErrorCallback(error: string): void {
        if (error.match('Duplicating')) {
            this.form.controls['username'].setErrors({ duplicated: true });
        }
    }
}
