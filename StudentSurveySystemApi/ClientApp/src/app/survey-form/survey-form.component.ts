import { Component, OnInit } from '@angular/core';
import { SurveyDto, SurveysService, SemesterDto, CourseDto, ValidationConfig, QuestionDto } from '../generated-api-client';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs/internal/Observable';
import { Subject } from 'rxjs';
import { QuestionFormComponent } from './question-form/question-form.component';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { arrayIsNotEmptyValidator } from '../shared/shared-validators';

@Component({
  selector: 'app-survey-form',
  templateUrl: './survey-form.component.html',
  styleUrls: ['./survey-form.component.scss']
})
export class SurveyFormComponent implements OnInit {
  surveyId;
  survey: SurveyDto;
  selectedSemester: SemesterDto;
  semesters : Array<SemesterDto>;
  courses: Subject<Array<CourseDto>>;
  deleteModalRef: BsModalRef;
  questionModalRef: BsModalRef;

  get name() { return this.surveyForm.get('name');}
  get endDate() { return this.surveyForm.get('endDate');}
  get anonymous() { return this.surveyForm.get('anonymous');}
  get active() { return this.surveyForm.get('active');}
  get semester() { return this.surveyForm.get('semester');}
  get courseId() { return this.surveyForm.get('courseId');}
  get questions() { return this.surveyForm.get('questions');}

  surveyForm = this.fb.group({
    name: ['', [Validators.required]],
    endDate: ['', [Validators.required]],
    isTemplate: [false],
    anonymous:[false],
    active: [false],
    questions: [[], [arrayIsNotEmptyValidator()]],
    semester: [''],
    courseId: ['', [Validators.required]],
  });

  constructor(private service: SurveysService,
    private modalService: BsModalService,
    private route: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private fb: FormBuilder,
    private router: Router) { }

  ngOnInit(): void {
    this.spinner.show('form');
    this.survey = { questions: []} as SurveyDto;
    this.route.params.subscribe(p => {
      this.surveyId = p['id'];
      if(this.surveyId === 'survey'){
        this.survey.isTemplate = false;
        this.surveyForm.patchValue(this.survey);
        this.loadCourses();
      }
      else if (this.surveyId === 'template'){
        this.survey.isTemplate = true;
        this.surveyForm.patchValue(this.survey);
        this.loadCourses();
      }
      else{
        this.service.surveysIdGet(this.surveyId).subscribe(s => {
          this.survey = s;
          this.survey.endDate = new Date(this.survey.endDate);
          this.surveyForm.patchValue(this.survey);
          this.loadCourses();
        });
      }
    })
  }

  loadCourses(){
    this.service.surveysGetSemestersAndMyCoursesGet().subscribe(x => {
      this.semesters = x
      let semester = this.semesters.find(x => x.courses.some(y => y.id == this.survey.courseId));
      this.semester.setValue(semester ?? this.semesters.slice(-1)[0]);
      this.onSemesterSelect();
      this.spinner.hide('form');
    });
  }

  onSemesterSelect(){
    this.courses = this.semester.value.courses;
  }

  onSubmit(){
    this.semester.setValue(null);
    console.log(this.surveyForm);
    if(this.surveyId !== 'survey' && this.surveyId!=='template'){
      this.service.surveysIdPut(this.surveyId, this.surveyForm.value).subscribe(x => {
        this.onNavigateBack();
      });
    }
    else{
      this.service.surveysPost(this.surveyForm.value).subscribe(x => {
        this.onNavigateBack();
      });
    }
  }

  onNavigateBack(){
    if(!this.survey.isTemplate)
      this.router.navigate(['Surveys']);
    else
      this.router.navigate(['SurveyTemplates']);
  }

  onAddQuestion(){
    let question = { index: this.survey.questions.length+1 ,validationConfig: {} as ValidationConfig} as QuestionDto;
    this.questionModalRef = this.modalService.show(QuestionFormComponent, {initialState: {question: question}, class: 'modal-xl'})
    this.questionModalRef.content.saved.subscribe(q => {
      if(q.index > this.questions.value.length)
        this.questions.value.push(q);
      else
        this.questions.value.splice(q.index-1, 0, q);
        this.normalizeIndexes();
    })
  }

  onQuestionEditClicked(id){
    let question = this.survey.questions[id];
    this.questionModalRef = this.modalService.show(QuestionFormComponent, {initialState: {question: question} , class: 'modal-xl'})
    this.questionModalRef.content.saved.subscribe(q => {
      this.survey.questions.splice(id, 1);
      if(q.index > this.survey.questions.length)
        this.questions.value.push(q);
      else
        this.questions.value.splice(q.index-1, 0, q);

      this.normalizeIndexes();
    })
   }

  onQuestionDeleteClicked(id){
    this.survey.questions.splice(id, 1);
    this.normalizeIndexes();
  }

  normalizeIndexes(){
    for (let i = 0; i < this.questions.value.length; ++i)
    {
        this.questions.value[i].index = i + 1;
    }
    this.questions.updateValueAndValidity();
  }

}
