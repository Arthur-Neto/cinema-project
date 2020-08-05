import { LayoutModule } from '@angular/cdk/layout';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing.module';
import { DashboardModule } from './features/dashboard/dashboard.module';
import { LoginModule } from './features/login/login.module';
import { NavBarModule } from './features/nav-bar/nav-bar.module';

@NgModule({
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        LayoutModule,

        NavBarModule,
        DashboardModule,
        LoginModule,
    ],
    declarations: [AppComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }
