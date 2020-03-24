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
        public static async Task<int> Seed(SurveyContext context, IUserService userService)
        {
            if (!context.Answer.Any() && !context.SurveyResponses.Any() && !context.Courses.Any() && !context.Questions.Any()
                && !context.Surveys.Any() && !context.Users.Any())
            {

                List<User> seedUsers = new List<User>
                {
                    new User
                    {
                        FirstName = "Adam", LastName = "Kowalski", Username = "student",
                        UserRole = UserRole.Student
                    },
                    new User
                    {
                        FirstName = "Admin", LastName = "Adminowski", Username = "admin",
                        UserRole = UserRole.Admin
                    },
                    new User
                    {
                        FirstName = "Wykładowca", LastName = "Wykładający", Username = "lecturer",
                        UserRole = UserRole.Lecturer
                    }
                };

                foreach (var user in seedUsers)
                {
                    await userService.Create(user, "password");
                }

                await context.SaveChangesAsync();


                List<Course> seedCourses = new List<Course>
                {
                    new Course {Name = "SDI", Year = 2019, SemesterPart = SemesterPart.Summer, LeaderId = seedUsers[2].Id.Value},
                    new Course {Name = "WPAM", Year = 2019, SemesterPart = SemesterPart.Winter, LeaderId =  seedUsers[2].Id.Value}
                };

                context.AddRange(seedCourses);
                await context.SaveChangesAsync();



                List<Survey> seedSurveys = new List<Survey>
                {
                    new Survey {Name = "SDI survey", CourseId = seedCourses[0].Id.Value, CreatorId = seedUsers[1].Id.Value},
                    new Survey {Name = "WPAM survey", CourseId = seedCourses[1].Id.Value, CreatorId = seedUsers[2].Id.Value},

                };

                context.AddRange(seedSurveys);
                await context.SaveChangesAsync();


                List<Question> seedQuestions = new List<Question>
                {
                    new Question {QuestionType = QuestionType.Numeric, QuestionText = "Overall experience", SurveyId = seedSurveys[0].Id.Value},
                    new Question {QuestionType = QuestionType.Date, QuestionText = "Project submit date", SurveyId = seedSurveys[0].Id.Value},
                    new Question {QuestionType = QuestionType.Numeric, QuestionText = "Overall experience", SurveyId = seedSurveys[1].Id.Value},
                    new Question {QuestionType = QuestionType.Date, QuestionText = "Project submit date", SurveyId = seedSurveys[1].Id.Value},
                    new Question {QuestionType = QuestionType.Boolean, QuestionText = "Erasmus", SurveyId = seedSurveys[1].Id.Value},
                    new Question {QuestionType = QuestionType.Text, QuestionText = "Project name", SurveyId = seedSurveys[1].Id.Value},
                    new Question
                    {
                        QuestionType = QuestionType.SingleSelect, QuestionText = "Technology", SurveyId = seedSurveys[1].Id.Value,
                        Values = JsonConvert.SerializeObject(new List<string>{"Xamarin", "Flutter", "React Native", "Ionic", "Adobe PhoneGap"})
                    },
                    new Question
                    {
                        QuestionType = QuestionType.MultipleSelect, QuestionText = "Project includes", SurveyId = seedSurveys[1].Id.Value,
                        Values = JsonConvert.SerializeObject(new List<string>{"Rest API", "Web Service", "Database", "Authentication", "Authentication"})
                    }
                };

                context.AddRange(seedQuestions);
                await context.SaveChangesAsync();


                List<SurveyResponse> seedSurveyResponses = new List<SurveyResponse>
                {
                    new SurveyResponse {Date = DateTime.Now,RespondentId = seedUsers[1].Id.Value, SurveyId = seedSurveys[0].Id.Value},
                    new SurveyResponse {Date = DateTime.Now.AddDays(-2) ,RespondentId = seedUsers[1].Id.Value, SurveyId = seedSurveys[1].Id.Value}
                };

                context.AddRange(seedSurveyResponses);
                await context.SaveChangesAsync();

                var seedAnswers = new List<Answer>
                {
                    new Answer {QuestionId = seedQuestions[0].Id.Value, Value = 7.ToString()},
                    new Answer
                    {
                        QuestionId = seedQuestions[1].Id.Value,
                        Value = DateTime.Now.AddDays(-2).ToString(CultureInfo.InvariantCulture)
                    },
                    new Answer {QuestionId = seedQuestions[2].Id.Value, Value = 9.ToString()},
                    new Answer
                    {
                        QuestionId = seedQuestions[3].Id.Value,
                        Value = DateTime.Now.AddDays(-4).ToString(CultureInfo.InvariantCulture)
                    }
                };

                context.AddRange(seedAnswers);

                return await context.SaveChangesAsync();
            }
            else return -1;
        }
    }
}