using System;
using System.Web.Http;
using VirtuClient;
using VirtuClient.Models;
namespace TcsInsurance.Controllers
{
    public partial class ProductsController : ApiController
    {
        [HttpGet]
        [ActionName("GetProducts")]
        public IHttpActionResult GetProducts()
        {
            var virtuClient = new VirtuClient.VirtuClient(new Uri("https://uralsiblife.virtusystems.ru"), VirtuMapper.Instance);
            virtuClient.Authenticate(new AuthenticationInput()
            {
                userName = "site_integr",
                password = "es4zMJ5JZs",
                createPersistentCookie = true,
            });
            var products = virtuClient.GetProducts();
            return Ok(products);
        }
    }
}
