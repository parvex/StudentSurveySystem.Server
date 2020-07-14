import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { SurveyResponsesService, SurveyResultsDto } from '../generated-api-client';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-survey-results',
  templateUrl: './survey-results.component.html',
  styleUrls: ['./survey-results.component.scss']
})
export class SurveyResultsComponent implements OnInit {


  activeTab = 1;
  id: number;
  surveyResults : SurveyResultsDto;

  constructor(private route: ActivatedRoute, private surveyResponsesService: SurveyResponsesService, private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.spinner.show();
    this.route.params.subscribe(
      (params: Params) => {
        this.id = +params['id'];
        this.surveyResponsesService.surveyResponsesSurveyResultsIdGet(this.id)
          .subscribe(data => {this.surveyResults = data; this.spinner.hide()});
      }
    );
  }

}
