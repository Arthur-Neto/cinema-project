import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';

import { take } from 'rxjs/operators';

import { IRoomsModel, IRoomUpdateCommand } from '../../shared/rooms.model';
import { RoomsApiService } from '../../shared/rooms.service';

@Component({
    templateUrl: './rooms-edit.component.html',
    styleUrls: ['./rooms-edit.component.scss']
})
export class RoomsEditComponent implements OnInit {
    public form: FormGroup;

    private room: IRoomsModel;
    private roomId: number;

    public get showNameRequiredError(): boolean {
        return this.form.controls['name'].hasError('required');
    }
    public get showNameAlreadyExistsError(): boolean {
        return this.form.controls['name'].hasError('duplicated');
    }
    public get showNumberOfChairsRequiredError(): boolean {
        return this.form.controls['numberOfChairs'].hasError('required');
    }
    public get showNumberOfChairsMinLengthError(): boolean {
        return this.form.controls['numberOfChairs'].hasError('min');
    }
    public get showNumberOfChairsMaxLengthError(): boolean {
        return this.form.controls['numberOfChairs'].hasError('max');
    }

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private roomsApiService: RoomsApiService,
        private snackBar: MatSnackBar,
    ) {
        this.room = this.router.getCurrentNavigation().extras.state.room;
    }

    public ngOnInit(): void {
        this.form = this.fb.group({
            name: [this.room.name, Validators.required],
            numberOfChairs: [this.room.numberOfChairs, [Validators.required, Validators.min(20), Validators.max(100)]],
        });
    }

    public onSubmit() {
        if (this.form.valid) {
            const command = <IRoomUpdateCommand>{
                id: this.room.id,
                name: this.form.controls['name'].value,
                numberOfChairs: +this.form.controls['numberOfChairs'].value
            };

            this.roomsApiService
                .update(command)
                .pipe(take(1))
                .subscribe({
                    next: this.onSuccessCallback.bind(this),
                    error: this.onErrorCallback.bind(this)
                });
        }
    }

    public onCanceClick(): void {
        this.backRoute();
    }

    private onSuccessCallback(): void {
        this.snackBar.open('Update success');
        this.backRoute();
    }

    private onErrorCallback(error: string): void {
        if (error.match('Duplicating')) {
            this.form.controls['name'].setErrors({ duplicated: true });
        }
    }

    private backRoute(): void {
        this.router.navigate(['../../list'], { relativeTo: this.route });
    }
}
