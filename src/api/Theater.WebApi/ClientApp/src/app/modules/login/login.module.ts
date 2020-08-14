import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

import { UsersApiService } from '../users/shared/users.service';
import { LoginEditComponent } from './login-edit/login-edit.component';
import { LoginComponent } from './login.component';
import { LoginRoutingModule } from './login.routing.module';

@NgModule({
    imports: [
        SharedModule,
        LoginRoutingModule,
    ],
    declarations: [
        LoginComponent,
        LoginEditComponent,
    ],
    exports: [
        LoginComponent,
    ],
    providers: [
        UsersApiService,
    ]
})
export class LoginModule { }
