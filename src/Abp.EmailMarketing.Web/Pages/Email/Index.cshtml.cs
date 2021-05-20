using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EmailMarketing.Emailing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Emailing.Templates;
using Volo.Abp.TextTemplating;

namespace Abp.EmailMarketing.Web.Pages.Email
{
    public class IndexModel : EmailMarketingPageModel, ITransientDependency
    {
        private readonly EmailService _emailService;
        public BViewModel model;

        public IndexModel(EmailService emailService)
        {
            _emailService = emailService;
        }

        public void OnGet()
        {
            model = new BViewModel();
        }

        public async Task<IActionResult> OnPost()
        {
            await _emailService.SendEmailAsync();

            return NoContent();
        }


        public class BViewModel
        {
            public string Name { get; set; }
        }
    }
}
