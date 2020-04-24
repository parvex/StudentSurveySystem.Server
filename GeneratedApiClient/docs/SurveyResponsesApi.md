# StudentSurveySystem.ApiClient.Api.SurveyResponsesApi

All URIs are relative to *https://localhost:5001/api*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ApiSurveyResponsesGet**](SurveyResponsesApi.md#apisurveyresponsesget) | **GET** /api/SurveyResponses | 
[**ApiSurveyResponsesIdGet**](SurveyResponsesApi.md#apisurveyresponsesidget) | **GET** /api/SurveyResponses/{id} | 
[**ApiSurveyResponsesMyCompletedGet**](SurveyResponsesApi.md#apisurveyresponsesmycompletedget) | **GET** /api/SurveyResponses/MyCompleted | 
[**ApiSurveyResponsesPost**](SurveyResponsesApi.md#apisurveyresponsespost) | **POST** /api/SurveyResponses | 

<a name="apisurveyresponsesget"></a>
# **ApiSurveyResponsesGet**
> List<SurveyResponseDetailsDto> ApiSurveyResponsesGet (string name = null, int? page = null, int? count = null)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class ApiSurveyResponsesGetExample
    {
        public void main()
        {

            var apiInstance = new SurveyResponsesApi();
            var name = name_example;  // string |  (optional) 
            var page = 56;  // int? |  (optional)  (default to 0)
            var count = 56;  // int? |  (optional)  (default to 20)

            try
            {
                List&lt;SurveyResponseDetailsDto&gt; result = apiInstance.ApiSurveyResponsesGet(name, page, count);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SurveyResponsesApi.ApiSurveyResponsesGet: " + e.Message );
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

[**List<SurveyResponseDetailsDto>**](SurveyResponseDetailsDto.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apisurveyresponsesidget"></a>
# **ApiSurveyResponsesIdGet**
> SurveyResponseDto ApiSurveyResponsesIdGet (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class ApiSurveyResponsesIdGetExample
    {
        public void main()
        {

            var apiInstance = new SurveyResponsesApi();
            var id = 56;  // int? | 

            try
            {
                SurveyResponseDto result = apiInstance.ApiSurveyResponsesIdGet(id);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SurveyResponsesApi.ApiSurveyResponsesIdGet: " + e.Message );
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

[**SurveyResponseDto**](SurveyResponseDto.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apisurveyresponsesmycompletedget"></a>
# **ApiSurveyResponsesMyCompletedGet**
> List<SurveyResponseDetailsDto> ApiSurveyResponsesMyCompletedGet (string name = null, int? page = null, int? count = null)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class ApiSurveyResponsesMyCompletedGetExample
    {
        public void main()
        {

            var apiInstance = new SurveyResponsesApi();
            var name = name_example;  // string |  (optional) 
            var page = 56;  // int? |  (optional)  (default to 0)
            var count = 56;  // int? |  (optional)  (default to 20)

            try
            {
                List&lt;SurveyResponseDetailsDto&gt; result = apiInstance.ApiSurveyResponsesMyCompletedGet(name, page, count);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SurveyResponsesApi.ApiSurveyResponsesMyCompletedGet: " + e.Message );
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

[**List<SurveyResponseDetailsDto>**](SurveyResponseDetailsDto.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="apisurveyresponsespost"></a>
# **ApiSurveyResponsesPost**
> SurveyResponseDto ApiSurveyResponsesPost (SurveyResponseDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class ApiSurveyResponsesPostExample
    {
        public void main()
        {

            var apiInstance = new SurveyResponsesApi();
            var body = new SurveyResponseDto(); // SurveyResponseDto |  (optional) 

            try
            {
                SurveyResponseDto result = apiInstance.ApiSurveyResponsesPost(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SurveyResponsesApi.ApiSurveyResponsesPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**SurveyResponseDto**](SurveyResponseDto.md)|  | [optional] 

### Return type

[**SurveyResponseDto**](SurveyResponseDto.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
