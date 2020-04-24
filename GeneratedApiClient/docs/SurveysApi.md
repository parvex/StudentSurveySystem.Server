# StudentSurveySystem.ApiClient.Api.SurveysApi

All URIs are relative to *https://localhost:5001/api*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ActivateIdPost**](SurveysApi.md#activateidpost) | **POST** /activate/{id} | 
[**ApiSurveysGet**](SurveysApi.md#apisurveysget) | **GET** /api/Surveys | 
[**ApiSurveysIdDelete**](SurveysApi.md#apisurveysiddelete) | **DELETE** /api/Surveys/{id} | 
[**ApiSurveysIdGet**](SurveysApi.md#apisurveysidget) | **GET** /api/Surveys/{id} | 
[**ApiSurveysIdPut**](SurveysApi.md#apisurveysidput) | **PUT** /api/Surveys/{id} | 
[**ApiSurveysMyNotFilledFormGet**](SurveysApi.md#apisurveysmynotfilledformget) | **GET** /api/Surveys/MyNotFilledForm | 
[**ApiSurveysMySurveysGet**](SurveysApi.md#apisurveysmysurveysget) | **GET** /api/Surveys/MySurveys | 
[**ApiSurveysPost**](SurveysApi.md#apisurveyspost) | **POST** /api/Surveys | 
[**ApiSurveysPut**](SurveysApi.md#apisurveysput) | **PUT** /api/Surveys | 
[**DeactivateIdPost**](SurveysApi.md#deactivateidpost) | **POST** /deactivate/{id} | 

<a name="activateidpost"></a>
# **ActivateIdPost**
> void ActivateIdPost (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class ActivateIdPostExample
    {
        public void main()
        {

            var apiInstance = new SurveysApi();
            var id = 56;  // int? | 

            try
            {
                apiInstance.ActivateIdPost(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SurveysApi.ActivateIdPost: " + e.Message );
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

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apisurveysget"></a>
# **ApiSurveysGet**
> List<SurveyDto> ApiSurveysGet (string name = null, int? page = null, int? count = null)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class ApiSurveysGetExample
    {
        public void main()
        {

            var apiInstance = new SurveysApi();
            var name = name_example;  // string |  (optional) 
            var page = 56;  // int? |  (optional)  (default to 0)
            var count = 56;  // int? |  (optional)  (default to 20)

            try
            {
                List&lt;SurveyDto&gt; result = apiInstance.ApiSurveysGet(name, page, count);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SurveysApi.ApiSurveysGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **name** | **string**|  | [optional] 
 **page** | **int?**|  | [optional] [default to 0]
 **count** | **int?**|  | [optional] [default to 20]

### Return type

[**List<SurveyDto>**](SurveyDto.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apisurveysiddelete"></a>
# **ApiSurveysIdDelete**
> void ApiSurveysIdDelete (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class ApiSurveysIdDeleteExample
    {
        public void main()
        {

            var apiInstance = new SurveysApi();
            var id = 56;  // int? | 

            try
            {
                apiInstance.ApiSurveysIdDelete(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SurveysApi.ApiSurveysIdDelete: " + e.Message );
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

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apisurveysidget"></a>
# **ApiSurveysIdGet**
> SurveyDto ApiSurveysIdGet (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class ApiSurveysIdGetExample
    {
        public void main()
        {

            var apiInstance = new SurveysApi();
            var id = 56;  // int? | 

            try
            {
                SurveyDto result = apiInstance.ApiSurveysIdGet(id);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SurveysApi.ApiSurveysIdGet: " + e.Message );
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

[**SurveyDto**](SurveyDto.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apisurveysidput"></a>
# **ApiSurveysIdPut**
> void ApiSurveysIdPut (int? id, SurveyDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class ApiSurveysIdPutExample
    {
        public void main()
        {

            var apiInstance = new SurveysApi();
            var id = 56;  // int? | 
            var body = new SurveyDto(); // SurveyDto |  (optional) 

            try
            {
                apiInstance.ApiSurveysIdPut(id, body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SurveysApi.ApiSurveysIdPut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int?**|  | 
 **body** | [**SurveyDto**](SurveyDto.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apisurveysmynotfilledformget"></a>
# **ApiSurveysMyNotFilledFormGet**
> List<SurveyDto> ApiSurveysMyNotFilledFormGet (string name = null, int? page = null, int? count = null)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class ApiSurveysMyNotFilledFormGetExample
    {
        public void main()
        {

            var apiInstance = new SurveysApi();
            var name = name_example;  // string |  (optional) 
            var page = 56;  // int? |  (optional)  (default to 0)
            var count = 56;  // int? |  (optional)  (default to 20)

            try
            {
                List&lt;SurveyDto&gt; result = apiInstance.ApiSurveysMyNotFilledFormGet(name, page, count);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SurveysApi.ApiSurveysMyNotFilledFormGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **name** | **string**|  | [optional] 
 **page** | **int?**|  | [optional] [default to 0]
 **count** | **int?**|  | [optional] [default to 20]

### Return type

[**List<SurveyDto>**](SurveyDto.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apisurveysmysurveysget"></a>
# **ApiSurveysMySurveysGet**
> List<SurveyDto> ApiSurveysMySurveysGet (string name = null, int? page = null, int? count = null)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class ApiSurveysMySurveysGetExample
    {
        public void main()
        {

            var apiInstance = new SurveysApi();
            var name = name_example;  // string |  (optional) 
            var page = 56;  // int? |  (optional)  (default to 0)
            var count = 56;  // int? |  (optional)  (default to 20)

            try
            {
                List&lt;SurveyDto&gt; result = apiInstance.ApiSurveysMySurveysGet(name, page, count);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SurveysApi.ApiSurveysMySurveysGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **name** | **string**|  | [optional] 
 **page** | **int?**|  | [optional] [default to 0]
 **count** | **int?**|  | [optional] [default to 20]

### Return type

[**List<SurveyDto>**](SurveyDto.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apisurveyspost"></a>
# **ApiSurveysPost**
> SurveyDto ApiSurveysPost (SurveyDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class ApiSurveysPostExample
    {
        public void main()
        {

            var apiInstance = new SurveysApi();
            var body = new SurveyDto(); // SurveyDto |  (optional) 

            try
            {
                SurveyDto result = apiInstance.ApiSurveysPost(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SurveysApi.ApiSurveysPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**SurveyDto**](SurveyDto.md)|  | [optional] 

### Return type

[**SurveyDto**](SurveyDto.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apisurveysput"></a>
# **ApiSurveysPut**
> SurveyDto ApiSurveysPut (SurveyDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class ApiSurveysPutExample
    {
        public void main()
        {

            var apiInstance = new SurveysApi();
            var body = new SurveyDto(); // SurveyDto |  (optional) 

            try
            {
                SurveyDto result = apiInstance.ApiSurveysPut(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SurveysApi.ApiSurveysPut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**SurveyDto**](SurveyDto.md)|  | [optional] 

### Return type

[**SurveyDto**](SurveyDto.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="deactivateidpost"></a>
# **DeactivateIdPost**
> void DeactivateIdPost (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class DeactivateIdPostExample
    {
        public void main()
        {

            var apiInstance = new SurveysApi();
            var id = 56;  // int? | 

            try
            {
                apiInstance.DeactivateIdPost(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SurveysApi.DeactivateIdPost: " + e.Message );
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

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
