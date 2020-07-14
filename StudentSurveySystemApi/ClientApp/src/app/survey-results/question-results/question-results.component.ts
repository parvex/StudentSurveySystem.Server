import { Component, OnInit, Input } from '@angular/core';
import { QuestionResultsDto, AnswerPercentage } from 'src/app/generated-api-client';

@Component({
  selector: 'app-question-results',
  templateUrl: './question-results.component.html',
  styleUrls: ['./question-results.component.scss']
})
export class QuestionResultsComponent implements OnInit {

  tabId = 1;

  @Input() questionResult: QuestionResultsDto;

  constructor() { }

  ngOnInit(): void {
  }

}
