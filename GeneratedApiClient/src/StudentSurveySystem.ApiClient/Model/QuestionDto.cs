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
    /// QuestionDto
    /// </summary>
    [DataContract]
        public partial class QuestionDto :  IEquatable<QuestionDto>
    {
        /// <summary>
        /// Gets or Sets QuestionType
        /// </summary>
        [DataMember(Name="questionType", EmitDefaultValue=false)]
        public QuestionType? QuestionType { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionDto" /> class.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="questionText">questionText.</param>
        /// <param name="questionType">questionType.</param>
        /// <param name="values">values.</param>
        public QuestionDto(int? id = default(int?), string questionText = default(string), QuestionType? questionType = default(QuestionType?), List<string> values = default(List<string>))
        {
            this.Id = id;
            this.QuestionText = questionText;
            this.QuestionType = questionType;
            this.Values = values;
        }
        
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or Sets QuestionText
        /// </summary>
        [DataMember(Name="questionText", EmitDefaultValue=false)]
        public string QuestionText { get; set; }


        /// <summary>
        /// Gets or Sets Values
        /// </summary>
        [DataMember(Name="values", EmitDefaultValue=false)]
        public List<string> Values { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class QuestionDto {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  QuestionText: ").Append(QuestionText).Append("\n");
            sb.Append("  QuestionType: ").Append(QuestionType).Append("\n");
            sb.Append("  Values: ").Append(Values).Append("\n");
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
            return this.Equals(input as QuestionDto);
        }

        /// <summary>
        /// Returns true if QuestionDto instances are equal
        /// </summary>
        /// <param name="input">Instance of QuestionDto to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(QuestionDto input)
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
                    this.QuestionText == input.QuestionText ||
                    (this.QuestionText != null &&
                    this.QuestionText.Equals(input.QuestionText))
                ) && 
                (
                    this.QuestionType == input.QuestionType ||
                    (this.QuestionType != null &&
                    this.QuestionType.Equals(input.QuestionType))
                ) && 
                (
                    this.Values == input.Values ||
                    this.Values != null &&
                    input.Values != null &&
                    this.Values.SequenceEqual(input.Values)
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
                if (this.QuestionText != null)
                    hashCode = hashCode * 59 + this.QuestionText.GetHashCode();
                if (this.QuestionType != null)
                    hashCode = hashCode * 59 + this.QuestionType.GetHashCode();
                if (this.Values != null)
                    hashCode = hashCode * 59 + this.Values.GetHashCode();
                return hashCode;
            }
        }
    }
}
