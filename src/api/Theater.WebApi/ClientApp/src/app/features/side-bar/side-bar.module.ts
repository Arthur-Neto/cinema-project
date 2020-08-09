import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterModule } from '@angular/router';

import { SideBarComponent } from './side-bar.component';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,

        MatSidenavModule,
        MatIconModule,
        MatButtonModule,
        MatDividerModule,
    ],
    declarations: [
        SideBarComponent,
    ],
    exports: [
        SideBarComponent,
    ]
})
export class SideBarModule { }
