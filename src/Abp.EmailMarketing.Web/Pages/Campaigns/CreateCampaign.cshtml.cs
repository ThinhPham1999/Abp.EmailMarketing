using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Abp.EmailMarketing.Campaigns;
using Abp.EmailMarketing.Contacts;
using Abp.EmailMarketing.GroupContacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Abp.EmailMarketing.Web.Pages.Campaigns
{
    public class CreateCampaignModel : EmailMarketingPageModel
    {
        [BindProperty]
        public CreateCampaignViewModel Campaign { get; set; }
        public List<SelectListItem> Groups { get; set; }

        private readonly ICampaignAppService _campaignAppService;
        private readonly IContactAppService _contactAppService;

        public CreateCampaignModel(ICampaignAppService campaignAppService, IContactAppService contactAppService)
        {
            _campaignAppService = campaignAppService;
            _contactAppService = contactAppService;
        }

        public async void OnGetAsync()
        {
            var groupLookup = await _contactAppService.GetGroupLookupAsync();
            Groups = groupLookup.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            Campaign = new CreateCampaignViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateCampaignViewModel, CreateUpdateCampaignDto>(Campaign);
            await _campaignAppService.CreateAsync(dto);
            return NoContent();
        }

        public class CreateCampaignViewModel
        {
            [Required]
            [StringLength(100)]
            public string Name { get; set; }
            [TextArea]
            public string Description { get; set; }
            [Required]
            [StringLength(100)]
            public string Title { get; set; }
            [TextArea]
            public string Content { get; set; }
            [Required]
            [DataType(DataType.DateTime)]
            public DateTime Schedule { get; set; } = DateTime.Now;

            [SelectItems(nameof(Groups))]
            [DisplayName("Group Contact")]
            public List<Guid> GroupId { get; set; }
        }
    }
}
