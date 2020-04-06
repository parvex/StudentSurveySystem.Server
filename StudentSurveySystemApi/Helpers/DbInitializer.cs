using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using StudentSurveySystem.Core.Models;
using StudentSurveySystemApi.Entities;
using StudentSurveySystemApi.Services;

namespace StudentSurveySystemApi.Helpers
{
    public class DbInitializer
    {
        public static int Seed(SurveyContext context, IUserService userService)
        {
            if (!context.Answer.Any() && !context.SurveyResponses.Any() && !context.Courses.Any() && !context.Questions.Any()
                && !context.Surveys.Any() && !context.Users.Any())
            {

                List<User> seedUsers = new List<User>
                {
                    new User
                    {
                        Id = 0,
                        FirstName = "Adam", LastName = "Kowalski", Username = "student",
                        UserRole = UserRole.Student
                    },
                    new User
                    {
                        Id = 1,
                        FirstName = "Admin", LastName = "Adminowski", Username = "admin",
                        UserRole = UserRole.Admin
                    },
                    new User
                    {
                        Id = 2,
                        FirstName = "Wykładowca", LastName = "Wykładający", Username = "lecturer",
                        UserRole = UserRole.Lecturer
                    }
                };

                foreach (var user in seedUsers)
                {
                    userService.Create(user, "password");
                }

                List<Course> seedCourses = new List<Course>
                {
                    new Course {Id = 0, Name = "SDI", Year = 2019, SemesterPart = SemesterPart.Summer, LeaderId = seedUsers[2].Id.Value},
                    new Course {Id = 1, Name = "WPAM", Year = 2019, SemesterPart = SemesterPart.Winter, LeaderId =  seedUsers[2].Id.Value}
                };

                context.AddRange(seedCourses);

                List<Survey> seedSurveys = new List<Survey>
                {
                    new Survey {Id = 0, Name = "SDI survey", CourseId = seedCourses[0].Id.Value, CreatorId = seedUsers[1].Id.Value},
                    new Survey {Id = 1, Name = "WPAM survey", CourseId = seedCourses[1].Id.Value, CreatorId = seedUsers[2].Id.Value},

                };

                context.AddRange(seedSurveys);

                List<Question> seedQuestions = new List<Question>
                {
                    new Question {Id = 0, QuestionType = QuestionType.Numeric, QuestionText = "Overall experience", SurveyId = seedSurveys[0].Id.Value},
                    new Question {Id = 1, QuestionType = QuestionType.Date, QuestionText = "Project submit date", SurveyId = seedSurveys[0].Id.Value},

                    new Question {Id = 2, QuestionType = QuestionType.Numeric, QuestionText = "Overall experience", SurveyId = seedSurveys[1].Id.Value},
                    new Question {Id = 3, QuestionType = QuestionType.Date, QuestionText = "Project submit date", SurveyId = seedSurveys[1].Id.Value},
                    new Question {Id = 4, QuestionType = QuestionType.Boolean, QuestionText = "Erasmus", SurveyId = seedSurveys[1].Id.Value},
                    new Question {Id = 5, QuestionType = QuestionType.Text, QuestionText = "Project name", SurveyId = seedSurveys[1].Id.Value},
                    new Question
                    {
                        Id = 6,
                        QuestionType = QuestionType.SingleSelect, QuestionText = "Technology", SurveyId = seedSurveys[1].Id.Value,
                        Values = JsonConvert.SerializeObject(new List<string>{"Xamarin", "Flutter", "React Native", "Ionic", "Adobe PhoneGap"})
                    },
                    new Question
                    {
                        Id = 7,
                        QuestionType = QuestionType.MultipleSelect, QuestionText = "Project includes", SurveyId = seedSurveys[1].Id.Value,
                        Values = JsonConvert.SerializeObject(new List<string>{"Rest API", "Web Service", "Database", "Authentication", "Authentication"})
                    }
                };

                context.AddRange(seedQuestions);



                var seedAnswers = new List<Answer>
                {
                    new Answer {Id = 0, QuestionId = seedQuestions[0].Id.Value, Value = 7.ToString()},
                    new Answer {Id = 1, QuestionId = seedQuestions[0].Id.Value, Value = new DateTime(2020, 1, 3).ToString(CultureInfo.InvariantCulture)},
                    new Answer
                    {
                        Id = 2,
                        QuestionId = seedQuestions[1].Id.Value,
                        Value = DateTime.Now.AddDays(-2).ToString(CultureInfo.InvariantCulture)
                    },
                    new Answer {Id = 3, QuestionId = seedQuestions[2].Id.Value, Value = 9.ToString()},
                    new Answer
                    {
                        Id = 4,
                        QuestionId = seedQuestions[3].Id.Value,
                        Value = DateTime.Now.AddDays(-4).ToString(CultureInfo.InvariantCulture)
                    }
                };

                context.AddRange(seedAnswers);


                List<SurveyResponse> seedSurveyResponses = new List<SurveyResponse>
                {
                    new SurveyResponse {Id = 0, Date = DateTime.Now, RespondentId = seedUsers[1].Id.Value, SurveyId = seedSurveys[0].Id.Value, Answers = new List<Answer>{seedAnswers[0], seedAnswers[1]}},
                    new SurveyResponse {Id = 1, Date = DateTime.Now.AddDays(-2), RespondentId = seedUsers[1].Id.Value, SurveyId = seedSurveys[1].Id.Value, Answers = new List<Answer>{seedAnswers[2], seedAnswers[3], seedAnswers[4]}}
                };

                context.AddRange(seedSurveyResponses);


                return context.SaveChanges();
            }
            else return -1;
        }
    }
}