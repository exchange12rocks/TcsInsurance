using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using VirtuClient.Models;
using VirtuClient.Models.Core;
namespace VirtuClient
{
    public class VirtuClient
    {
        protected VirtuMapper Mapper { get; set; }
        protected Uri BaseUrl { get; set; }
        protected AuthenticationResult AuthenticationResult { get; set; }
        public VirtuClient(Uri baseUrl, VirtuMapper mapper, AuthenticationResult authenticationResult = null)
        {
            this.Mapper = mapper;
            this.BaseUrl = baseUrl;
            this.AuthenticationResult = authenticationResult;
        }
        protected RestClient getClient()
        {
            return new RestClient(BaseUrl);
        }
        protected IRestRequest getNewRequest(string resource, Method method)
        {
            IRestRequest result = new RestRequest(resource, method);
            if (this.AuthenticationResult?.Cookie != null)
            {
                result.AddCookie(this.AuthenticationResult.Cookie.Name, this.AuthenticationResult.Cookie.Value);
            }
            return result;
        }
        protected IRestResponse execute(IRestRequest request)
        {
            return this.getClient().Execute(request);
        }

        private T deserializeContent<T>(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new VirtuResponceException("content is null or empty");
            }
            return JsonConvert.DeserializeObject<T>(content);
        }
        private string getContent(IRestResponse response)
        {
            if (response == null)
            {
                throw new VirtuResponceException("responce is null");
            }
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new VirtuResponceException($"ResponseStatus: {response.ResponseStatus} is not {ResponseStatus.Completed}");
            }
            if (response.ErrorException != null)
            {
                throw new VirtuResponceException($"ErrorException is not null", response.ErrorException);
            }
            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                throw new VirtuResponceException($"ErrorMessage: {response.ErrorMessage}");
            }
            return response.Content;
        }
        protected T GetResult<T>(VirtuResult<T> result)
        {
            if (result == null)
            {
                throw new VirtuResponceException("result: null");
            }
            if (!result.IsValid || result.Errors?.Any() != false)
            {
                throw new VirtuResponceException($"IsValid: {result.IsValid}, Errors: {string.Join(", ", result.Errors ?? new string[0])}");
            }
            return result.Result;
        }
        protected T GetResult<T>(string content)
        {
            return this.GetResult(this.deserializeContent<VirtuRootResult<VirtuResult<T>>>(content)?.d);
        }
        protected T GetResult<T>(IRestResponse response)
        {
            return this.GetResult<T>(this.getContent(response));
        }
        public AuthenticationResult GetAuthentication(AuthenticationInput parameters)
        {
            IRestRequest request = this.getNewRequest("Authentication_JSON_AppService.axd/Login", Method.POST);
            request.AddJsonBody(parameters);
            IRestResponse response = this.execute(request);
            bool? success = this.deserializeContent<VirtuRootResult<bool>>(this.getContent(response))?.d;
            if(success != true)
            {
                throw new VirtuResponceException($"success: {success?.ToString() ?? "null"}");
            }
            return new AuthenticationResult()
            {
                Cookie = response.Cookies.Single(),
            };
        }
        public void Authenticate(AuthenticationInput parameters)
        {
            this.AuthenticationResult = this.GetAuthentication(parameters);
        }

        public GetProductResult[] GetProducts()
        {
            IRestRequest request = this.getNewRequest("ClientCardFeature/product/list.dat?id=b9744a90-3196-c95a-ec62-268caf4ebfbb", Method.GET);
            IRestResponse response = this.execute(request);
            return this.Mapper.Map<GetProductOutput[], GetProductResult[]>(this.GetResult<GetProductOutput[]>(response));
        }
    }
}
