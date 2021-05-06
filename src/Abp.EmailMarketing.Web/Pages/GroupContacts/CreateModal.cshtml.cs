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
    public class CreateModalModel : EmailMarketingPageModel
    {
        [BindProperty]
        public CreateGroupViewModel Group { get; set; }

        private readonly IGroupAppService _groupAppService;

        public CreateModalModel(IGroupAppService groupAppService)
        {
            _groupAppService = groupAppService;
        }

        public void OnGet()
        {
            Group = new CreateGroupViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateGroupViewModel, CreateGroupDto>(Group);
            await _groupAppService.CreateAsync(dto);
            return NoContent();
        }

        public class CreateGroupViewModel
        {
            [Required]
            [StringLength(GroupConsts.MaxNameLength)]
            public string Name { get; set; }

            [TextArea]
            public string Description { get; set; }
        }
    }
}
