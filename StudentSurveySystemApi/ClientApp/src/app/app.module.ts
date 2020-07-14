import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthComponent } from './auth/auth.component';
import { HeaderComponent } from './header/header.component';
import { SurveyListComponent } from './surveys-list/surveys-list.component';
import { SurveyResultsComponent } from './survey-results/survey-results.component';
import { AboutComponent } from './about/about.component';
import { HttpClientModule } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ApiModule, BASE_PATH, Configuration, CurrentUserDto } from './generated-api-client';
import { AuthService } from './auth/auth.service';
import { JwtModule } from '@auth0/angular-jwt';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { NotFoundComponent } from './not-found/not-found.component';
import { QuestionResultsComponent } from './survey-results/question-results/question-results.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { ResponseListComponent } from './survey-results/response-list/response-list.component';
import { ResponseDetailsComponent } from './survey-results/response-list/response-details/response-details.component';
import { NgxSpinnerModule } from "ngx-spinner";
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { SurveyFormComponent } from './survey-form/survey-form.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { QuestionFormComponent } from './survey-form/question-form/question-form.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    HeaderComponent,
    SurveyListComponent,
    SurveyResultsComponent,
    AboutComponent,
    NotFoundComponent,
    QuestionResultsComponent,
    ResponseListComponent,
    ResponseDetailsComponent,
    SurveyFormComponent,
    QuestionFormComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ApiModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    InfiniteScrollModule,
    NgxChartsModule,
    BrowserAnimationsModule,
    NgxSpinnerModule,
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    BsDatepickerModule.forRoot(),
    CollapseModule.forRoot(),
    TabsModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: getToken,
      },
    })
  ],
  providers: [
    {provide: BASE_PATH, useValue: environment.API_PATH },
    {
      provide: Configuration,
      useFactory: (authSvc: AuthService) => new Configuration({accessToken: authSvc.getAuthToken.bind(authSvc)}),
      deps: [AuthService],
      multi: false
    },
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule { }

export function getToken(){
  return (JSON.parse(localStorage.getItem("user")) as CurrentUserDto).token;
}
