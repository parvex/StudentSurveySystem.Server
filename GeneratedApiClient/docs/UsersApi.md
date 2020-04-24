# StudentSurveySystem.ApiClient.Api.UsersApi

All URIs are relative to *https://server20200331200409.azurewebsites.net/api*

Method | HTTP request | Description
------------- | ------------- | -------------
[**UsersAuthenticatePost**](UsersApi.md#usersauthenticatepost) | **POST** /Users/Authenticate | 
[**UsersIdGet**](UsersApi.md#usersidget) | **GET** /Users/{id} | 
[**UsersUsosAuthDataGet**](UsersApi.md#usersusosauthdataget) | **GET** /Users/UsosAuthData | 
[**UsersUsosPinAuthPost**](UsersApi.md#usersusospinauthpost) | **POST** /Users/UsosPinAuth | 

<a name="usersauthenticatepost"></a>
# **UsersAuthenticatePost**
> CurrentUserDto UsersAuthenticatePost (AuthenticateDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class UsersAuthenticatePostExample
    {
        public void main()
        {

            var apiInstance = new UsersApi();
            var body = new AuthenticateDto(); // AuthenticateDto |  (optional) 

            try
            {
                CurrentUserDto result = apiInstance.UsersAuthenticatePost(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling UsersApi.UsersAuthenticatePost: " + e.Message );
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
<a name="usersidget"></a>
# **UsersIdGet**
> User UsersIdGet (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class UsersIdGetExample
    {
        public void main()
        {

            var apiInstance = new UsersApi();
            var id = 56;  // int? | 

            try
            {
                User result = apiInstance.UsersIdGet(id);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling UsersApi.UsersIdGet: " + e.Message );
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
<a name="usersusosauthdataget"></a>
# **UsersUsosAuthDataGet**
> UsosAuthDto UsersUsosAuthDataGet ()



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class UsersUsosAuthDataGetExample
    {
        public void main()
        {

            var apiInstance = new UsersApi();

            try
            {
                UsosAuthDto result = apiInstance.UsersUsosAuthDataGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling UsersApi.UsersUsosAuthDataGet: " + e.Message );
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
<a name="usersusospinauthpost"></a>
# **UsersUsosPinAuthPost**
> CurrentUserDto UsersUsosPinAuthPost (UsosAuthDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class UsersUsosPinAuthPostExample
    {
        public void main()
        {

            var apiInstance = new UsersApi();
            var body = new UsosAuthDto(); // UsosAuthDto |  (optional) 

            try
            {
                CurrentUserDto result = apiInstance.UsersUsosPinAuthPost(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling UsersApi.UsersUsosPinAuthPost: " + e.Message );
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
