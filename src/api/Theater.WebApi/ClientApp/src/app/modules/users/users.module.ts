import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { UsersApiService } from './shared/users.service';

@NgModule({
    imports: [
        CommonModule
    ],
    exports: [
        UsersApiService,
    ]
})
export class UsersModule { }
