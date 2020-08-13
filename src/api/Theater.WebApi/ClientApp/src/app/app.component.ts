import { Component } from '@angular/core';
import { Event, NavigationEnd, NavigationError, NavigationStart, Router } from '@angular/router';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html'
})
export class AppComponent {
    public isLoading: boolean;

    constructor(private router: Router) {

        this.router.events.subscribe((event: Event) => {
            if (event instanceof NavigationStart) {
                this.isLoading = true;
            }

            if (event instanceof NavigationEnd) {
                this.isLoading = false;
            }

            if (event instanceof NavigationError) {
                this.isLoading = false;

                console.log(event.error);
            }
        });
    }
}
