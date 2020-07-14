import { Component, OnInit, Input } from '@angular/core';
import { SurveyResponseDetailsDto } from 'src/app/generated-api-client';

@Component({
  selector: 'app-response-details',
  templateUrl: './response-details.component.html',
  styleUrls: ['./response-details.component.scss']
})
export class ResponseDetailsComponent implements OnInit {

  @Input() response: SurveyResponseDetailsDto;
  constructor() { }

  ngOnInit(): void {
  }

}
