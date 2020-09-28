import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IsAuthenticatedUserManagerGuard } from '@app/guards/is-authenticated-user-manager.guard';

import { ReportsListComponent } from './reports-list/reports-list.component';
import { ReportsMovieBillingComponent } from './reports-movie-billing/reports-movie-billing.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/reports/list',
        pathMatch: 'full',
    },
    {
        path: '',
        canActivate: [IsAuthenticatedUserManagerGuard],
        children: [
            {
                path: 'list',
                component: ReportsListComponent,
            },
            {
                path: 'movies-billing',
                component: ReportsMovieBillingComponent,
            },
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ReportsRoutingModule { }
