import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IsAuthenticatedUserManagerGuard } from '@app/guards/is-authenticated-user-manager.guard';

import { MoviesCreateComponent } from './movies-list/movies-create/movies-create.component';
import { MoviesEditComponent } from './movies-list/movies-edit/movies-edit.component';
import { MoviesListComponent } from './movies-list/movies-list.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/movies/list',
        pathMatch: 'full',
    },
    {
        path: '',
        canActivate: [IsAuthenticatedUserManagerGuard],
        children: [
            {
                path: 'list',
                component: MoviesListComponent,
            },
            {
                path: 'create',
                component: MoviesCreateComponent,
            },
            {
                path: 'edit/:id',
                component: MoviesEditComponent,
            },
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class MoviesRoutingModule { }
