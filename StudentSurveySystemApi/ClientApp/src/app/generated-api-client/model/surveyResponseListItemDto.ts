/**
 * Student survey system API
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */

export interface SurveyResponseListItemDto { 
    id?: number;
    respondentId?: number;
    surveyName?: string;
    respondent?: string;
    date?: Date;
    creator?: string;
    courseName?: string;
}