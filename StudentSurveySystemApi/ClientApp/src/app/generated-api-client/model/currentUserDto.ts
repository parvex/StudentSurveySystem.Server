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
import { UserRole } from './userRole';

export interface CurrentUserDto { 
    id?: number;
    firstName?: string;
    lastName?: string;
    username?: string;
    userRole?: UserRole;
    token?: string;
    tokenExpirationDate?: Date;
}