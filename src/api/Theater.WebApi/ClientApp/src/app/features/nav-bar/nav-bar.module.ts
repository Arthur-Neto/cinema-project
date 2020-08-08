import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';

import { NavBarLoginComponent } from './nav-bar-login/nav-bar-login.component';
import { NavBarComponent } from './nav-bar.component';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,

        MatToolbarModule,
        MatButtonModule,
        MatIconModule,
        MatInputModule,
        MatMenuModule,
    ],
    declarations: [
        NavBarComponent,
        NavBarLoginComponent,
    ],
    exports: [
        NavBarComponent
    ],
})
export class NavBarModule { }
