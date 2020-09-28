import { NgModule } from '@angular/core';

import { SharedModule } from '../../shared/shared.module';
import { ReportsListComponent } from './reports-list/reports-list.component';
import { ReportsMovieBillingComponent } from './reports-movie-billing/reports-movie-billing.component';
import { ReportsRoutingModule } from './reports.routing.module';
import { ReportsODataService } from './shared/reports.service';

@NgModule({
    imports: [
        SharedModule,
        ReportsRoutingModule,
    ],
    declarations: [
        ReportsListComponent,
        ReportsMovieBillingComponent,
    ],
    providers: [
        ReportsODataService,
    ]
})
export class ReportsModule { }
