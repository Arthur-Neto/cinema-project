import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { GridModule } from '@components/grid/grid.module';
import { SpinnerModule } from '@components/spinner/spinner.module';

import { RoomsCreateComponent } from './rooms-list/rooms-create/rooms-create.component';
import { RoomsEditComponent } from './rooms-list/rooms-edit/rooms-edit.component';
import { RoomsListComponent } from './rooms-list/rooms-list.component';
import { RoomsRoutingModule } from './rooms.routing.module';
import { RoomsApiService, RoomsODataService } from './shared/rooms.service';

@NgModule({
    imports: [
        CommonModule,
        RoomsRoutingModule,
        GridModule,
        SpinnerModule,

        MatInputModule,
        MatTableModule,
        MatFormFieldModule,
        MatPaginatorModule,
        MatSortModule,
        MatSnackBarModule,
    ],
    declarations: [
        RoomsListComponent,
        RoomsCreateComponent,
        RoomsEditComponent,
    ],
    providers: [
        RoomsODataService,
        RoomsApiService,
    ]
})
export class RoomsModule { }
