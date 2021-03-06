import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { IActionModel } from '@shared/components/grid/shared/grid.model';

import { finalize, take } from 'rxjs/operators';

import { IRoomsModel } from '../shared/rooms.model';
import { RoomsApiService, RoomsODataService } from '../shared/rooms.service';

@Component({
    templateUrl: './rooms-list.component.html',
    styleUrls: ['./rooms-list.component.scss']
})
export class RoomsListComponent implements OnInit {
    public dataSource: MatTableDataSource<IRoomsModel>;
    public selectedRoom: IRoomsModel;
    public isLoading = true;

    public headerNames: string[] = ['Name', 'Number of Chairs'];
    public displayedColumns: string[] = ['id', 'name', 'numberOfChairs'];
    public actions: IActionModel[] = [
        {
            icon: 'add',
            name: 'Add',
            function: () => {
                this.router.navigate(['../create'], { relativeTo: this.route });
            }
        },
        {
            icon: 'edit',
            name: 'Edit',
            function: () => {
                if (!this.selectedRoom) {
                    this.snackBar.open('Select a room');

                    return;
                }

                this.router.navigate(['../edit', this.selectedRoom.id], { relativeTo: this.route, state: { room: this.selectedRoom } });
            }
        },
        {
            icon: 'delete',
            name: 'Delete',
            function: () => {
                this.onDelete();
            }
        }
    ];

    constructor(
        private roomsODataService: RoomsODataService,
        private roomsApiService: RoomsApiService,
        private snackBar: MatSnackBar,
        private router: Router,
        private route: ActivatedRoute,
    ) { }

    public ngOnInit(): void {
        this.roomsODataService
            .getAll()
            .pipe(
                take(1),
                finalize(() => this.isLoading = false))
            .subscribe((rooms: IRoomsModel[]) => {
                this.dataSource = new MatTableDataSource(rooms);
            });
    }

    public onSelectionChange(room: IRoomsModel): void {
        this.selectedRoom = room;
    }

    private onDelete(): void {
        if (!this.selectedRoom) {
            this.snackBar.open('Select a room');

            return;
        }

        this.isLoading = true;
        this.roomsApiService
            .delete(this.selectedRoom.id)
            .pipe(take(1))
            .subscribe({
                next: () => {
                    this.snackBar.open('Delete success');

                    this.roomsODataService
                        .getAll()
                        .pipe(
                            take(1),
                            finalize(() => this.isLoading = false))
                        .subscribe((rooms: IRoomsModel[]) => {
                            this.dataSource.data = rooms;
                        });
                },
                error: this.onErrorCallback.bind(this)
            });
    }

    private onErrorCallback(error: string): void {
        if (error.match('RoomWithSession')) {
            this.snackBar.open(`Room included in a session`);
        }

        this.isLoading = false;
    }
}
