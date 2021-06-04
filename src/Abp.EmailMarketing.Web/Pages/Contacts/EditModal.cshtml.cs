using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Abp.EmailMarketing.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Abp.EmailMarketing.Web.Pages.Contacts
{
    public class EditModalModel : EmailMarketingPageModel
    {

        [BindProperty]
        public EditContactViewModel Contact { get; set; }

        [BindProperty]
        public List<SelectListItem> Groups { get; set; }

        [BindProperty]
        public List<string> Result { get; set; }
        private readonly IContactAppService _contactAppService;

        public EditModalModel(IContactAppService contactAppService)
        {
            _contactAppService = contactAppService;
        }

        public async Task OnGetAsync(Guid Id)
        {
            var contactDto = await _contactAppService.GetAsync(Id);
            Contact = ObjectMapper.Map<ContactDto, EditContactViewModel>(contactDto);
            var groupLockup = await _contactAppService.GetGroupLookupAsync();
            Groups = groupLockup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
            var groupInContact = await _contactAppService.GetGroupByContactId(Id);
            List<Guid> groupIds = new List<Guid>();
            foreach (var group in groupInContact.Items)
            {
                groupIds.Add(group.Id);
            }
            Contact.GroupIds = groupIds;
            //Contact = ObjectMapper.Map<ContactDto, EditContactViewModel>(contactDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            List<Guid> groupid = new List<Guid>();
            for (int i = 0; i < Result.Count; i++)
            {
                groupid.Add(Guid.Parse(Result[i]));
            }

            Contact.GroupIds = groupid;

            await _contactAppService.UpdateAsync(
                Contact.Id,
                ObjectMapper.Map<EditContactViewModel, CreateUpdateContactDto>(Contact)
            );
            return NoContent();
        }

        public class EditContactViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            public List<Guid> GroupIds { get; set; }

            [Required]
            [ReadOnlyInput]
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
