using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp.EmailMarketing.Contacts
{
    public class Contact : AuditedAggregateRoot<Guid>
    {
        /*[Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]*/
        public string Email { get; set; }
        /*[PersonalData]
        [Column(TypeName = "nvarchar(100)")]*/
        public string FirstName { get; set; }
        /*[PersonalData]
        [Column(TypeName = "nvarchar(100)")]*/
        public string LastName { get; set; }
        /*[Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]*/
        public DateTime? DateOfBirth { get; set; }
        /*[StringLength(15, MinimumLength = 10, ErrorMessage = "Phone number length must between 10 and 15")]
        [DataType(DataType.PhoneNumber)]*/
        public string PhoneNumber { get; set; }
        public string Addition { get; set; }
        public int Status { get; set; }
        /*[Required]*/
        public ContactType Type { get; set; }
    }
}
