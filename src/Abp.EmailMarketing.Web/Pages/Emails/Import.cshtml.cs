using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EmailMarketing.Emails;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abp.EmailMarketing.Web.Pages.Emails
{
    public class ImportModel : EmailMarketingPageModel
    {
        private readonly IEmailAppService _emailAppService;
        private readonly EmailManager _emailManager;


        public ImportModel(IEmailAppService emailAppService, EmailManager emailManager)
        {
            _emailAppService = emailAppService;
            _emailManager = emailManager;
        }

        public void OnGet()
        {
        }

        public List<EmailDto> EmailDtos = new List<EmailDto>();

        public async Task<IActionResult> OnPostImportAsync()
        {
            List<CreateUpdateEmailDto> createUpdateContactDtos = new List<CreateUpdateEmailDto>();
            IFormFile excel = Request.Form.Files[0];
            if (excel == null)
            {
                return NoContent();
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<table class='table table-bordered'><tr>");
            sb.Append("<th>Email</th>");
            sb.Append("<th>Password</th>");
            sb.Append("</tr>");
            

            using (var workbook = new XLWorkbook(excel.OpenReadStream()))
            {
                var worksheet = workbook.Worksheet("Email");

                var count = 0;
                foreach (var row in worksheet.Rows())
                {
                    count += 1;
                    if (count > 1) //skip the first row.
                    {
                        if (row.Cell(1).Value.Equals("") || row.Cell(2).Equals("")) break;
                        var createEmailDto = new CreateUpdateEmailDto()
                        {
                            EmailString = row.Cell(1).Value.ToString(),
                            Password = row.Cell(2).Value.ToString(),
                            Order = 0
                        };
                        try
                        {
                            await _emailAppService.CreateAsync(
                                createEmailDto
                            );
                            sb.AppendLine("<tr>");
                            sb.Append("<td>" + createEmailDto.EmailString + "</td>");
                            sb.Append("<td>" + createEmailDto.Password + "</td>");
                            sb.AppendLine("</tr>");
                        }
                        catch (Exception ex)
                        {
                            sb.AppendLine($"<tr class='table-danger'>");
                            sb.Append("<td>" + createEmailDto.EmailString + "</td>");
                            sb.Append("<td>" + createEmailDto.Password + "</td>");
                            sb.AppendLine("</tr>");
                        }
                    }
                }

                //View result ajax 
                sb.Append("</table>");

                return this.Content(sb.ToString());
            }
        }
    }
}
