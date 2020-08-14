import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { RouterModule } from '@angular/router';

import { UsersApiService } from '../users/shared/users.service';
import { LoginEditComponent } from './login-edit/login-edit.component';
import { LoginComponent } from './login.component';
import { LoginRoutingModule } from './login.routing.module';

@NgModule({
    declarations: [
        LoginComponent,
        LoginEditComponent,
    ],
    imports: [
        CommonModule,
        LoginRoutingModule,
        RouterModule,

        ReactiveFormsModule,
        MatButtonModule,
        MatIconModule,
        MatCardModule,
        MatInputModule,
        MatSnackBarModule,
    ],
    exports: [
        LoginComponent,
    ],
    providers: [
        UsersApiService,
    ]
})
export class LoginModule { }
