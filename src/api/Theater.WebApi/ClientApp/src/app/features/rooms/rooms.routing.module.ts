import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IsAuthenticatedUserManagerGuard } from '@app/guards/is-authenticated-user-manager.guard';

import { RoomsCreateComponent } from './rooms-list/rooms-create/rooms-create.component';
import { RoomsEditComponent } from './rooms-list/rooms-edit/rooms-edit.component';
import { RoomsListComponent } from './rooms-list/rooms-list.component';

const routes: Routes = [
    {
        path: '',
        component: RoomsListComponent,
        canActivate: [IsAuthenticatedUserManagerGuard],
    },
    {
        path: 'rooms/create',
        component: RoomsCreateComponent,
        canActivate: [IsAuthenticatedUserManagerGuard],
    },
    {
        path: 'rooms/edit/:id',
        component: RoomsEditComponent,
        canActivate: [IsAuthenticatedUserManagerGuard],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class RoomsRoutingModule { }
