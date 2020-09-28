import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

import * as moment from 'moment';
import { finalize, take } from 'rxjs/operators';

import { AudioType, IMovieDashboardModel, ScreenType } from '../../movies/shared/movies.model';
import { IRoomDashboardModel } from '../../rooms/shared/rooms.model';
import { ICreateOccupiedChairsCommand, IOccupiedChair, ISessionDashboardModel } from '../shared/sessions.model';
import { SessionsApiService, SessionsODataService } from '../shared/sessions.service';

@Component({
    templateUrl: './sessions-ticket-buy.component.html',
    styleUrls: ['./sessions-ticket-buy.component.scss']
})
export class SessionsBuyTicketComponent implements OnInit {
    public isLoading = true;
    public form: FormGroup;
    public session: ISessionDashboardModel;
    public movie: IMovieDashboardModel;
    public room: IRoomDashboardModel;
    public selectedDate: Date;
    public endingTime: string;
    public price: string;
    public chairs: number[];
    public chairsOccupied: number[] = [];
    public chairsSelected: number[] = [];

    public showNoChairsSelectedError = false;

    public audioType = AudioType;
    public screenType = ScreenType;

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private snackBar: MatSnackBar,
        private sessionsApiService: SessionsApiService,
        private sessionsODataService: SessionsODataService,
    ) {
        this.room = this.router.getCurrentNavigation().extras.state.room;
        this.movie = this.router.getCurrentNavigation().extras.state.movie;
        this.session = this.router.getCurrentNavigation().extras.state.session;
        this.selectedDate = this.router.getCurrentNavigation().extras.state.selectedDate;
    }

    public ngOnInit(): void {
        this.form = this.fb.group({
            chairs: [null, Validators.required],
            price: [null, Validators.required],
        });

        this.sessionsODataService
            .getOccupiedChairs(this.session.id)
            .pipe(
                take(1),
                finalize(() => this.isLoading = false))
            .subscribe((occupiedChairs: IOccupiedChair[]) => {
                this.chairsOccupied = occupiedChairs.map((value: IOccupiedChair, _) => value.number);
                this.chairs = Array(this.room.numberOfChairs);

                this.updateEndingTime();
                this.updatePrice();
            });
    }

    public onSubmit() {
        if (this.form.valid) {
            this.showNoChairsSelectedError = false;
            this.isLoading = true;

            const command = <ICreateOccupiedChairsCommand>{
                chairsNumbers: this.chairsSelected,
                sessionId: this.session.id
            }

            this.sessionsApiService
                .createOccupiedChairs(command)
                .pipe(
                    take(1),
                    finalize(() => this.isLoading = false))
                .subscribe({
                    next: this.onSuccessCallback.bind(this),
                });
        } else {
            this.showNoChairsSelectedError = true;
        }
    }

    public onChairSelect(number: number) {
        this.showNoChairsSelectedError = false;
        const chairSelectedIndex: number = this.chairsSelected.findIndex((x) => x === number);
        const chairsOccupiedIndex: number = this.chairsOccupied.findIndex((x) => x === number);

        if (chairsOccupiedIndex !== -1) {
            return;
        } else if (chairSelectedIndex !== -1) {
            this.chairsSelected.splice(chairSelectedIndex, 1);
        } else {
            this.chairsSelected.push(number);
        }
        this.chairsSelected = this.chairsSelected.sort();
        this.form.get('chairs').setValue(this.chairsSelected);

        this.updatePrice();
    }

    public getBackgroundColor(number: number): string {
        const chairSelectedIndex: number = this.chairsSelected.findIndex((x) => x === number);
        const chairsOccupiedIndex: number = this.chairsOccupied.findIndex((x) => x === number);

        if (chairSelectedIndex === -1 && chairsOccupiedIndex === -1) {
            return 'green';
        } else if (chairSelectedIndex !== -1) {
            return 'blueviolet';
        } else {
            return 'darkred';
        }
    }

    private updatePrice(): void {
        this.price = (this.chairsSelected.length * 10).toString();

        this.form.get('price').setValue(this.price);
    }

    private updateEndingTime(): void {
        const hour = this.movie.duration.split('h')[0];
        const minutes = this.movie.duration.split('h')[1].split('m')[0];
        const movieDurationOnMinutes = +hour * 60 + +minutes;

        this.endingTime = moment(this.selectedDate).add(movieDurationOnMinutes, 'm').format('HH:mm');
    }

    private onSuccessCallback(): void {
        this.isLoading = false;
        this.snackBar.open('Ticket bought');
        this.router.navigate(['dashboard']);
    }
}
