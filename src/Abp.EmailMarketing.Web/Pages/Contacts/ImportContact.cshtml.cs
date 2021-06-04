using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EmailMarketing.Contacts;
using Abp.EmailMarketing.GroupContacts;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abp.EmailMarketing.Web.Pages.Contacts
{
    public class ImportContactModel : EmailMarketingPageModel
    {
        private readonly IContactAppService _contactAppService;
        private readonly IGroupRepository _groupRepository;


        public ImportContactModel(IContactAppService contactAppService, IGroupRepository groupRepository)
        {
            _contactAppService = contactAppService;
            _groupRepository = groupRepository;
        }

        public void OnGet()
        {
        }

        public List<ContactDto> ContactDtos = new List<ContactDto>();

        public async Task<IActionResult> OnPostImportAsync()
        {
            List<CreateUpdateContactDto> createUpdateContactDtos = new List<CreateUpdateContactDto>();
            IFormFile excel = Request.Form.Files[0];
            if (excel == null)
            {
                return NoContent();
            }

            using (var workbook = new XLWorkbook(excel.OpenReadStream()))
            {
                var worksheet = workbook.Worksheet("Contact");

                var count = 0;
                foreach (var row in worksheet.Rows())
                {
                    count += 1;
                    if (count > 1) //skip the first row.
                    {
                        if (row.Cell(1).Value.Equals("") || row.Cell(2).Equals("") || row.Cell(3).Equals("")
                            || row.Cell(4).Equals("") || row.Cell(5).Equals(6)) break;
                        ContactDtos.Add(
                            new ContactDto()
                            {
                                //GroupName = row.Cell(1).Value.ToString(),
                                Email = row.Cell(2).Value.ToString(),
                                FirstName = row.Cell(3).Value.ToString(),
                                LastName = row.Cell(4).Value.ToString(),
                                DateOfBirth = row.Cell(5).Value.To<DateTime>(),
                                PhoneNumber = row.Cell(6).Value == null ? "" : row.Cell(6).Value.ToString(),
                                Addition = row.Cell(7).Value == null ? "" : row.Cell(7).Value.ToString(),
                                Status = row.Cell(8).Value.Equals("") ? 0 : row.Cell(8).Value.To<int>()
                            }
                        );
                        createUpdateContactDtos.Add(
                            new CreateUpdateContactDto()
                            {
                                //GroupId = _groupRepository.FindByNameAsync(row.Cell(1).Value.ToString()).Result.Id,
                                Email = row.Cell(2).Value.ToString(),
                                FirstName = row.Cell(3).Value.ToString(),
                                LastName = row.Cell(4).Value.ToString(),
                                DateOfBirth = row.Cell(5).Value.To<DateTime>(),
                                PhoneNumber = row.Cell(6).Value == null ? "" : row.Cell(6).Value.ToString(),
                                Addition = row.Cell(7).Value == null ? "" : row.Cell(7).Value.ToString(),
                                Status = row.Cell(8).Value.Equals("") ? 0 : row.Cell(8).Value.To<int>()
                            }
                        );
                    }
                }

                foreach(CreateUpdateContactDto createUpdateContactDto in createUpdateContactDtos)
                {
                    await _contactAppService.CreateAsync(createUpdateContactDto);
                }

                //View result ajax 
                StringBuilder sb = new StringBuilder();
                sb.Append("<table class='table table-bordered'><tr>");
                sb.Append("<th>Group Contact</th>");
                sb.Append("<th>Email</th>");
                sb.Append("<th>FirstName</th>");
                sb.Append("<th>LastName</th>");
                sb.Append("<th>DateOfBirth</th>");
                sb.Append("<th>PhoneNumber</th>");
                sb.Append("</tr>");
                sb.AppendLine("<tr>");
                foreach(ContactDto contactDto in ContactDtos)
                {
                    //sb.Append("<td>" + contactDto.GroupName + "</td>");
                    sb.Append("<td>" + contactDto.Email + "</td>");
                    sb.Append("<td>" + contactDto.FirstName + "</td>");
                    sb.Append("<td>" + contactDto.LastName + "</td>");
                    sb.Append("<td>" + contactDto.DateOfBirth + "</td>");
                    sb.Append("<td>" + contactDto.PhoneNumber + "</td>");
                    sb.AppendLine("</tr>");
                }
                sb.Append("</table>");

                return this.Content(sb.ToString());
            }
        }
    }
}
