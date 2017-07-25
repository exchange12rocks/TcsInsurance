using System;
using System.Web.Services;
using tinkoff.ru.partners.insurance.investing.types;
namespace TinkoffService
{
    /// <summary>
    /// Summary description for Test
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Test : System.Web.Services.WebService
    {
        [WebMethod]
        public string tm()
        {
            try
            {
                Insurance service = new Insurance();
                var result = service.getPolicy(new getPolicyRequest1()
                {
                    GetPolicyRequest = new GetPolicyRequest()
                    {
                        policyId = "F0710553-788D-4DB8-8660-133A53E22BE2",
                    }
                });
                return result.GetPolicyResponse.profitability.ToString();
            }
            catch(Exception exception)
            {
                return exception.ToString();
            }
        }
    }
}
