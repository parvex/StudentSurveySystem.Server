# StudentSurveySystem.ApiClient.Api.SurveyResponsesApi

All URIs are relative to *https://localhost:5001*

Method | HTTP request | Description
------------- | ------------- | -------------
[**SurveyResponsesGet**](SurveyResponsesApi.md#surveyresponsesget) | **GET** /SurveyResponses | 
[**SurveyResponsesIdGet**](SurveyResponsesApi.md#surveyresponsesidget) | **GET** /SurveyResponses/{id} | 
[**SurveyResponsesMyCompletedGet**](SurveyResponsesApi.md#surveyresponsesmycompletedget) | **GET** /SurveyResponses/MyCompleted | 
[**SurveyResponsesPost**](SurveyResponsesApi.md#surveyresponsespost) | **POST** /SurveyResponses | 

<a name="surveyresponsesget"></a>
# **SurveyResponsesGet**
> List<SurveyResponseDetailsDto> SurveyResponsesGet (string name = null, int? page = null, int? count = null)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class SurveyResponsesGetExample
    {
        public void main()
        {

            var apiInstance = new SurveyResponsesApi();
            var name = name_example;  // string |  (optional) 
            var page = 56;  // int? |  (optional)  (default to 0)
            var count = 56;  // int? |  (optional)  (default to 20)

            try
            {
                List&lt;SurveyResponseDetailsDto&gt; result = apiInstance.SurveyResponsesGet(name, page, count);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SurveyResponsesApi.SurveyResponsesGet: " + e.Message );
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
<a name="surveyresponsesidget"></a>
# **SurveyResponsesIdGet**
> SurveyResponseDto SurveyResponsesIdGet (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class SurveyResponsesIdGetExample
    {
        public void main()
        {

            var apiInstance = new SurveyResponsesApi();
            var id = 56;  // int? | 

            try
            {
                SurveyResponseDto result = apiInstance.SurveyResponsesIdGet(id);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SurveyResponsesApi.SurveyResponsesIdGet: " + e.Message );
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
<a name="surveyresponsesmycompletedget"></a>
# **SurveyResponsesMyCompletedGet**
> List<SurveyResponseDetailsDto> SurveyResponsesMyCompletedGet (string name = null, int? page = null, int? count = null)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class SurveyResponsesMyCompletedGetExample
    {
        public void main()
        {

            var apiInstance = new SurveyResponsesApi();
            var name = name_example;  // string |  (optional) 
            var page = 56;  // int? |  (optional)  (default to 0)
            var count = 56;  // int? |  (optional)  (default to 20)

            try
            {
                List&lt;SurveyResponseDetailsDto&gt; result = apiInstance.SurveyResponsesMyCompletedGet(name, page, count);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SurveyResponsesApi.SurveyResponsesMyCompletedGet: " + e.Message );
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
<a name="surveyresponsespost"></a>
# **SurveyResponsesPost**
> SurveyResponseDto SurveyResponsesPost (SurveyResponseDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using StudentSurveySystem.ApiClient.Api;
using StudentSurveySystem.ApiClient.Client;
using StudentSurveySystem.ApiClient.Model;

namespace Example
{
    public class SurveyResponsesPostExample
    {
        public void main()
        {

            var apiInstance = new SurveyResponsesApi();
            var body = new SurveyResponseDto(); // SurveyResponseDto |  (optional) 

            try
            {
                SurveyResponseDto result = apiInstance.SurveyResponsesPost(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SurveyResponsesApi.SurveyResponsesPost: " + e.Message );
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
