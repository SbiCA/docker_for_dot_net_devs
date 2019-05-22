using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Home.Models;

namespace Home.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(OrderForm form)
        {
            using (var dbContext = new ContainerContext())
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Email == form.Email);
                if (customer == null)
                {
                    customer = new Customer()
                    {
                        Email = form.Email
                    };
                    dbContext.Customers.Add(customer);
                    await dbContext.SaveChangesAsync();
                }

                // TODO use dbcontext store order
                var order = new Order
                {
                    CustomerId = customer.Id,
                    NumberOfContainers = form.NumberOfUnits
                };
                dbContext.Orders.Add(order);
                await dbContext.SaveChangesAsync();

                // TODO produce containers

                for (var i = 0; i < form.NumberOfUnits; i++)
                {
                    var container = new Container
                    {
                        OrderId = order.Id,
                        ProductionDate = DateTime.UtcNow
                    };
                    await Task.Delay(500);
                    dbContext.Conatiners.Add(container);
                }
                await dbContext.SaveChangesAsync();
                // TODO send out confirmation email
            }
            return RedirectToAction("ThankYou");
        }

        [HttpGet]
        public ActionResult ThankYou()
        {
            return View();
        }
    }
}