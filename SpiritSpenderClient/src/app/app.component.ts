import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from "@angular/platform-browser";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
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
  }

  private registerIcons(): void {
    this.matIconRegistry.addSvgIcon(
      `oneShotIcon`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/oneShotIcon.svg"));

      this.matIconRegistry.addSvgIcon(
        `not_interested`,
        this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/not_interested.svg"));
  }
}
