using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Abp.EmailMarketing.Emails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abp.EmailMarketing.Web.Pages.Emails
{
    public class CreateModalModel : EmailMarketingPageModel
    {
        [BindProperty]
        public CreateEmailViewModel Email { get; set; }

        private readonly IEmailAppService _emailAppService;

        public CreateModalModel(IEmailAppService emailAppService)
        {
            _emailAppService = emailAppService;
        }

        public void OnGet()
        {
            Email = new CreateEmailViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEmailViewModel, CreateUpdateEmailDto>(Email);
            await _emailAppService.CreateAsync(dto);
            return NoContent();
        }

        public class CreateEmailViewModel
        {
            [Required]
            [StringLength(EmailConsts.MaxEmailStringLength)]
            [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
            public string EmailString { get; set; }
            [Required]
            public string Password { get; set; }
        }
    }
}
