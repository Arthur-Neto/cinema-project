import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

import { IRoomsModel } from '../shared/rooms.model';
import { RoomsService } from '../shared/rooms.service';

@Component({
    templateUrl: './rooms-list.component.html',
    styleUrls: ['./rooms-list.component.scss']
})
export class RoomsListComponent implements OnInit, OnDestroy {
    public headerNames: string[] = ['Name', 'Number of Chairs'];
    public displayedColumns: string[] = ['name', 'numberOfChairs'];
    public dataSource: MatTableDataSource<IRoomsModel>;

    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private roomsService: RoomsService) { }

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
}
