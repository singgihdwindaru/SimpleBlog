using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace simpleBlog.Ui.Helper
{
    public class HTTPClientHelper
    {
         #region Abstract, Async, static HTTP functions for GET, POST, PUT, DELETE               
        public static async Task<T> GetAsync<T>(string url, Dictionary<string, string> Headers = null, params string[] parameters)
        {
            T data = default;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);
                    if (Headers != null)
                    {
                        foreach (var item in Headers)
                        {
                            client.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }
                    using (HttpResponseMessage response = await client.GetAsync(url))
                    {
                        // The following line will throw HttpRequestException with StatusCode set if it wasn't 2xx.
                        if (response.StatusCode >= HttpStatusCode.BadRequest)
                        {
                            throw new HttpRequestException("Something went wrong", inner: null, response.StatusCode);
                        }
                        using (HttpContent content = response.Content)
                        {
                            string d = await content.ReadAsStringAsync();
                            if (d != null)
                            {
                                data = JsonConvert.DeserializeObject<T>(d);
                                return (T)data;
                            }
                        }
                    }
                }
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Handle 404
                // Console.WriteLine("Not found: " + ex.Message);
            }
            // Filter by InnerException.
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                // Handle timeout.
                // Console.WriteLine("Timed out: " + ex.Message);
            }
            catch (TaskCanceledException )
            {
                // Handle cancellation.
                // Console.WriteLine("Canceled: " + ex.Message);
            }
            Object o = new Object();
            return (T)o;
        }

        public static async Task<T> PostAsync<T>(string url,
                                                 HttpContent contentPost,
                                                 Dictionary<string, string> Headers = null, params string[] parameters)
        {
            T responseModel = default;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // client.Timeout = TimeSpan.FromSeconds(30);
                    if (Headers != null)
                    {
                        foreach (var item in Headers)
                        {
                            client.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }
                    using (HttpResponseMessage response = await client.PostAsync(url, contentPost))
                    {
                        // The following line will throw HttpRequestException with StatusCode set if it wasn't 2xx.
                        if (response.StatusCode >= HttpStatusCode.BadRequest)
                        {
                            throw new HttpRequestException("Something went wrong", inner: null, response.StatusCode);
                        }
                        using (HttpContent content = response.Content)
                        {
                            string d = await content.ReadAsStringAsync();
                            if (d != null)
                            {
                                responseModel = JsonConvert.DeserializeObject<T>(d);
                                return (T)responseModel;
                            }
                        }
                    }
                }
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Handle 404
                // Console.WriteLine("Not found: " + ex.Message);
            }
            // Filter by InnerException.
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                // Handle timeout.
                // Console.WriteLine("Timed out: " + ex.Message);
            }
            catch (TaskCanceledException)
            {
                // Handle cancellation.
                // Console.WriteLine("Canceled: " + ex.Message);
            }

            Object o = new Object();
            return (T)o;
        }

        public static async Task<T> PutAsync<T>(string url,
                                                HttpContent contentPut,
                                                Dictionary<string, string> Headers = null, params string[] parameters)
        {
            T data;

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                if (Headers != null)
                {
                    foreach (var item in Headers)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                using (HttpResponseMessage response = await client.PutAsync(url,
                                                                            contentPut))
                using (HttpContent content = response.Content)
                {
                    string d = await content.ReadAsStringAsync();
                    if (d != null)
                    {
                        data = JsonConvert.DeserializeObject<T>(d);
                        return (T)data;
                    }
                }
            }
            Object o = new Object();
            return (T)o;
        }

        public static async Task<T> DeleteAsync<T>(string url, Dictionary<string, string> Headers = null, params string[] parameters)
        {
            T newT;

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                if (Headers != null)
                {
                    foreach (var item in Headers)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                using (HttpResponseMessage response = await client.DeleteAsync(url))
                using (HttpContent content = response.Content)
                {
                    string data = await content.ReadAsStringAsync();
                    if (data != null)
                    {
                        newT = JsonConvert.DeserializeObject<T>(data);
                        return newT;
                    }
                }
            }
            Object o = new Object();
            return (T)o;
        }

        #endregion
    }
}
