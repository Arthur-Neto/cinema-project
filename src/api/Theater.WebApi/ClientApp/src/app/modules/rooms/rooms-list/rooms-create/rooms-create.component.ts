import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';

import { take } from 'rxjs/operators';

import { IRoomCreateCommand } from '../../shared/rooms.model';
import { RoomsApiService } from '../../shared/rooms.service';

@Component({
    templateUrl: './rooms-create.component.html',
    styleUrls: ['./rooms-create.component.scss']
})
export class RoomsCreateComponent implements OnInit {
    public form: FormGroup;

    public get showNameRequiredError(): boolean {
        return this.form.controls['name'].hasError('required');
    }
    public get showNameDuplicatedError(): boolean {
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
    ) { }

    public ngOnInit(): void {
        this.form = this.fb.group({
            name: [null, Validators.required],
            numberOfChairs: ['', [Validators.required, Validators.min(20), Validators.max(100)]],
        });
    }

    public onSubmit() {
        if (this.form.valid) {
            const command = <IRoomCreateCommand>{
                name: this.form.controls['name'].value,
                numberOfChairs: +this.form.controls['numberOfChairs'].value
            };

            this.roomsApiService
                .create(command)
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
        this.snackBar.open('Create success');
        this.backRoute();
    }

    private onErrorCallback(error: string): void {
        if (error.match('Duplicating')) {
            this.form.controls['name'].setErrors({ duplicated: true });
        }
    }

    private backRoute(): void {
        this.router.navigate(['../list'], { relativeTo: this.route });
    }
}
