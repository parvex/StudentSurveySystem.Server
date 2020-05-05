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
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SwaggerDateConverter = StudentSurveySystem.ApiClient.Client.SwaggerDateConverter;

namespace StudentSurveySystem.ApiClient.Model
{
    /// <summary>
    /// SurveyDto
    /// </summary>
    [DataContract]
        public partial class SurveyDto :  IEquatable<SurveyDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyDto" /> class.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="name">name.</param>
        /// <param name="creatorId">creatorId.</param>
        /// <param name="courseId">courseId.</param>
        /// <param name="questions">questions.</param>
        /// <param name="active">active.</param>
        /// <param name="courseName">courseName.</param>
        /// <param name="creatorName">creatorName.</param>
        /// <param name="endDate">endDate.</param>
        public SurveyDto(int? id = default(int?), string name = default(string), int? creatorId = default(int?), int? courseId = default(int?), List<QuestionDto> questions = default(List<QuestionDto>), bool? active = default(bool?), string courseName = default(string), string creatorName = default(string), DateTime? endDate = default(DateTime?))
        {
            this.Id = id;
            this.Name = name;
            this.CreatorId = creatorId;
            this.CourseId = courseId;
            this.Questions = questions;
            this.Active = active;
            this.CourseName = courseName;
            this.CreatorName = creatorName;
            this.EndDate = endDate;
        }
        
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets CreatorId
        /// </summary>
        [DataMember(Name="creatorId", EmitDefaultValue=false)]
        public int? CreatorId { get; set; }

        /// <summary>
        /// Gets or Sets CourseId
        /// </summary>
        [DataMember(Name="courseId", EmitDefaultValue=false)]
        public int? CourseId { get; set; }

        /// <summary>
        /// Gets or Sets Questions
        /// </summary>
        [DataMember(Name="questions", EmitDefaultValue=false)]
        public List<QuestionDto> Questions { get; set; }

        /// <summary>
        /// Gets or Sets Active
        /// </summary>
        [DataMember(Name="active", EmitDefaultValue=false)]
        public bool? Active { get; set; }

        /// <summary>
        /// Gets or Sets CourseName
        /// </summary>
        [DataMember(Name="courseName", EmitDefaultValue=false)]
        public string CourseName { get; set; }

        /// <summary>
        /// Gets or Sets CreatorName
        /// </summary>
        [DataMember(Name="creatorName", EmitDefaultValue=false)]
        public string CreatorName { get; set; }

        /// <summary>
        /// Gets or Sets EndDate
        /// </summary>
        [DataMember(Name="endDate", EmitDefaultValue=false)]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SurveyDto {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  CreatorId: ").Append(CreatorId).Append("\n");
            sb.Append("  CourseId: ").Append(CourseId).Append("\n");
            sb.Append("  Questions: ").Append(Questions).Append("\n");
            sb.Append("  Active: ").Append(Active).Append("\n");
            sb.Append("  CourseName: ").Append(CourseName).Append("\n");
            sb.Append("  CreatorName: ").Append(CreatorName).Append("\n");
            sb.Append("  EndDate: ").Append(EndDate).Append("\n");
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
            return this.Equals(input as SurveyDto);
        }

        /// <summary>
        /// Returns true if SurveyDto instances are equal
        /// </summary>
        /// <param name="input">Instance of SurveyDto to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(SurveyDto input)
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
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.CreatorId == input.CreatorId ||
                    (this.CreatorId != null &&
                    this.CreatorId.Equals(input.CreatorId))
                ) && 
                (
                    this.CourseId == input.CourseId ||
                    (this.CourseId != null &&
                    this.CourseId.Equals(input.CourseId))
                ) && 
                (
                    this.Questions == input.Questions ||
                    this.Questions != null &&
                    input.Questions != null &&
                    this.Questions.SequenceEqual(input.Questions)
                ) && 
                (
                    this.Active == input.Active ||
                    (this.Active != null &&
                    this.Active.Equals(input.Active))
                ) && 
                (
                    this.CourseName == input.CourseName ||
                    (this.CourseName != null &&
                    this.CourseName.Equals(input.CourseName))
                ) && 
                (
                    this.CreatorName == input.CreatorName ||
                    (this.CreatorName != null &&
                    this.CreatorName.Equals(input.CreatorName))
                ) && 
                (
                    this.EndDate == input.EndDate ||
                    (this.EndDate != null &&
                    this.EndDate.Equals(input.EndDate))
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
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.CreatorId != null)
                    hashCode = hashCode * 59 + this.CreatorId.GetHashCode();
                if (this.CourseId != null)
                    hashCode = hashCode * 59 + this.CourseId.GetHashCode();
                if (this.Questions != null)
                    hashCode = hashCode * 59 + this.Questions.GetHashCode();
                if (this.Active != null)
                    hashCode = hashCode * 59 + this.Active.GetHashCode();
                if (this.CourseName != null)
                    hashCode = hashCode * 59 + this.CourseName.GetHashCode();
                if (this.CreatorName != null)
                    hashCode = hashCode * 59 + this.CreatorName.GetHashCode();
                if (this.EndDate != null)
                    hashCode = hashCode * 59 + this.EndDate.GetHashCode();
                return hashCode;
            }
        }
    }
}
