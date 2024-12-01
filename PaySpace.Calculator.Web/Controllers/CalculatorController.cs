using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using PaySpace.Calculator.Web.Models;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Controllers
{
    public class CalculatorController(ICalculatorHttpService calculatorHttpService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            //var vm = this.GetCalculatorViewModelAsync();

            //return this.View(new CalculatorViewModel
            //{
            //    PostalCodes = new SelectList(vm)
            //});

            var k = await calculatorHttpService.GetPostalCodesAsync();
            return this.View(new CalculatorViewModel { 
            PostalCodes= new SelectList(k, "Calculator", "Code")
            });
            //return this.View(vm);
        }

        public async Task<IActionResult> History()
        {
            return this.View(new CalculatorHistoryViewModel
            {
                CalculatorHistory = await calculatorHttpService.GetHistoryAsync()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Index(CalculateRequestViewModel request)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    await calculatorHttpService.CalculateTaxAsync(new CalculateRequest
                    {
                        PostalCode = request.PostalCode,
                        Income = request.Income
                    });

                    return this.RedirectToAction(nameof(this.History));
                }
                catch (Exception e)
                {
                    this.ModelState.AddModelError(string.Empty, e.Message);
                }
            }

            //var vm = await this.GetCalculatorViewModelAsync(request);
            var vm = new CalculatorViewModel();
            return this.View(vm);
        }

        private async Task<CalculatorViewModel> GetCalculatorViewModelAsync()
        {
            var postalCodes = await calculatorHttpService.GetPostalCodesAsync();

            return new CalculatorViewModel
            {
                PostalCodes = new SelectList(postalCodes),
                //Income = request.Income,
                //PostalCode = request.PostalCode ?? string.Empty
            };
        }

        //private async Task<CalculatorViewModel> GetCalculatorViewModelAsync(CalculateRequestViewModel? request = null)
        //{
        //    var postalCodes = await calculatorHttpService.GetPostalCodesAsync();

        //    return new CalculatorViewModel
        //    {
        //        PostalCodes = new SelectList(postalCodes),
        //        Income = request.Income,
        //        PostalCode = request.PostalCode ?? string.Empty
        //    };
        //}
    }
}