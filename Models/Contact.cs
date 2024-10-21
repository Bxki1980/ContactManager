using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public class Contact
{
    public int ContactId { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [Phone]
    public string Phone { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Category is required.")]
    public int CategoryId { get; set; }

    [ValidateNever]
    public Category Category { get; set; }

    public DateTime DateAdded { get; private set; } = DateTime.Now;

    public string Slug => $"{FirstName}-{LastName}".ToLower().Replace(' ', '-');
}
