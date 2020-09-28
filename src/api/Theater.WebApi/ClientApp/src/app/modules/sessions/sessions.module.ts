import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

import { MoviesODataService } from '../movies/shared/movies.service';
import { RoomsApiService } from '../rooms/shared/rooms.service';
import { SessionsCreateComponent } from './sessions-list/sessions-create/sessions-create.component';
import { SessionsEditComponent } from './sessions-list/sessions-edit/sessions-edit.component';
import { SessionsListComponent } from './sessions-list/sessions-list.component';
import { SessionsBuyTicketComponent } from './sessions-ticket-buy/sessions-ticket-buy.component';
import { SessionsRoutingModule } from './sessions.routing.module';
import { SessionsApiService, SessionsODataService } from './shared/sessions.service';

@NgModule({
    imports: [
        SharedModule,
        SessionsRoutingModule,
    ],
    declarations: [
        SessionsListComponent,
        SessionsCreateComponent,
        SessionsEditComponent,
        SessionsBuyTicketComponent,
    ],
    providers: [
        SessionsODataService,
        SessionsApiService,
        RoomsApiService,
        MoviesODataService,
    ]
})
export class SessionsModule { }
