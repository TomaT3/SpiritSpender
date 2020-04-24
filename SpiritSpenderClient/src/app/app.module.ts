import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AutomaticModule } from './automatic/automatic.module';
import { MatTabsModule } from '@angular/material/tabs';
import { SharedModule } from './shared/shared.module';
import { SetupModule } from './setup/setup.module';
import { MatIconModule } from "@angular/material/icon";
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AutomaticModule,
    SharedModule,
    MatTabsModule,
    SetupModule,
    HttpClientModule,
    MatIconModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
