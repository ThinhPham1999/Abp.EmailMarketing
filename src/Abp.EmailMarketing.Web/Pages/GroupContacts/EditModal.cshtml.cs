using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Abp.EmailMarketing.GroupContacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Abp.EmailMarketing.Web.Pages.GroupContacts
{
    public class EditModalModel : EmailMarketingPageModel
    {
        [BindProperty]
        public EditGroupViewModel Group { get; set; }

        private readonly IGroupAppService _groupAppService;

        public EditModalModel(IGroupAppService groupAppService)
        {
            _groupAppService = groupAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            var groupDto = await _groupAppService.GetAsync(id);
            Group = ObjectMapper.Map<GroupDto, EditGroupViewModel>(groupDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _groupAppService.UpdateAsync(
                Group.Id,
                ObjectMapper.Map<EditGroupViewModel, UpdateGroupDto>(Group)
            );

            return NoContent();
        }

        public class EditGroupViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [StringLength(GroupConsts.MaxNameLength)]
            public string Name { get; set; }

            [TextArea]
            public string Description { get; set; }
        }
    }
}
