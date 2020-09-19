import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';

import { finalize, take } from 'rxjs/operators';

import { AudioType, IMovieCoverUploadCommand, IMovieCreateCommand, ScreenType } from '../../shared/movies.model';
import { MoviesApiService } from '../../shared/movies.service';

@Component({
    templateUrl: './movies-create.component.html',
    styleUrls: ['./movies-create.component.scss']
})
export class MoviesCreateComponent implements OnInit {
    public form: FormGroup;

    public isLoading = true;

    public screenType: any = ScreenType;
    public audioType: any = AudioType;

    public file: File | null = null;

    public screenTypeSelected: ScreenType = ScreenType.three_dimension;
    public audioTypeSelected: AudioType = AudioType.dubbed;

    public showRequiredSelectedFile = false;

    public get showTitleRequiredError(): boolean {
        return this.form.controls['title'].hasError('required');
    }
    public get showTitleDuplicatedError(): boolean {
        return this.form.controls['title'].hasError('duplicated');
    }
    public get showDescriptionRequiredError(): boolean {
        return this.form.controls['description'].hasError('required');
    }
    public get showDescriptionMinLengthError(): boolean {
        return this.form.controls['description'].hasError('minLength');
    }
    public get showDescriptionMaxLengthError(): boolean {
        return this.form.controls['description'].hasError('maxLength');
    }
    public get showDurationRequiredError(): boolean {
        return this.form.controls['duration'].hasError('required');
    }

    @ViewChild('fileInput') fileInput: { nativeElement: { click: () => void; files: { [key: string]: File; }; }; };

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
            description: [null, [Validators.required, Validators.minLength(1), Validators.maxLength(200)]],
            duration: [null, [Validators.required]],
        });

        this.isLoading = false;
    }

    public onSubmit() {
        if (this.form.valid) {
            if (!this.file) {
                this.showRequiredSelectedFile = true;
                return;
            } else {
                this.showRequiredSelectedFile = false;
            }

            this.isLoading = false;
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


    public onClickFileInputButton(): void {
        this.fileInput.nativeElement.click();
    }

    public onChangeFileInput(): void {
        const files: { [key: string]: File } = this.fileInput.nativeElement.files;
        this.file = files[0];
        this.showRequiredSelectedFile = false;
    }

    public onCanceClick(): void {
        this.backRoute();
    }

    private onSuccessCallback(movieId: number): void {
        const command = <IMovieCoverUploadCommand>{
            movieId,
            imgFile: this.file
        };

        this.movieApiService
            .updateCover(command)
            .pipe(
                take(1),
                finalize(() => this.isLoading = false))
            .subscribe({
                next: () => {
                    this.snackBar.open('Create success');
                    this.backRoute();
                },
            });
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
