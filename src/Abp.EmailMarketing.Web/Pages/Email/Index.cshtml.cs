using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EmailMarketing.Emailing;
using Abp.EmailMarketing.MySetting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Emailing.Templates;
using Volo.Abp.Settings;
using Volo.Abp.TextTemplating;

namespace Abp.EmailMarketing.Web.Pages.Email
{
    public class IndexModel : EmailMarketingPageModel, ITransientDependency
    {
        private readonly EmailService _emailService;
        public BViewModel model;
        //private readonly MySetting.MySetting _mySetting;
        //private readonly ISettingProvider _settingProvider;

        public IndexModel(EmailService emailService)
        {
            _emailService = emailService;
            //_settingProvider = settingProvider;
        }

        public void OnGet()
        {
            model = new BViewModel();
        }

        public async Task<IActionResult> OnPost()
        {
            AnotherEmailService service = new AnotherEmailService();
            //await _emailService.SendEmailAsync();
            //_mySetting.Define();
            //_settingProvider
            service.Send("Thuba", "beni09082004@gmail.com", "ABP", "<p>Hello</p>");
            return NoContent();
        }


        public class BViewModel
        {
            public string Name { get; set; }
        }
    }
}
