import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
    },
    {
        path: 'auth',
        loadChildren: () => import('@modules/login/login.module').then(m => m.LoginModule)
    },
    {
        path: 'dashboard',
        loadChildren: () => import('@modules/dashboard/dashboard.module').then(m => m.DashboardModule)
    },
    {
        path: 'rooms',
        loadChildren: () => import('@modules/rooms/rooms.module').then(m => m.RoomsModule)
    },
    {
        path: 'movies',
        loadChildren: () => import('@modules/movies/movies.module').then(m => m.MoviesModule)
    },
    {
        path: 'sessions',
        loadChildren: () => import('@modules/sessions/sessions.module').then(m => m.SessionsModule)
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { useHash: true })],
    exports: [RouterModule]
})
export class AppRoutingModule { }
