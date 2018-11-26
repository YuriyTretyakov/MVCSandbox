using System.ComponentModel.DataAnnotations;

namespace Whoops.ViewModels.Contacts
{
    public class ContactViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(4096)]
        public string Message { get; set; }
    }
}
