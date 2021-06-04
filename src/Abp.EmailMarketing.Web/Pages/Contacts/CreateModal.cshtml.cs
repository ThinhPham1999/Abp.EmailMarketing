using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Abp.EmailMarketing.Contacts;
using Abp.EmailMarketing.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Abp.EmailMarketing.Web.Pages.Contacts
{
    public class CreateModalModel : EmailMarketingPageModel
    {
        [BindProperty]
        public CreateContactViewModel Contact { get; set; }

        [BindProperty]
        public List<SelectListItem> Groups { get; set; }

        [BindProperty]
        public List<string> Result { get; set; }

        private readonly IContactAppService _contactAppService;

        public CreateModalModel(IContactAppService contactAppService, IContactRepository contactRepository)
        {
            _contactAppService = contactAppService;
        }

        public async Task OnGetAsync()
        {
            Contact = new CreateContactViewModel();

            var groupLookup = await _contactAppService.GetGroupLookupAsync();
            Groups = groupLookup.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            List<Guid> groupid = new List<Guid>();
            for (int i = 0; i < Result.Count; i++)
            {
                groupid.Add(Guid.Parse(Result[i]));
            }

            Contact.GroupIds = groupid;
            
            await _contactAppService.CreateAsync(
                ObjectMapper.Map<CreateContactViewModel, CreateUpdateContactDto>(Contact)    
            );
            return NoContent();
        }

        public class CreateContactViewModel
        {
            public List<Guid> GroupIds { get; set; }

            [Required]
            [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
            public string Email { get; set; }

            [Required]
            [StringLength(64)]
            public string FirstName { get; set; }

            [Required]
            [StringLength(64)]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime DateOfBirth { get; set; } = DateTime.Now;

            [DataType(DataType.PhoneNumber)]
            [StringLength(15, MinimumLength = 10, ErrorMessage = "The phone number's length must be between 10 and 15")]
            public string PhoneNumber { get; set; }

            [TextArea]
            public string Addition { get; set; }

            public int Status { get; set; }
        }
    }
}
