import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';

import { GridModule } from '../../components/grid/grid.module';
import { RoomsListComponent } from './rooms-list/rooms-list.component';
import { RoomsRoutingModule } from './rooms.routing.module';
import { RoomsService } from './shared/rooms.service';

@NgModule({
    imports: [
        CommonModule,
        RoomsRoutingModule,
        GridModule,

        MatInputModule,
        MatTableModule,
        MatFormFieldModule,
        MatPaginatorModule,
        MatSortModule,
    ],
    declarations: [
        RoomsListComponent,
    ],
    providers: [
        RoomsService,
    ]
})
export class RoomsModule { }
