import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AutomaticModule } from './automatic/automatic.module';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { SharedModule } from './shared/shared.module';
import { SetupModule } from './setup/setup.module';
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { HttpClientModule } from '@angular/common/http';
import { ReleaseNotesDialogComponent } from './dialogs/release-notes-dialog/release-notes-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    ReleaseNotesDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AutomaticModule,
    SharedModule,
    MatTabsModule,
    MatToolbarModule,
    SetupModule,
    HttpClientModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule
  ],
  providers: [],
  entryComponents: [
    ReleaseNotesDialogComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
