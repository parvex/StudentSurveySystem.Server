import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { AboutComponent } from './about/about.component';
import { SurveyListComponent } from './surveys-list/surveys-list.component';
import { AuthGuard } from './auth/auth.guard';
import { NotFoundComponent } from './not-found/not-found.component';
import { SurveyResultsComponent } from './survey-results/survey-results.component';
import { SurveyListType } from './surveys-list/survey-list-type.enum';
import { SurveyFormComponent } from './survey-form/survey-form.component';


const routes: Routes = [
  { path: '', component: AboutComponent, canActivate:[AuthGuard]},
  { path: 'SurveysResults', component: SurveyListComponent, canActivate:[AuthGuard], data: { expectedRole: 'Lecturer', surveyListType: SurveyListType.Results}},
  { path: 'SurveysResults/:id', component: SurveyResultsComponent, canActivate:[AuthGuard], data: { expectedRole: 'Lecturer'}},
  { path: 'Surveys', component: SurveyListComponent, canActivate:[AuthGuard], data: { expectedRole: 'Lecturer', surveyListType: SurveyListType.Surveys}},
  { path: 'SurveyForm/:id', component: SurveyFormComponent, canActivate:[AuthGuard], data: { expectedRole: 'Lecturer'}},
  { path: 'SurveyTemplates', component: SurveyListComponent, canActivate:[AuthGuard], data: { expectedRole: 'Lecturer', surveyListType: SurveyListType.SurveyTemplates}},
  { path: 'SurveyTemplates/:id', component: SurveyFormComponent, canActivate:[AuthGuard], data: { expectedRole: 'Lecturer'}},
  { path: 'About', component: AboutComponent },
  { path: 'Auth', component: AuthComponent },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
