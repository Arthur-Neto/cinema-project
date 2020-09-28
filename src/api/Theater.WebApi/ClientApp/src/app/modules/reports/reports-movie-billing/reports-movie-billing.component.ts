import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

import { finalize, take } from 'rxjs/operators';

import { IReportsMoviesBilling } from '../shared/reports.model';
import { ReportsODataService } from '../shared/reports.service';

@Component({
    templateUrl: './reports-movie-billing.component.html',
    styleUrls: ['./reports-movie-billing.component.scss']
})
export class ReportsMovieBillingComponent implements OnInit {
    public isLoading = true;
    public dataSource: MatTableDataSource<IReportsMoviesBilling>;

    public headerNames: string[] = ['Movie', 'Total Billing'];
    public displayedColumns: string[] = ['movieName', 'totalBilling'];

    constructor(
        private reportsODataService: ReportsODataService
    ) { }

    public ngOnInit(): void {
        this.reportsODataService
            .getMoviesBillingReport()
            .pipe(
                take(1),
                finalize(() => this.isLoading = false))
            .subscribe((moviesBilling: IReportsMoviesBilling[]) => {
                this.dataSource = new MatTableDataSource(moviesBilling);
            });
    }
}
