﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Core.Models.Auth;
using Core.Models.Survey;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using Server.Entities;
using Server.Services;

namespace Server.Helpers
{
    public class DbInitializer
    {
        public static int Seed(SurveyContext context, IUserService userService, bool relational)
        {
            if (!context.Answer.Any() && !context.SurveyResponses.Any() && !context.Courses.Any() && !context.Questions.Any()
                && !context.Surveys.Any() && !context.Users.Any())
            {

                    if(relational)
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Users] ON");

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

                    context.SaveChanges();
                    if (relational)
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Users] OFF");

                    if (relational)
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Courses] ON");


                    List<Course> seedCourses = new List<Course>
                    {
                        new Course
                        {
                            Id = 0, Name = "SDI", Year = 2019, SemesterPart = SemesterPart.Summer,
                            LeaderId = seedUsers[2].Id.Value
                        },
                        new Course
                        {
                            Id = 1, Name = "WPAM", Year = 2019, SemesterPart = SemesterPart.Winter,
                            LeaderId = seedUsers[2].Id.Value
                        }
                    };

                    context.AddRange(seedCourses);
                    context.SaveChanges();
                    if (relational)
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Courses] OFF");

                    if (relational)
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Surveys] ON");


                    List<Survey> seedSurveys = new List<Survey>
                    {
                        new Survey
                        {
                            Id = 0, Name = "SDI survey", CourseId = seedCourses[0].Id.Value,
                            CreatorId = seedUsers[1].Id.Value
                        },
                        new Survey
                        {
                            Id = 1, Name = "WPAM survey", CourseId = seedCourses[1].Id.Value,
                            CreatorId = seedUsers[2].Id.Value
                        },

                    };

                    for (int i = 2; i < 100; ++i)
                    {
                        seedSurveys.Add(new Survey()
                        {
                            Id = i,
                            Name = $"Seed(empty) {i}",
                            CreatorId = i % 2 == 0 ? 1 : 2
                        });
                    }
                    context.AddRange(seedSurveys);
                    context.SaveChanges();
                    if (relational)
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Surveys] OFF");

                    if (relational)
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Questions] ON");
                    List<Question> seedQuestions = new List<Question>
                    {
                        new Question
                        {
                            Id = 0, QuestionType = QuestionType.Numeric, QuestionText = "Overall experience",
                            SurveyId = seedSurveys[0].Id.Value, Index = 1
                        },
                        new Question
                        {
                            Id = 1, QuestionType = QuestionType.Date, QuestionText = "Project submit date",
                            SurveyId = seedSurveys[0].Id.Value, Index = 2
                        },

                        new Question
                        {
                            Id = 2, QuestionType = QuestionType.Numeric, QuestionText = "Overall experience",
                            SurveyId = seedSurveys[1].Id.Value, Index = 3
                        },
                        new Question
                        {
                            Id = 3, QuestionType = QuestionType.Date, QuestionText = "Project submit date",
                            SurveyId = seedSurveys[1].Id.Value, Index = 4
                        },
                        new Question
                        {
                            Id = 4, QuestionType = QuestionType.Boolean, QuestionText = "Erasmus",
                            SurveyId = seedSurveys[1].Id.Value, Index = 5
                        },
                        new Question
                        {
                            Id = 5, QuestionType = QuestionType.Text, QuestionText = "Project name",
                            SurveyId = seedSurveys[1].Id.Value, Index = 6
                        },
                        new Question
                        {
                            Id = 6,
                            QuestionType = QuestionType.SingleSelect, QuestionText = "Technology",
                            SurveyId = seedSurveys[1].Id.Value, Index = 7,
                            Values = JsonConvert.SerializeObject(new List<string>
                                {"Xamarin", "Flutter", "React Native", "Ionic", "Adobe PhoneGap"})
                        },
                        new Question
                        {
                            Id = 7,
                            QuestionType = QuestionType.MultipleSelect, QuestionText = "Project includes",
                            SurveyId = seedSurveys[1].Id.Value, Index = 8,
                            Values = JsonConvert.SerializeObject(new List<string>
                                {"Rest API", "Web Service", "Database", "Authentication", "Authentication"})
                        }
                    };

                    context.AddRange(seedQuestions);
                    context.SaveChanges();
                    if (relational)
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Questions] OFF");

                    if (relational)
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Answer] ON");


                    var seedAnswers = new List<Answer>
                    {
                        new Answer {Id = 0, QuestionId = seedQuestions[0].Id.Value, Value = 7.ToString()},
                        new Answer
                        {
                            Id = 1, QuestionId = seedQuestions[1].Id.Value,
                            Value = new DateTime(2020, 1, 3).ToString(CultureInfo.InvariantCulture)
                        },
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
                    context.SaveChanges();
                    if (relational)
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Answer] OFF");

                    if (relational)
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[SurveyResponses] ON");

                    List<SurveyResponse> seedSurveyResponses = new List<SurveyResponse>
                    {
                        new SurveyResponse
                        {
                            Id = 0, Date = DateTime.Now, RespondentId = seedUsers[1].Id.Value,
                            SurveyId = seedSurveys[0].Id.Value,
                            Answers = new List<Answer> {seedAnswers[0], seedAnswers[1]}
                        },
                        new SurveyResponse
                        {
                            Id = 1, Date = DateTime.Now.AddDays(-2), RespondentId = seedUsers[1].Id.Value,
                            SurveyId = seedSurveys[1].Id.Value,
                            Answers = new List<Answer> {seedAnswers[2], seedAnswers[3], seedAnswers[4]}
                        }
                    };

                    context.AddRange(seedSurveyResponses);
                    context.SaveChanges();
                    if (relational)
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[SurveyResponses] OFF");
                    return 0;
            }
            else return -1;
        }

        public static void SeedAppDb(SurveyContext context, IUserService userService)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                Seed(context, userService, true);

                transaction.Commit();
            }
        }
    }
}