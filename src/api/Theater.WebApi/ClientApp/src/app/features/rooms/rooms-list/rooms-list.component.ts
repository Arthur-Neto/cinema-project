import { finalize, take } from 'rxjs/operators';

import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';

import { GridComponent } from '../../../components/grid/grid.component';
import { IActionModel } from '../../../components/grid/shared/grid.model';
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
                this.router.navigate(['create'], { relativeTo: this.route });
            }
        },
        {
            icon: 'edit',
            name: 'Edit',
            function: () => {
                this.router.navigate(['edit', this.selectedRoom.id], { relativeTo: this.route });
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

    @ViewChild(GridComponent, { static: false }) grid: GridComponent<IRoomsModel>;

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
                            this.grid.refreshGrid(rooms);
                        });
                }
            });
    }
}
