import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { RouterModule } from '@angular/router';

import { LoginComponent } from './login.component';
import { LoginRoutingModule } from './login.routing.module';

@NgModule({
    declarations: [
        LoginComponent,
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
    ],
    exports: [
        LoginComponent,
    ],
})
export class LoginModule { }
