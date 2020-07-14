import { Component, OnInit, Input } from '@angular/core';
import { SurveysService, SurveyListItemDto, SurveyDto } from '../generated-api-client';
import { NgxSpinnerService } from 'ngx-spinner';
import { SurveyListType } from './survey-list-type.enum';
import { ActivatedRoute, Router } from '@angular/router';
import { ModalModule, BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { SurveyFormComponent } from '../survey-form/survey-form.component';

@Component({
  selector: 'app-surveys-list',
  templateUrl: './surveys-list.component.html',
  styleUrls: ['./surveys-list.component.scss']
})
export class SurveyListComponent implements OnInit {

  pageSize = 20;
  filterText = "";
  selectedSurvey: SurveyDto;
  @Input() surveyListType: SurveyListType;
  modal: BsModalRef;
  page: number = 1;

  surveys: Array<SurveyListItemDto>;
  iconPath: string;
  constructor(private surveysService: SurveysService,
    private spinner : NgxSpinnerService,
     private route: ActivatedRoute,
     private router: Router,
     private modalService: BsModalService) {}

  ngOnInit(): void {
    this.surveyListType = this.route.snapshot.data.surveyListType;
    this.onFilter();
  }

  onFilter(){
    this.spinner.show();
    switch(this.surveyListType)
    {
      case SurveyListType.Surveys: {
        this.surveysService.surveysMySurveysGet(this.filterText, 0, this.pageSize).subscribe(data => {this.surveys = data; this.spinner.hide();})
        break;
      }
      case SurveyListType.Results: {
        this.surveysService.surveysMySurveysGet(this.filterText, 0, this.pageSize).subscribe(data =>
          {
            this.surveys = data;
            this.spinner.hide();})
        break;
      }
      case SurveyListType.SurveyTemplates: {
        this.surveysService.surveysMySurveyTemplatesGet(this.filterText, 0, this.pageSize).subscribe(data => {this.surveys = data; this.spinner.hide();})
        break;
      }
    }
  }

  onScrollDown(){
    let newPage = Math.floor(this.surveys.length/this.pageSize) + 1;
    if(newPage > this.page)
    {
      this.spinner.show();
      this.page = newPage;
      switch(this.surveyListType)
      {
        case SurveyListType.Surveys: {
          this.surveysService.surveysMySurveysGet(this.filterText, newPage, this.pageSize).subscribe(data => {this.surveys.push(...data); this.spinner.hide();})
          break;
        }
        case SurveyListType.Results: {
          this.surveysService.surveysMySurveysGet(this.filterText, newPage, this.pageSize, true).subscribe(data => {this.surveys.push(...data); this.spinner.hide();})
          break;
        }
        case SurveyListType.SurveyTemplates: {
          this.surveysService.surveysMySurveyTemplatesGet(this.filterText, newPage, this.pageSize).subscribe(data => {this.surveys.push(...data); this.spinner.hide();})
          break;
        }
      }
    }

  }

  onNavigate(id: number){
    switch(this.surveyListType)
    {
      case SurveyListType.Results: {
        this.router.navigate([id], { relativeTo: this.route });
        break;
      }
      case SurveyListType.Surveys: {
      }
      case SurveyListType.SurveyTemplates: {
        this.spinner.show();
        this.router.navigate(['SurveyForm', id]);
        this.spinner.hide();

        break;
      }
    }
  }

  onAdd(){
    let type = this.surveyListType === SurveyListType.SurveyTemplates ? 'template' : 'survey';
    this.router.navigate(['SurveyForm', type]);
  }
}
