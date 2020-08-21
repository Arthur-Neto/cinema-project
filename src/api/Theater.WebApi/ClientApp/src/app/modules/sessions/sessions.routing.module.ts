import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IsAuthenticatedUserManagerGuard } from '@app/guards/is-authenticated-user-manager.guard';

import { SessionsCreateComponent } from './sessions-list/sessions-create/sessions-create.component';
import { SessionsEditComponent } from './sessions-list/sessions-edit/sessions-edit.component';
import { SessionsListComponent } from './sessions-list/sessions-list.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/sessions/list',
        pathMatch: 'full',
    },
    {
        path: '',
        canActivate: [IsAuthenticatedUserManagerGuard],
        children: [
            {
                path: 'list',
                component: SessionsListComponent,
            },
            {
                path: 'create',
                component: SessionsCreateComponent,
            },
            {
                path: 'edit/:id',
                component: SessionsEditComponent,
            },
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SessionsRoutingModule { }
