using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EmailMarketing.Contacts;
using Abp.EmailMarketing.GroupContacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abp.EmailMarketing.Web.Pages.GroupContacts
{
    public class ContactViewModel : EmailMarketingPageModel
    {

        public List<ContactView> ContactDtos { get; set; }
        public List<ContactView> ContactOld { get; set; }
        [BindProperty]
        public GroupId Groupid { get; set; }
        [BindProperty]
        public List<string> ContactSave { get; set; }

        private readonly IGroupAppService _groupAppService;
        private readonly IContactAppService _contactAppService;

        public ContactViewModel(IGroupAppService groupAppService, IContactAppService contactAppService)
        {
            _groupAppService = groupAppService;
            _contactAppService = contactAppService;
        }

        public async Task OnGetAsync(Guid Id)
        {
            var contacts = await _groupAppService.GetListContact();
            ContactDtos = ObjectMapper.Map<List<ContactDto>, List<ContactView>>(contacts);
            var contactOld = await _groupAppService.GetListContactByGroup(Id);
            ContactOld = ObjectMapper.Map<List<ContactDto>, List<ContactView>>(contactOld);
            Groupid = new GroupId();
            Groupid.Id = Id;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            SaveModel save = new SaveModel();
            save.GroupId = Groupid.Id;
            var list = new List<Guid>();
            for (int i = 0; i < ContactSave.Count; i++)
            {
                list.Add(Guid.Parse(ContactSave[i]));
            }
            save.Id = list;
            await _groupAppService.UpdateContactInGroup(
                ObjectMapper.Map<SaveModel, UpdateContactInGroupDto>(save)
            );
            return NoContent();
        }

        public class ContactView
        {
            [HiddenInput]
            public Guid Id { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string DateOfBirth { get; set; }
        }
        public class GroupId
        {
            [HiddenInput]
            public Guid Id { get; set; }
        }

        public class SaveModel
        {
            public List<Guid> Id { get; set; }
            public Guid GroupId { get; set; }
        }
    }
}
