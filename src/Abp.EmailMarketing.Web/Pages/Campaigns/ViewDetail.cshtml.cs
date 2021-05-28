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
    public class ViewDetailModel : EmailMarketingPageModel
    {

        [BindProperty]
        public ViewDetailCampaignViewModel Campaign { get; set; }

        private readonly ICampaignAppService _campaignAppService;

        public ViewDetailModel(ICampaignAppService campaignAppService)
        {
            _campaignAppService = campaignAppService;
        }


        public async void OnGet(Guid Id)
        {
            var campaignDto = await _campaignAppService.GetAsync(Id);
            Campaign = ObjectMapper.Map<CampaignDto, ViewDetailCampaignViewModel>(campaignDto);
        }

        public class ViewDetailCampaignViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }
            [Required]
            [StringLength(100)]
            [ReadOnlyInput]
            public string Name { get; set; }
            [TextArea]
            [ReadOnlyInput]
            public string Description { get; set; }
            [Required]
            [StringLength(100)]
            [ReadOnlyInput]
            public string Title { get; set; }
            [TextArea]
            [ReadOnlyInput]
            public string Content { get; set; }
            [Required]
            [DataType(DataType.DateTime)]
            [ReadOnlyInput]
            public DateTime Schedule { get; set; } = DateTime.Now;
        }
    }
}
