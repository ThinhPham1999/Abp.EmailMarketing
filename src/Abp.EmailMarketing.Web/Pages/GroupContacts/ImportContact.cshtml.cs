using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EmailMarketing.Contacts;
using Abp.EmailMarketing.GroupContacts;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abp.EmailMarketing.Web.Pages.GroupContacts
{
    public class ImportContactModel : EmailMarketingPageModel
    {
        public Guid Id { get; set; }

        private readonly IContactAppService _contactAppService;
        private readonly IGroupRepository _groupRepository;

        public ImportContactModel(IContactAppService contactAppService, IGroupRepository groupRepository)
        {
            _contactAppService = contactAppService;
            _groupRepository = groupRepository;
        }

        public void OnGet(string id)
        {
            Id = Guid.Parse(id);
        }

        public async Task<IActionResult> OnPostImportAsync()
        {
            List<CreateUpdateContactDto> createUpdateContactDtos = new List<CreateUpdateContactDto>();
            IFormFile excel = Request.Form.Files[0];
            if (excel == null)
            {
                return NoContent();
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<table class='table table-bordered table-hover'><tr>");
            sb.Append("<th>Email</th>");
            sb.Append("<th>First Name</th>");
            sb.Append("<th>Last Name</th>");
            sb.Append("<th>Date of birth</th>");
            sb.Append("<th>PhoneNumber</th>");
            sb.Append("<th>Addition</th>");
            sb.Append("</tr>");


            using (var workbook = new XLWorkbook(excel.OpenReadStream()))
            {
                var worksheet = workbook.Worksheet("Contact");
                var count = 0;
                foreach (var row in worksheet.Rows())
                {
                    count += 1;
                    if (count > 1) //skip the first row.
                    {
                        if (row.Cell(1).Value.Equals("") || row.Cell(2).Value.Equals("")
                           || row.Cell(3).Value.Equals("") || row.Cell(4).Value.Equals("")) break;
                        var groupName = row.Cell(7).Value.ToString();
                        var group = await _groupRepository.FindByNameAsync(groupName);
                        var createContactDto = new CreateUpdateContactDto()
                        {
                            GroupIds = new List<Guid>() { group.Id },
                            Email = row.Cell(1).Value.ToString(),
                            FirstName = row.Cell(2).Value.ToString(),
                            LastName = row.Cell(3).Value.ToString(),
                            DateOfBirth = row.Cell(4).Value.To<DateTime>(),
                            PhoneNumber = row.Cell(5).Value == null ? "" : row.Cell(5).Value.ToString(),
                            Addition = row.Cell(6).Value == null ? "" : row.Cell(6).Value.ToString(),
                            Status = 0
                        };
                        try
                        {
                            await _contactAppService.CreateAsync(
                                createContactDto
                            );
                            sb.AppendLine("<tr>");
                            sb.Append("<td>" + createContactDto.Email + "</td>");
                            sb.Append("<td>" + createContactDto.FirstName + "</td>");
                            sb.Append("<td>" + createContactDto.LastName + "</td>");
                            sb.Append("<td>" + createContactDto.DateOfBirth + "</td>");
                            sb.Append("<td>" + createContactDto.PhoneNumber + "</td>");
                            sb.Append("<td>" + createContactDto.Addition + "</td>");
                            sb.AppendLine("</tr>");
                        }
                        catch (Exception ex)
                        {
                            sb.AppendLine($"<tr class='table-danger'>");
                            sb.Append("<td>" + createContactDto.Email + "</td>");
                            sb.Append("<td>" + createContactDto.FirstName + "</td>");
                            sb.Append("<td>" + createContactDto.LastName + "</td>");
                            sb.Append("<td>" + createContactDto.DateOfBirth + "</td>");
                            sb.Append("<td>" + createContactDto.PhoneNumber + "</td>");
                            sb.Append("<td>" + createContactDto.Addition + "</td>");
                            sb.AppendLine("</tr>");
                            Console.WriteLine(ex);
                        }
                    }
                }

                //View result ajax 
                sb.Append("</table>");

                return this.Content(sb.ToString());
            }
        }

        public async Task<IActionResult> OnPostDownloadAsync()
        {
            /*if (filename == null)
                return Content("filename not present");*/

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot\\File", "ContactDto.xlsx");

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
