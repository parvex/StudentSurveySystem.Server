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
    /// CurrentUserDto
    /// </summary>
    [DataContract]
        public partial class CurrentUserDto :  IEquatable<CurrentUserDto>
    {
        /// <summary>
        /// Gets or Sets UserRole
        /// </summary>
        [DataMember(Name="userRole", EmitDefaultValue=false)]
        public UserRole? UserRole { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentUserDto" /> class.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="usosId">usosId.</param>
        /// <param name="firstName">firstName.</param>
        /// <param name="lastName">lastName.</param>
        /// <param name="username">username.</param>
        /// <param name="userRole">userRole.</param>
        /// <param name="token">token.</param>
        public CurrentUserDto(int? id = default(int?), int? usosId = default(int?), string firstName = default(string), string lastName = default(string), string username = default(string), UserRole? userRole = default(UserRole?), string token = default(string))
        {
            this.Id = id;
            this.UsosId = usosId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Username = username;
            this.UserRole = userRole;
            this.Token = token;
        }
        
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or Sets UsosId
        /// </summary>
        [DataMember(Name="usosId", EmitDefaultValue=false)]
        public int? UsosId { get; set; }

        /// <summary>
        /// Gets or Sets FirstName
        /// </summary>
        [DataMember(Name="firstName", EmitDefaultValue=false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets LastName
        /// </summary>
        [DataMember(Name="lastName", EmitDefaultValue=false)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or Sets Username
        /// </summary>
        [DataMember(Name="username", EmitDefaultValue=false)]
        public string Username { get; set; }


        /// <summary>
        /// Gets or Sets Token
        /// </summary>
        [DataMember(Name="token", EmitDefaultValue=false)]
        public string Token { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CurrentUserDto {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  UsosId: ").Append(UsosId).Append("\n");
            sb.Append("  FirstName: ").Append(FirstName).Append("\n");
            sb.Append("  LastName: ").Append(LastName).Append("\n");
            sb.Append("  Username: ").Append(Username).Append("\n");
            sb.Append("  UserRole: ").Append(UserRole).Append("\n");
            sb.Append("  Token: ").Append(Token).Append("\n");
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
            return this.Equals(input as CurrentUserDto);
        }

        /// <summary>
        /// Returns true if CurrentUserDto instances are equal
        /// </summary>
        /// <param name="input">Instance of CurrentUserDto to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CurrentUserDto input)
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
                    this.UsosId == input.UsosId ||
                    (this.UsosId != null &&
                    this.UsosId.Equals(input.UsosId))
                ) && 
                (
                    this.FirstName == input.FirstName ||
                    (this.FirstName != null &&
                    this.FirstName.Equals(input.FirstName))
                ) && 
                (
                    this.LastName == input.LastName ||
                    (this.LastName != null &&
                    this.LastName.Equals(input.LastName))
                ) && 
                (
                    this.Username == input.Username ||
                    (this.Username != null &&
                    this.Username.Equals(input.Username))
                ) && 
                (
                    this.UserRole == input.UserRole ||
                    (this.UserRole != null &&
                    this.UserRole.Equals(input.UserRole))
                ) && 
                (
                    this.Token == input.Token ||
                    (this.Token != null &&
                    this.Token.Equals(input.Token))
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
                if (this.UsosId != null)
                    hashCode = hashCode * 59 + this.UsosId.GetHashCode();
                if (this.FirstName != null)
                    hashCode = hashCode * 59 + this.FirstName.GetHashCode();
                if (this.LastName != null)
                    hashCode = hashCode * 59 + this.LastName.GetHashCode();
                if (this.Username != null)
                    hashCode = hashCode * 59 + this.Username.GetHashCode();
                if (this.UserRole != null)
                    hashCode = hashCode * 59 + this.UserRole.GetHashCode();
                if (this.Token != null)
                    hashCode = hashCode * 59 + this.Token.GetHashCode();
                return hashCode;
            }
        }
    }
}
