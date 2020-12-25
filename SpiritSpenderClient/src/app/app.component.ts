import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from "@angular/platform-browser";
import { VersionInformation } from './shared/types/versioin-information';
import { VersionInformationApiService } from './shared/services/version-information-api.service';
import { MatDialog } from '@angular/material/dialog';
import { ReleaseNotesDialogComponent } from './dialogs/release-notes-dialog/release-notes-dialog.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'SpiritSpenderClient';
  navLinks: any[];
  activeLinkIndex = -1; 
  versionString: string;
  releaseNotesUrl: string;

  constructor(
    private versionApiService: VersionInformationApiService,
    private router: Router,
    private matIconRegistry: MatIconRegistry,
    public dialog: MatDialog,
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
    this.getVersionInformation();
  }

  async getVersionInformation(): Promise<VersionInformation> {
    const versionInformation = await this.versionApiService.getVersionInformation();
    this.versionString = "v" + versionInformation.version;
    this.releaseNotesUrl = versionInformation.releaseNotesUrl;
    return versionInformation;
  }

  openReleaseNotesDialog(): void {
    this.dialog.open(ReleaseNotesDialogComponent,{
      data: {
        releaseNotesUrl: this.releaseNotesUrl
      }
    });
  }

  private registerIcons(): void {
    this.matIconRegistry.addSvgIcon(
      `oneShotIcon`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/oneShotIcon.svg"));

      this.matIconRegistry.addSvgIcon(
        `emptyPositionIcon`,
        this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/emptyPositionIcon.svg"));
  }
}
