/* 
 * Student survey system API
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace StudentSurveySystem.ApiClient.Model
{
    /// <summary>
    /// SurveyResponseDetailsDto
    /// </summary>
    [DataContract]
        public partial class SurveyResponseDetailsDto :  IEquatable<SurveyResponseDetailsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyResponseDetailsDto" /> class.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="respondentId">respondentId.</param>
        /// <param name="answers">answers.</param>
        /// <param name="surveyName">surveyName.</param>
        /// <param name="respondent">respondent.</param>
        /// <param name="date">date.</param>
        /// <param name="creator">creator.</param>
        /// <param name="courseName">courseName.</param>
        /// <param name="surveyId">surveyId.</param>
        public SurveyResponseDetailsDto(int? id = default(int?), int? respondentId = default(int?), List<AnswerDto> answers = default(List<AnswerDto>), string surveyName = default(string), string respondent = default(string), DateTime? date = default(DateTime?), string creator = default(string), string courseName = default(string), int? surveyId = default(int?))
        {
            this.Id = id;
            this.RespondentId = respondentId;
            this.Answers = answers;
            this.SurveyName = surveyName;
            this.Respondent = respondent;
            this.Date = date;
            this.Creator = creator;
            this.CourseName = courseName;
            this.SurveyId = surveyId;
        }
        
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or Sets RespondentId
        /// </summary>
        [DataMember(Name="respondentId", EmitDefaultValue=false)]
        public int? RespondentId { get; set; }

        /// <summary>
        /// Gets or Sets Answers
        /// </summary>
        [DataMember(Name="answers", EmitDefaultValue=false)]
        public List<AnswerDto> Answers { get; set; }

        /// <summary>
        /// Gets or Sets SurveyName
        /// </summary>
        [DataMember(Name="surveyName", EmitDefaultValue=false)]
        public string SurveyName { get; set; }

        /// <summary>
        /// Gets or Sets Respondent
        /// </summary>
        [DataMember(Name="respondent", EmitDefaultValue=false)]
        public string Respondent { get; set; }

        /// <summary>
        /// Gets or Sets Date
        /// </summary>
        [DataMember(Name="date", EmitDefaultValue=false)]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Gets or Sets Creator
        /// </summary>
        [DataMember(Name="creator", EmitDefaultValue=false)]
        public string Creator { get; set; }

        /// <summary>
        /// Gets or Sets CourseName
        /// </summary>
        [DataMember(Name="courseName", EmitDefaultValue=false)]
        public string CourseName { get; set; }

        /// <summary>
        /// Gets or Sets SurveyId
        /// </summary>
        [DataMember(Name="surveyId", EmitDefaultValue=false)]
        public int? SurveyId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SurveyResponseDetailsDto {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  RespondentId: ").Append(RespondentId).Append("\n");
            sb.Append("  Answers: ").Append(Answers).Append("\n");
            sb.Append("  SurveyName: ").Append(SurveyName).Append("\n");
            sb.Append("  Respondent: ").Append(Respondent).Append("\n");
            sb.Append("  Date: ").Append(Date).Append("\n");
            sb.Append("  Creator: ").Append(Creator).Append("\n");
            sb.Append("  CourseName: ").Append(CourseName).Append("\n");
            sb.Append("  SurveyId: ").Append(SurveyId).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as SurveyResponseDetailsDto);
        }

        /// <summary>
        /// Returns true if SurveyResponseDetailsDto instances are equal
        /// </summary>
        /// <param name="input">Instance of SurveyResponseDetailsDto to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SurveyResponseDetailsDto input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.RespondentId == input.RespondentId ||
                    (this.RespondentId != null &&
                    this.RespondentId.Equals(input.RespondentId))
                ) && 
                (
                    this.Answers == input.Answers ||
                    this.Answers != null &&
                    input.Answers != null &&
                    this.Answers.SequenceEqual(input.Answers)
                ) && 
                (
                    this.SurveyName == input.SurveyName ||
                    (this.SurveyName != null &&
                    this.SurveyName.Equals(input.SurveyName))
                ) && 
                (
                    this.Respondent == input.Respondent ||
                    (this.Respondent != null &&
                    this.Respondent.Equals(input.Respondent))
                ) && 
                (
                    this.Date == input.Date ||
                    (this.Date != null &&
                    this.Date.Equals(input.Date))
                ) && 
                (
                    this.Creator == input.Creator ||
                    (this.Creator != null &&
                    this.Creator.Equals(input.Creator))
                ) && 
                (
                    this.CourseName == input.CourseName ||
                    (this.CourseName != null &&
                    this.CourseName.Equals(input.CourseName))
                ) && 
                (
                    this.SurveyId == input.SurveyId ||
                    (this.SurveyId != null &&
                    this.SurveyId.Equals(input.SurveyId))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.RespondentId != null)
                    hashCode = hashCode * 59 + this.RespondentId.GetHashCode();
                if (this.Answers != null)
                    hashCode = hashCode * 59 + this.Answers.GetHashCode();
                if (this.SurveyName != null)
                    hashCode = hashCode * 59 + this.SurveyName.GetHashCode();
                if (this.Respondent != null)
                    hashCode = hashCode * 59 + this.Respondent.GetHashCode();
                if (this.Date != null)
                    hashCode = hashCode * 59 + this.Date.GetHashCode();
                if (this.Creator != null)
                    hashCode = hashCode * 59 + this.Creator.GetHashCode();
                if (this.CourseName != null)
                    hashCode = hashCode * 59 + this.CourseName.GetHashCode();
                if (this.SurveyId != null)
                    hashCode = hashCode * 59 + this.SurveyId.GetHashCode();
                return hashCode;
            }
        }
    }
}
