using Abp.EmailMarketing.GroupContacts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Abp.EmailMarketing.Contacts
{
    public class CreateUpdateContactDto
    {
        [Required]
        public List<Guid> GroupIds { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; } = DateTime.Now;
        [DataType(DataType.PhoneNumber)]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "The phone number's length must be between 10 and 15")]
        public string PhoneNumber { get; set; }
        public string Addition { get; set; }
        public int Status { get; set; }
        [Required]
        public ContactType Type { get; set; } = ContactType.Group01;

    }
}
