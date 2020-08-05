import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { NavBarComponent } from './nav-bar.component';

const routes: Routes = [
    {
        path: '',
        component: NavBarComponent,
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class NavBarRoutingModule { }
