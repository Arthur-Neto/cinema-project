import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

import { MoviesDashboardODataService } from '../movies/shared/movies.service';
import { DashboardComponent } from './dashboard.component';
import { DashboardRoutingModule } from './dashboard.routing.module';

@NgModule({
    imports: [
        SharedModule,
        DashboardRoutingModule,
    ],
    declarations: [
        DashboardComponent,
    ],
    exports: [
        DashboardComponent,
    ],
    providers: [
        MoviesDashboardODataService,
    ]
})
export class DashboardModule { }
