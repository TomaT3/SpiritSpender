import { Component, OnInit } from '@angular/core';
import { AutomaticApiService } from '../services/automatic-api.service';

@Component({
  selector: 'app-automatic-main',
  templateUrl: './automatic-main.component.html',
  styleUrls: ['./automatic-main.component.scss']
})
export class AutomaticMainComponent implements OnInit {

  constructor(private automaticApiService: AutomaticApiService) { }

  ngOnInit(): void {
  }

  public async releaseTheSpirit(): Promise<void> {
    await this.automaticApiService.releaseTheSpirit();
  }
}
