import { LayoutModule } from '@angular/cdk/layout';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { MAT_SNACK_BAR_DEFAULT_OPTIONS } from '@angular/material/snack-bar';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing.module';
import { SpinnerModule } from './components/spinner/spinner.module';
import { ErrorInterceptor } from './core/interceptors/error-interceptor.service';
import { JwtInterceptor } from './core/interceptors/jwt-interceptor.service';
import { DashboardModule } from './features/dashboard/dashboard.module';
import { LoginModule } from './features/login/login.module';
import { NavBarModule } from './features/nav-bar/nav-bar.module';
import { RoomsModule } from './features/rooms/rooms.module';
import { SideBarModule } from './features/side-bar/side-bar.module';
import { UsersModule } from './features/users/users.module';

@NgModule({
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        LayoutModule,
        SpinnerModule,

        NavBarModule,
        SideBarModule,
        DashboardModule,
        LoginModule,
        UsersModule,
        RoomsModule,
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
        { provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: { duration: 5000 } },
    ],
    declarations: [AppComponent],
    bootstrap: [AppComponent],
})
export class AppModule { }
