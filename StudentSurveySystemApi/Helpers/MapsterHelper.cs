using System.Collections.Generic;
using Core.Models.Survey;
using Core.Models.SurveyResponse;
using Mapster;
using Newtonsoft.Json;
using Server.Entities;

namespace Server.Helpers
{
    public class MapsterHelper
    {
        public static void SetCustomMappings()
        {
            TypeAdapterConfig<Survey, SurveyDto>.NewConfig()
                .Map(dest => dest.CreatorName,
                    src => $"{src.Creator.FirstName} {src.Creator.LastName}");

            TypeAdapterConfig<Question, QuestionDto>.NewConfig()
                .Map(dest => dest.Values,
                    src => src.Values != null ? JsonConvert.DeserializeObject<List<string>>(src.Values) : null)
                .Map(dest => dest.ValidationConfig,
                src => src.ValidationConfig != null ? JsonConvert.DeserializeObject<ValidationConfig>(src.ValidationConfig) : null);
            TypeAdapterConfig<QuestionDto, Question>.NewConfig()
                .Map(dest => dest.Values, src => JsonConvert.SerializeObject(src.Values))
                .Map(dest => dest.ValidationConfig, src => JsonConvert.SerializeObject(src.ValidationConfig));

            TypeAdapterConfig<SurveyResponse, SurveyResponseDetailsDto>.NewConfig()
                .Map(dest => dest.CourseName,
                    src => src.Survey.Course.Name)
                .Map(dest => dest.Creator, src => src.Survey.Creator.FirstName + " " + src.Survey.Creator.LastName)
                .Map(dest => dest.Respondent, src => src.Respondent.FirstName + " " + src.Respondent.LastName);

            TypeAdapterConfig<Answer, AnswerDto>.NewConfig()
                .Map(dest => dest.QuestionText,
                    src => src.Question.QuestionText)
                .Map(dest => dest.QuestionType, src => src.Question.QuestionType);

        }
    }
}