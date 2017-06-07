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
        protected Uri BaseUrl { get; set; }
        protected AuthenticationResult AuthenticationResult { get; set; }
        public VirtuClient(Uri baseUrl, AuthenticationResult authenticationResult = null)
        {
            this.BaseUrl = baseUrl;
            this.AuthenticationResult = authenticationResult;
        }
        protected RestClient getClient()
        {
            return new RestClient(this.BaseUrl);
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
        protected T GetSimpleResult<T>(IRestResponse response)
        {
            var content = this.getContent(response);
            var deserializedContent = this.deserializeContent<SimpleResult<SimpleDataResult<T>>>(content)?.d;
            if (deserializedContent == null)
            {
                throw new VirtuResponceException("deserializedContent: null");
            }
            if (!deserializedContent.IsValid || deserializedContent.Errors?.Any() != false)
            {
                throw new VirtuResponceException($"IsValid: {deserializedContent.IsValid}, Errors: {string.Join(", ", deserializedContent.Errors ?? new string[0])}");
            }
            return deserializedContent.Result;
        }
        protected T GetRpcDataResult<T>(IRestResponse response)
        {
            var content = this.getContent(response);
            var deserializedContent = this.deserializeContent<RpcResult<RpcDataResult<T>>[]>(content).SingleOrDefault()?.result;
            if (deserializedContent == null)
            {
                throw new VirtuResponceException($"deserializedContent: null");
            }
            if (deserializedContent.success == false)
            {
                throw new VirtuResponceException($"success: false");
            }
            if (deserializedContent.data == null)
            {
                throw new VirtuResponceException($"data: null");
            }
            return deserializedContent.data;
        }
        protected T GetRpcResult<T>(IRestResponse response)
        {
            var content = this.getContent(response);
            var deserializedContent = this.deserializeContent<RpcResult<T>[]>(content).SingleOrDefault().result;
            if (deserializedContent == null)
            {
                throw new VirtuResponceException($"deserializedContent: null");
            }
            return deserializedContent;
        }
        //
        public AuthenticationResult GetAuthentication(AuthenticationInput parameters)
        {
            IRestRequest request = this.getNewRequest("Authentication_JSON_AppService.axd/Login", Method.POST);
            request.AddJsonBody(parameters);
            IRestResponse response = this.execute(request);
            bool? success = this.deserializeContent<SimpleResult<bool>>(this.getContent(response))?.d;
            if (success != true)
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
        public GetProductOutput[] GetProducts()
        {
            IRestRequest request = this.getNewRequest("/ClientCardFeature/product/list.dat?id=b9744a90-3196-c95a-ec62-268caf4ebfbb", Method.GET);
            IRestResponse response = this.execute(request);
            return this.GetSimpleResult<GetProductOutput[]>(response);
        }
        protected GetClassifierOutput[] GetClassifier(string productId, string classifierId)
        {
            IRestRequest request = this.getNewRequest("/ClassifierFeature/Classifier.dat?id=ed85075a-da0d-cfe0-b5ae-a8fbb168872a", Method.GET);
            request.AddHeader("x-vs-parameters", JsonConvert.SerializeObject(new
            {
                productId = productId,
                id = classifierId,
            }));
            IRestResponse response = this.execute(request);
            return this.GetSimpleResult<GetClassifierOutput[]>(response);
        }
        public GetClassifierOutput[] GetRisks(string productId)
        {
            return this.GetClassifier(productId, "E6BEEA0B-C8FD-4B69-B50E-68DB8D7AEB75");
        }
        public GetClassifierOutput[] GetInsurancePeriods(string productId)
        {
            return this.GetClassifier(productId, "A31BF043-5340-48E2-AB12-430412E9DB82");
        }
        public GetClassifierOutput[] GetDocumentTypes(string productId)
        {
            return this.GetClassifier(productId, "3B07454C-9535-48AF-A344-780D6DED4F77");
        }
        public GetClassifierOutput[] GetInsuranceSums(string productId)
        {
            return this.GetClassifier(productId, "E3F65177-9603-40C5-BD1A-DDFF5DAB5598");
        }
        public GetClassifierOutput[] GetCurrencies(string productId)
        {
            return this.GetClassifier(productId, "63665791-125E-46E7-878B-7E625EA62803");
        }
        public GetClassifierOutput[] GetStrategies(string productId)
        {
            return this.GetClassifier(productId, "12EB0CBE-7332-4878-B6A9-E78BC5767F74");
        }
        public GetClassifierOutput[] GetStatuses(string productId)
        {
            return this.GetClassifier(productId, "471DE2BD-AF38-45D8-BDE6-C1FA2099B88A");
        }
        public GetClassifierOutput[] InsuredDocumentTypes(string productId)
        {
            return this.GetClassifier(productId, "33DB538C-6EDC-4708-8ABE-E90345F5361E");
        }
        public GetTariffOutput[] GetTariffs(string productId, bool decodeClassified = true)
        {
            IRestRequest request = this.getNewRequest("/ClientCardFeature/Product/Tariffs.dat?id=8603db0d-d9c1-c80f-53c3-422774502afd", Method.GET);
            request.AddHeader("x-vs-parameters", JsonConvert.SerializeObject(new
            {
                tariffId = "buyout",
                productId = productId,
                decodeClassified = decodeClassified,
            }));
            IRestResponse response = this.execute(request);
            return this.GetSimpleResult<GetTariffOutput[]>(response);
        }
        public GetPrintformsOutput[] GetPrintforms(string productId)
        {
            IRestRequest request = this.getNewRequest("/ClientCardFeature/product/Views.dat?id=b9744a90-3196-c95a-ec62-268caf4ebfbb", Method.GET);
            request.AddHeader("x-vs-parameters", JsonConvert.SerializeObject(new
            {
                productId = productId,
            }));
            IRestResponse response = this.execute(request);
            return this.GetSimpleResult<GetPrintformsOutput[]>(response);
        }
        public StrategiesSearchDataOutput[] StrategiesSearch(StrategiesSearchInput parameter)
        {
            IRestRequest request = this.getNewRequest("/Companies/UralsibLife/RightDecision/Resources/api.vlib", Method.POST);
            var requestParameter = new RpcInput<StrategiesSearchInput[]>()
            {
                tid = 11,
                action = "RightDecisionDirect",
                method = "StrategiesSearch",
                type = "rpc",
                data = new StrategiesSearchInput[]
                {
                    parameter,
                }
            };
            request.AddJsonBody(requestParameter);
            IRestResponse response = this.execute(request);
            return this.GetRpcDataResult<StrategiesSearchDataOutput[]>(response);
        }
        public CalculateOutput Calculate(CalculateInput parameter)
        {
            IRestRequest request = this.getNewRequest("/Companies/UralsibLife/RightDecision/Resources/api.vlib", Method.POST);
            var requestParameter = new RpcInput<CalculateInput[]>()
            {
                tid = 22,
                action = "RightDecisionDirect",
                method = "Calculate",
                type = "rpc",
                data = new CalculateInput[]
                {
                    parameter,
                }
            };
            request.AddJsonBody(requestParameter);
            IRestResponse response = this.execute(request);
            return this.GetRpcDataResult<CalculateOutput>(response);
        }
        public Policy Read(string policyId)
        {
            IRestRequest request = this.getNewRequest("/Companies/UralsibLife/RightDecision/Resources/api.vlib", Method.POST);
            var requestParameter = new RpcInput<string[]>()
            {
                tid = 21,
                action = "Policy",
                method = "Read",
                type = "rpc",
                data = new string[]
                {
                    policyId,
                }
            };
            request.AddJsonBody(requestParameter);
            IRestResponse response = this.execute(request);
            return this.GetRpcResult<Policy>(response);
        }
        public Policy Save(Policy policy)
        {
            IRestRequest request = this.getNewRequest("/Companies/UralsibLife/RightDecision/Resources/api.vlib", Method.POST);
            var requestParameter = new RpcInput<Policy[]>()
            {
                tid = 18,
                action = "RightDecisionDirect",
                method = "Save",
                type = "rpc",
                data = new Policy[]
                {
                    policy,
                }
            };
            request.AddJsonBody(requestParameter);
            IRestResponse response = this.execute(request);
            return this.GetRpcDataResult<Policy>(response);
        }
        public Policy Accept(Policy policy)
        {
            IRestRequest request = this.getNewRequest("/Companies/UralsibLife/RightDecision/Resources/api.vlib", Method.POST);
            var requestParameter = new RpcInput<Policy[]>()
            {
                tid = 40,
                action = "RightDecisionDirect",
                method = "Accept",
                type = "rpc",
                data = new Policy[]
                {
                    policy,
                }
            };
            request.AddJsonBody(requestParameter);
            IRestResponse response = this.execute(request);
            return this.GetRpcDataResult<Policy>(response);
        }
        public Policy Annulate(Policy policy)
        {
            IRestRequest request = this.getNewRequest("/Companies/UralsibLife/RightDecision/Resources/api.vlib", Method.POST);
            var requestParameter = new RpcInput<Policy[]>()
            {
                tid = 21,
                action = "Policy",
                method = "Annulate",
                type = "rpc",
                data = new Policy[]
                {
                    policy,
                }
            };
            request.AddJsonBody(requestParameter);
            IRestResponse response = this.execute(request);
            return this.GetRpcDataResult<Policy>(response);
        }
        private byte getByte(char text)
        {
            switch(text)
            {
                case '0': return 0;
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
            }
            throw new VirtuResponceException($"text = {text}");
        }
        public byte[] Print(PrintInput parameter)
        {
            IRestRequest request = this.getNewRequest("/JPolicyFeature/GetPolicyPDF.cmd?id=b99699fa-43df-d981-917a-15ce29af79c6", Method.POST);
            request.AddJsonBody(parameter);
            IRestResponse response = this.execute(request);
            var requestResult = this.GetSimpleResult<string>(response);
            if(requestResult.Length % 3 != 0)
            {
                throw new VirtuResponceException($"requestResult.Length = {requestResult.Length}, requestResult.Length % 3 = {requestResult.Length % 3}");
            }
            byte[] result = new byte[requestResult.Length / 3];
            for(int index = 0; index < result.Length; ++index)
            {
                result[index] = (byte)(100 * this.getByte(requestResult[3 * index]) + 10 * this.getByte(requestResult[3 * index + 1]) + 1 * this.getByte(requestResult[3 * index + 2]));
            }
            return result;
        }
    }
}