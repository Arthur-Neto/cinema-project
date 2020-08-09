import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { IsAuthenticatedUserManagerGuard } from '../../core/guards/is-authenticated-user-manager.guard';
import { RoomsListComponent } from './rooms-list/rooms-list.component';

const routes: Routes = [
    {
        path: '',
        component: RoomsListComponent,
        canActivate: [IsAuthenticatedUserManagerGuard]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class RoomsRoutingModule { }
