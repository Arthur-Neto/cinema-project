import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

import { MoviesCreateComponent } from './movies-list/movies-create/movies-create.component';
import { MoviesEditComponent } from './movies-list/movies-edit/movies-edit.component';
import { MoviesListComponent } from './movies-list/movies-list.component';
import { MoviesRoutingModule } from './movies.routing.module';
import { MoviesApiService, MoviesODataService } from './shared/movies.service';

@NgModule({
    imports: [
        SharedModule,
        MoviesRoutingModule,
    ],
    declarations: [
        MoviesListComponent,
        MoviesCreateComponent,
        MoviesEditComponent,
    ],
    providers: [
        MoviesODataService,
        MoviesApiService,
    ],
})
export class MoviesModule { }
