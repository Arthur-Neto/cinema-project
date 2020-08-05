import { error } from 'protractor';
import { take } from 'rxjs/operators';

import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthenticateCommand } from '../../core/authentication/authentication-models';
import { AuthenticationService } from '../../core/authentication/authentication.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    public loginForm: FormGroup;

    public get showUsernameRequiredError(): boolean {
        return this.loginForm.controls['username'].hasError('required');
    }
    public get showUsernameNotFoundError(): boolean {
        return this.loginForm.controls['username'].hasError('doesntExist');
    }
    public get showPasswordRequiredError(): boolean {
        return this.loginForm.controls['password'].hasError('required');
    }
    public get showWrongPasswordError(): boolean {
        return this.loginForm.controls['password'].hasError('wrongPassword');
    }
    public get showConfirmPasswordRequiredError(): boolean {
        return this.loginForm.controls['confirmPassword'].hasError('required');
    }
    public get showPasswordDoesntMatchError(): boolean {
        return this.loginForm.controls['confirmPassword'].hasError('pattern');
    }

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private authenticationService: AuthenticationService,
    ) { }

    public ngOnInit(): void {
        this.loginForm = this.fb.group({
            username: [null, Validators.required],
            password: ['', Validators.required],
            confirmPassword: ['', Validators.required],
        });
    }

    public onSubmit() {
        if (this.loginForm.valid) {
            const command = <AuthenticateCommand>{
                username: this.loginForm.controls['username'].value,
                password: this.loginForm.controls['password'].value
            };

            this.authenticationService
                .login(command)
                .pipe(take(1))
                .subscribe({
                    next: this.onSuccessCallback.bind(this),
                    error: this.onErrorCallback.bind(this)
                });
        }
    }

    private onSuccessCallback(result: any): void {
        this.router.navigate(['/']);
    }

    private onErrorCallback(errorResponse: HttpErrorResponse): void {
        if (errorResponse.error.error.match('UserNotFound')) {
            this.loginForm.controls['username'].setErrors({ doesntExist: true });
        } else if (errorResponse.error.error.match('IncorrectUserPassword')) {
            this.loginForm.controls['password'].setErrors({ wrongPassword: true });
        }
    }
}
