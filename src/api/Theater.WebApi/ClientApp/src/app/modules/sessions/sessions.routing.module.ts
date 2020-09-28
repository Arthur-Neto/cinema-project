import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@app/guards/authentication.guard';
import { IsAuthenticatedUserManagerGuard } from '@app/guards/is-authenticated-user-manager.guard';

import { SessionsCreateComponent } from './sessions-list/sessions-create/sessions-create.component';
import { SessionsEditComponent } from './sessions-list/sessions-edit/sessions-edit.component';
import { SessionsListComponent } from './sessions-list/sessions-list.component';
import { SessionsBuyTicketComponent } from './sessions-ticket-buy/sessions-ticket-buy.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/sessions/list',
        pathMatch: 'full',
    },
    {
        path: '',
        children: [
            {
                path: 'list',
                canActivate: [IsAuthenticatedUserManagerGuard],
                component: SessionsListComponent,
            },
            {
                path: 'create',
                canActivate: [IsAuthenticatedUserManagerGuard],
                component: SessionsCreateComponent,
            },
            {
                path: 'edit/:id',
                canActivate: [IsAuthenticatedUserManagerGuard],
                component: SessionsEditComponent,
            },
            {
                path: 'buy-tickets',
                canActivate: [AuthGuard],
                component: SessionsBuyTicketComponent,
            },
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SessionsRoutingModule { }
