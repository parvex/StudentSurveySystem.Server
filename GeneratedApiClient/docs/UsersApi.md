# StudentSurveySystem.ApiClient.Api.UsersApi

All URIs are relative to *https://localhost:5001/api*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ApiUsersAuthenticatePost**](UsersApi.md#apiusersauthenticatepost) | **POST** /api/Users/Authenticate | 
[**ApiUsersIdGet**](UsersApi.md#apiusersidget) | **GET** /api/Users/{id} | 
[**ApiUsersUsosAuthDataGet**](UsersApi.md#apiusersusosauthdataget) | **GET** /api/Users/UsosAuthData | 
[**ApiUsersUsosPinAuthPost**](UsersApi.md#apiusersusospinauthpost) | **POST** /api/Users/UsosPinAuth | 

<a name="apiusersauthenticatepost"></a>
# **ApiUsersAuthenticatePost**
> CurrentUserDto ApiUsersAuthenticatePost (AuthenticateDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class ApiUsersAuthenticatePostExample
    {
        public void main()
        {

            var apiInstance = new UsersApi();
            var body = new AuthenticateDto(); // AuthenticateDto |  (optional) 

            try
            {
                CurrentUserDto result = apiInstance.ApiUsersAuthenticatePost(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling UsersApi.ApiUsersAuthenticatePost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**AuthenticateDto**](AuthenticateDto.md)|  | [optional] 

### Return type

[**CurrentUserDto**](CurrentUserDto.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apiusersidget"></a>
# **ApiUsersIdGet**
> User ApiUsersIdGet (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class ApiUsersIdGetExample
    {
        public void main()
        {

            var apiInstance = new UsersApi();
            var id = 56;  // int? | 

            try
            {
                User result = apiInstance.ApiUsersIdGet(id);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling UsersApi.ApiUsersIdGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int?**|  | 

### Return type

[**User**](User.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apiusersusosauthdataget"></a>
# **ApiUsersUsosAuthDataGet**
> UsosAuthDto ApiUsersUsosAuthDataGet ()



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class ApiUsersUsosAuthDataGetExample
    {
        public void main()
        {

            var apiInstance = new UsersApi();

            try
            {
                UsosAuthDto result = apiInstance.ApiUsersUsosAuthDataGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling UsersApi.ApiUsersUsosAuthDataGet: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**UsosAuthDto**](UsosAuthDto.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apiusersusospinauthpost"></a>
# **ApiUsersUsosPinAuthPost**
> CurrentUserDto ApiUsersUsosPinAuthPost (UsosAuthDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class ApiUsersUsosPinAuthPostExample
    {
        public void main()
        {

            var apiInstance = new UsersApi();
            var body = new UsosAuthDto(); // UsosAuthDto |  (optional) 

            try
            {
                CurrentUserDto result = apiInstance.ApiUsersUsosPinAuthPost(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling UsersApi.ApiUsersUsosPinAuthPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**UsosAuthDto**](UsosAuthDto.md)|  | [optional] 

### Return type

[**CurrentUserDto**](CurrentUserDto.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
