import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env';

import { Observable } from 'rxjs';

import { IReportsMoviesBilling } from './reports.model';

@Injectable()
export class ReportsODataService {
    private apiUrl: string;

    constructor(
        private http: HttpClient
    ) {
        this.apiUrl = `${ environment.apiUrl }odata/reports`;
    }

    public getMoviesBillingReport(): Observable<IReportsMoviesBilling[]> {
        return this.http.get<IReportsMoviesBilling[]>(`${ this.apiUrl }/movies-billing`);
    }
}
