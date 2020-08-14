import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IsAuthenticatedUserManagerGuard } from '@app/guards/is-authenticated-user-manager.guard';

import { RoomsCreateComponent } from './rooms-list/rooms-create/rooms-create.component';
import { RoomsEditComponent } from './rooms-list/rooms-edit/rooms-edit.component';
import { RoomsListComponent } from './rooms-list/rooms-list.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/rooms/list',
        pathMatch: 'full',
    },
    {
        path: '',
        canActivate: [IsAuthenticatedUserManagerGuard],
        children: [
            {
                path: 'list',
                component: RoomsListComponent,
            },
            {
                path: 'create',
                component: RoomsCreateComponent,
            },
            {
                path: 'edit/:id',
                component: RoomsEditComponent,
            },
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class RoomsRoutingModule { }
