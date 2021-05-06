using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EmailMarketing.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abp.EmailMarketing.Web.Pages.Contacts
{
    public class EditModalModel : EmailMarketingPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateContactDto Contact { get; set; }

        private readonly IContactAppService _contactAppService;

        public EditModalModel(IContactAppService contactAppService)
        {
            _contactAppService = contactAppService;
        }

        public async Task OnGetAsync()
        {
            var contactDto = await _contactAppService.GetAsync(Id);
            Contact = ObjectMapper.Map<ContactDto, CreateUpdateContactDto>(contactDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _contactAppService.UpdateAsync(Id, Contact);
            return NoContent();
        }
    }
}
