using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace DBManageSystem.FunctionalTests;
public static class HttpClientHelper
{
  public static async Task TestWithoutAuthorize<T>(this HttpClient client,T data,string route,HttpMethod method)
  {
    client.DefaultRequestHeaders.Authorization = null;
    var JsonRequest = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
    HttpResponseMessage result;
    if (method == HttpMethod.Post)
    {
      result = await client.PostAsync(route, JsonRequest);
    }
    else if (method == HttpMethod.Put)
    {
      result = await client.PutAsync(route, JsonRequest);
    }
    else if (method == HttpMethod.Delete)
    {
      result = await client.DeleteAsync(route+"/"+data);
    }
    else if (method == HttpMethod.Get)
    {
      result = await client.GetAsync(route);
    }
    else
    {
      throw new NotImplementedException();
    }
    Assert.Equal(System.Net.HttpStatusCode.Unauthorized, result.StatusCode);
  }


  public static async Task<HttpResponseMessage> TestWithAuthorizeUsingJWT<T>(this HttpClient client, T data, string route, HttpMethod method,string token)
  {
    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token); ;
    var JsonRequest = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
    HttpResponseMessage result;
    if (method == HttpMethod.Post)
    {
      result = await client.PostAsync(route, JsonRequest);
    }
    else if (method == HttpMethod.Put)
    {
      result = await client.PutAsync(route, JsonRequest);
    }
    else if (method == HttpMethod.Delete)
    {
      result = await client.DeleteAsync(route + "/" + data);
    }
    else if (method == HttpMethod.Get)
    {
      result = await client.GetAsync(route);
    }
    else
    {
      throw new NotImplementedException();
    }

    return result;
  }

}
