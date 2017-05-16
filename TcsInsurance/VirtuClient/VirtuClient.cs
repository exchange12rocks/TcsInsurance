using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft;
using VirtuClient.Models;

namespace VirtuClient
{
    public class VirtuClient
    {
        protected Uri baseUrl;
        protected AuthenticationResult authenticationResult;
        public VirtuClient(Uri baseUrl, AuthenticationResult authenticationResult = null)
        {
            this.baseUrl = baseUrl;
            this.authenticationResult = authenticationResult;
        }
        public AuthenticationResult GetAuthentication(AuthenticationInputParams parameters)
        {
            RestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest("Authentication_JSON_AppService.axd/Login", Method.POST);
            request.AddJsonBody(parameters);

            IRestResponse<SimpleResult> response = client.Execute<SimpleResult>(request);
            response.Data.ThrowExceptionIfFail();
            return new AuthenticationResult()
            {
                Cookie = response.Cookies.Single(),
            };
        }
        public void Authenticate(AuthenticationInputParams parameters)
        {
            this.authenticationResult = this.GetAuthentication(parameters);
        }


        public GetProductResult[] GetProducts()
        {
            RestClient client = new RestClient(baseUrl);
            IRestRequest request = new RestRequest("ClientCardFeature/product/list.dat?id=b9744a90-3196-c95a-ec62-268caf4ebfbb", Method.GET);
            request.AddCookie(this.authenticationResult.Cookie.Name, this.authenticationResult.Cookie.Value);

            IRestResponse<VirtuRootResult<VirtuResultArray<GetProductResult>>> response = client.Execute<VirtuRootResult<VirtuResultArray<GetProductResult>>>(request);
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            if(response.Data?.d?.IsValid != true)
            {
                throw new FailedResultException();
            }
            return response.Data.d.Result;
        }
    }
}
