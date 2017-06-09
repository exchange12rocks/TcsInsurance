using System;
using System.Xml;
using tinkoff.ru.partners.insurance.investing.types;
namespace TinkoffService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service2" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service2.svc or Service2.svc.cs at the Solution Explorer and start debugging.
    public class Service2 : InvestingInsuranceInterface
    {
        public acceptPolicyResponse1 acceptPolicy(acceptPolicyRequest1 request)
        {
            throw new NotImplementedException();
            /*XmlDocument doc = new XmlDocument();
            var node1 = doc.CreateNode(XmlNodeType.Element, "", null);
            node1.Value = "errorCode";
            var node2 = doc.CreateNode(XmlNodeType.Element, "", null);
            node2.Value = "errorMessage ";
            throw new CommonFault()
            {
                Nodes = new XmlNode[]
                {
                    node1,
                    node2,
                },
            };*/
        }

        public createPolicyResponse1 createPolicy(createPolicyRequest1 request)
        {
            throw new NotImplementedException();
        }

        public getPolicyResponse1 getPolicy(getPolicyRequest1 request)
        {
            throw new NotImplementedException();
        }

        public getPolicyDocumentResponse1 getPolicyDocument(getPolicyDocumentRequest1 request)
        {
            throw new NotImplementedException();
        }

        public getPolicyDocumentsListResponse getPolicyDocumentsList(getPolicyDocumentsListRequest1 request)
        {
            throw new NotImplementedException();
        }

        public getProductsResponse getProducts(getProductsRequest1 request)
        {
            throw new NotImplementedException();
        }

        public getQuotesResponse1 getQuotes(getQuotesRequest1 request)
        {
            throw new NotImplementedException();
        }
    }
}
