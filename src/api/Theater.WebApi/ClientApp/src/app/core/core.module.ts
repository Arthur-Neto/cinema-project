import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { ErrorInterceptor } from './interceptors/error-interceptor.service';
import { JwtInterceptor } from './interceptors/jwt-interceptor.service';

@NgModule({
    imports: [
        CommonModule,
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    ]
})
export class CoreModule { }
