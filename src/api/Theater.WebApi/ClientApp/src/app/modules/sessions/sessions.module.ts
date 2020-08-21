import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

import { SessionsCreateComponent } from './sessions-list/sessions-create/sessions-create.component';
import { SessionsEditComponent } from './sessions-list/sessions-edit/sessions-edit.component';
import { SessionsListComponent } from './sessions-list/sessions-list.component';
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
    ],
    providers: [
        SessionsODataService,
        SessionsApiService,
    ]
})
export class SessionsModule { }
