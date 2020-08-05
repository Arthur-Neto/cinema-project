import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';

import { NavBarComponent } from './nav-bar.component';
import { NavBarRoutingModule } from './nav-bar.routing.module';

@NgModule({
    declarations: [
        NavBarComponent,
    ],
    imports: [
        CommonModule,
        NavBarRoutingModule,

        MatToolbarModule,
        MatButtonModule,
        MatSidenavModule,
        MatIconModule,
        MatInputModule,
    ],
    exports: [
        NavBarComponent
    ],
})
export class NavBarModule { }
