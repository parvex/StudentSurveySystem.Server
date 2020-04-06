using System.Collections.Generic;
using Core.Models;
using Mapster;
using Newtonsoft.Json;
using StudentSurveySystem.Core.Models;
using StudentSurveySystemApi.Entities;

namespace StudentSurveySystemApi.Helpers
{
    public class MapsterHelper
    {
        public static void SetCustomMappings()
        {
            TypeAdapterConfig<Survey, SurveyDto>
                .NewConfig()
                .Map(dest => dest.Creator,
                    src => $"{src.Creator.FirstName} {src.Creator.LastName}");

            TypeAdapterConfig<Question, QuestionDto>
                .NewConfig()
                .Map(dest => dest.Values,
                    src => src.Values != null ? JsonConvert.DeserializeObject<List<string>>(src.Values) : null);

            TypeAdapterConfig<SurveyResponse, SurveyResponseDetailsDto>
                .NewConfig()
                .Map(dest => dest.CourseName,
                    src => src.Survey.Course.Name)
                .Map(dest => dest.Creator, src => src.Survey.Creator.FirstName + " " + src.Survey.Creator.LastName)
                .Map(dest => dest.Respondent, src => src.Respondent.FirstName + " " + src.Respondent.LastName);

            TypeAdapterConfig<Answer, AnswerDto>
                .NewConfig()
                .Map(dest => dest.QuestionText,
                    src => src.Question.QuestionText)
                .Map(dest => dest.QuestionType, src => src.Question.QuestionType);
        }
    }
}