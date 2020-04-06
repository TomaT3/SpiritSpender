import { Component, OnInit } from '@angular/core';
import { DrivesApiService } from 'src/app/setup/services/drives-api.service';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-setup-main',
  templateUrl: './setup-main.component.html',
  styleUrls: ['./setup-main.component.scss']
})
export class SetupMainComponent implements OnInit {
  public drives: string[];

  constructor(private drivesApiService: DrivesApiService) { }

  async ngOnInit(): Promise<void> {
    this.drives = await this.drivesApiService.getAllDrives();
  }
}
