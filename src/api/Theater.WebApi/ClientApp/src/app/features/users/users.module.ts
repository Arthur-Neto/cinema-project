import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { UsersService } from './shared/users.service';

@NgModule({
    imports: [
        CommonModule
    ],
    exports: [
        UsersService,
    ]
})
export class UsersModule { }
