using System.Threading.Tasks;
using Abp.EmailMarketing.Contacts;
using Abp.EmailMarketing.Web.Pages;
using Microsoft.AspNetCore.Mvc;

namespace Abp.EmailMarketing.Web.Pages.Contacts
{
    public class CreateModalModel : EmailMarketingPageModel
    {
        [BindProperty]
        public CreateUpdateContactDto Contact { get; set; }

        private readonly IContactAppService _contactAppService;

        public CreateModalModel(IContactAppService contactAppService)
        {
            _contactAppService = contactAppService;
        }

        public void OnGet()
        {
            Contact = new CreateUpdateContactDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _contactAppService.CreateAsync(Contact);
            return NoContent();
        }
    }
}
