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
    public class EditModalModel : EmailMarketingPageModel
    {
        [BindProperty]
        public EditEmailViewModel Email { get; set; }

        private readonly IEmailAppService _emailAppService;

        public EditModalModel(IEmailAppService emailAppService)
        {
            _emailAppService = emailAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            var emailDto = await _emailAppService.GetAsync(id);
            Email = ObjectMapper.Map<EmailDto, EditEmailViewModel>(emailDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _emailAppService.UpdateAsync(
                Email.Id,
                ObjectMapper.Map<EditEmailViewModel, CreateUpdateEmailDto>(Email)
            );

            return NoContent();
        }

        public class EditEmailViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [StringLength(EmailConsts.MaxEmailStringLength)]
            [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
            [Display(Name = "Email")]
            public string EmailString { get; set; }
            [Required]
            public string Password { get; set; }
        }
    }
}
