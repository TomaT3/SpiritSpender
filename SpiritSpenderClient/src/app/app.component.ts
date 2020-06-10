import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from "@angular/platform-browser";
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  private hubConnection: HubConnection;

  title = 'SpiritSpenderClient';
  navLinks: any[];
  activeLinkIndex = -1;
  constructor(
    private router: Router,
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer){
      this.registerIcons();
      this.navLinks = [
        {
            label: 'Automatic',
            path: './automatic',
            index: 0
        }, {
            label: 'Setup',
            path: './setup',
            index: 1
        },
    ];
  }

  ngOnInit(): void {
    this.router.events.subscribe((res) => {
        this.activeLinkIndex = this.navLinks.indexOf(this.navLinks.find(tab => tab.path === '.' + this.router.url));
    });

    // todo: This should be in its own service
    this.hubConnection = new HubConnectionBuilder().withUrl(environment.baseUrl).build();
    this.hubConnection
      .start()
      .then(() => console.log("SignalR connection established"))
      .catch(err => {
        console.error(`Error while establishing SignalR conneciton: ${err}`);
      });
  }

  private registerIcons(): void {
    this.matIconRegistry.addSvgIcon(
      `oneShotIcon`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/oneShotIcon.svg"));
  }
}
