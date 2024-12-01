using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using PaySpace.Calculator.Web.Models;
using PaySpace.Calculator.Web.Services;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Models;
using System.Text.Json;
using System;
using Microsoft.Extensions.Logging;

namespace PaySpace.Calculator.Web.Controllers
{
    public class CalculatorController(ICalculatorHttpService calculatorHttpService, ILogger<CalculatorController> logger) : Controller
    {
        public async Task<IActionResult> Index()
        {
            logger.LogInformation("Index Start");

            var postalCodes = await calculatorHttpService.GetPostalCodesAsync();

            logger.LogInformation("Index-> PostalCodes successfully fetched");

            return this.View(new CalculatorViewModel { 
            PostalCodes= new SelectList(postalCodes, "Calculator", "Code")
            });
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
            logger.LogInformation($"Index Post Save Start | CalculateRequestViewModel: {JsonSerializer.Serialize(request)}");

            if (this.ModelState.IsValid)
            {
                try
                {
                    await calculatorHttpService.CalculateTaxAsync(new CalculateRequest
                    {
                        PostalCode = request.PostalCode,
                        Income = request.Income
                    });

                    logger.LogInformation("Index Post Save End | Calculated successfully, redirecting now.");

                    return this.RedirectToAction(nameof(this.History));
                }
                catch (Exception e)
                {
                    logger.LogError($"Index Post Save End | Some error occurred | {e.Message}");
                    this.ModelState.AddModelError(string.Empty, e.Message);
                }
            }

            logger.LogInformation("Index Post Save End | Some error occurred");

            var vm = await this.GetCalculatorViewModelAsync(request);
            return this.View(vm);
        }

        private async Task<CalculatorViewModel> GetCalculatorViewModelAsync(CalculateRequestViewModel request)
        {
            logger.LogInformation($"GetCalculatorViewModelAsync Start | CalculateRequestViewModel: {JsonSerializer.Serialize(request)}");

            var postalCodes = await calculatorHttpService.GetPostalCodesAsync();

            logger.LogInformation($"GetCalculatorViewModelAsync-> Postal Code successfully fetched");

            return new CalculatorViewModel
            {
                PostalCodes = new SelectList(postalCodes, "Calculator", "Code"),
                Income = request.Income,
                PostalCode = request.PostalCode
            };
        }
    }
}