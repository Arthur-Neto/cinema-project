import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ReportType } from '../shared/reports.enum';

@Component({
    templateUrl: './reports-list.component.html',
    styleUrls: ['./reports-list.component.scss']
})
export class ReportsListComponent {
    public reportType = ReportType;

    constructor(
        private router: Router,
        private route: ActivatedRoute
    ) { }

    public onGenerateReport(reportType: ReportType): void {
        switch (reportType) {
            case ReportType.MovieBilling:
            default:
                this.router.navigate(['../movies-billing'], { relativeTo: this.route });
                break;
        }
    }
}
