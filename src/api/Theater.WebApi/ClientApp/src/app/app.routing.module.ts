import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
    },
    {
        path: 'dashboard',
        loadChildren: () => import('@modules/dashboard/dashboard.module').then(m => m.DashboardModule)
    },
    {
        path: 'login',
        loadChildren: () => import('@modules/login/login.module').then(m => m.LoginModule)
    },
    {
        path: 'rooms',
        loadChildren: () => import('@modules/rooms/rooms.module').then(m => m.RoomsModule)
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
