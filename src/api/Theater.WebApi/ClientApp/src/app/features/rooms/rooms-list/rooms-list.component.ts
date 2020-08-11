import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';

import { IActionModel } from '../../../components/grid/shared/grid.model';
import { IRoomsModel } from '../shared/rooms.model';
import { RoomsService } from '../shared/rooms.service';

@Component({
    templateUrl: './rooms-list.component.html',
    styleUrls: ['./rooms-list.component.scss']
})
export class RoomsListComponent implements OnInit, OnDestroy {
    public dataSource: MatTableDataSource<IRoomsModel>;
    public selectedRoom: IRoomsModel;

    public headerNames: string[] = ['Name', 'Number of Chairs'];
    public displayedColumns: string[] = ['id', 'name', 'numberOfChairs'];
    public actions: IActionModel[] = [
        {
            icon: 'add',
            name: 'Add',
            function: () => {
                this.router.navigate(['/dashboard']);
            }
        },
        {
            icon: 'edit',
            name: 'Edit',
            function: () => {
                this.router.navigate(['/dashboard']);
            }
        },
        {
            icon: 'delete',
            name: 'Delete',
            function: () => {
                this.router.navigate(['/dashboard']);
            }
        }
    ];

    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(
        private roomsService: RoomsService,
        private router: Router,
    ) { }

    public ngOnInit(): void {
        this.roomsService
            .getAll()
            .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe((rooms: IRoomsModel[]) => {
                this.dataSource = new MatTableDataSource(rooms);
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    public onSelectionChange(room: IRoomsModel): void {
        this.selectedRoom = room;
    }
}
