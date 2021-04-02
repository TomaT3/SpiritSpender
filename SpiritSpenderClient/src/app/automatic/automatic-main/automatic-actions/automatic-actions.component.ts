import { Component, OnInit } from '@angular/core';
import { AutomaticApiService } from '../../services/automatic-api.service';

@Component({
  selector: 'app-automatic-actions',
  templateUrl: './automatic-actions.component.html',
  styleUrls: ['./automatic-actions.component.scss']
})
export class AutomaticActionsComponent implements OnInit {

  constructor(private automaticApiService: AutomaticApiService) { }

  ngOnInit(): void {
  }

  public async referenceAllAxis(): Promise<void> {
    this.automaticApiService.referenceAllAxis()
  }

}
