import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';

import { take } from 'rxjs/operators';

import { AudioType, IMovieCreateCommand, ScreenType } from '../../shared/movies.model';
import { MoviesApiService } from '../../shared/movies.service';

@Component({
    templateUrl: './movies-create.component.html',
    styleUrls: ['./movies-create.component.scss']
})
export class MoviesCreateComponent implements OnInit {
    public form: FormGroup;

    public screenType: any = ScreenType;
    public audioType: any = AudioType;

    public screenTypeSelected: ScreenType = ScreenType.three_dimension;
    public audioTypeSelected: AudioType = AudioType.dubbed;

    public get showTitleRequiredError(): boolean {
        return this.form.controls['title'].hasError('required');
    }
    public get showTitleDuplicatedError(): boolean {
        return this.form.controls['title'].hasError('duplicated');
    }
    public get showDescriptionRequiredError(): boolean {
        return this.form.controls['description'].hasError('required');
    }
    public get showDurationRequiredError(): boolean {
        return this.form.controls['duration'].hasError('required');
    }

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private snackBar: MatSnackBar,
        private movieApiService: MoviesApiService
    ) { }

    public ngOnInit(): void {
        this.form = this.fb.group({
            title: [null, Validators.required],
            description: [null, [Validators.required]],
            duration: [null, [Validators.required]],
        });
    }

    public onSubmit() {
        if (this.form.valid) {
            const command = <IMovieCreateCommand>{
                title: this.form.controls['title'].value,
                description: this.form.controls['description'].value,
                duration: this.form.controls['duration'].value,
                audioType: this.audioTypeSelected,
                screenType: this.screenTypeSelected,
            };

            this.movieApiService
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
            this.form.controls['title'].setErrors({ duplicated: true });
        }
    }

    private backRoute(): void {
        this.router.navigate(['../list'], { relativeTo: this.route });
    }
}
