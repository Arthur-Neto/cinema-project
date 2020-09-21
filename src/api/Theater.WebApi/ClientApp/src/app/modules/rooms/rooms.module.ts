import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

import { RoomsCreateComponent } from './rooms-list/rooms-create/rooms-create.component';
import { RoomsEditComponent } from './rooms-list/rooms-edit/rooms-edit.component';
import { RoomsListComponent } from './rooms-list/rooms-list.component';
import { RoomsRoutingModule } from './rooms.routing.module';
import { RoomsApiService, RoomsODataService } from './shared/rooms.service';

@NgModule({
    imports: [
        SharedModule,
        RoomsRoutingModule,
    ],
    declarations: [
        RoomsListComponent,
        RoomsCreateComponent,
        RoomsEditComponent,
    ],
    providers: [
        RoomsODataService,
        RoomsApiService,
    ],
})
export class RoomsModule { }
