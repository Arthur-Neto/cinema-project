import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterModule } from '@angular/router';

import { SideBarComponent } from './side-bar.component';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,

        MatSidenavModule,
    ],
    declarations: [
        SideBarComponent,
    ],
    exports: [
        SideBarComponent,
    ]
})
export class SideBarModule { }
