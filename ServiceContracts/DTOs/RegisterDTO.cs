using E_Bus.Entities.Entities;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;


namespace ServiceContracts.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [RegularExpression("[A-Za-z]*")]
        public string? FirstName { get; set; }
        [Required]
        [RegularExpression("[A-Za-z]*")]
        public string? LastName { get; set;}
        [Required]
        [StringLength(14, ErrorMessage ="NationalID is 14 digits.")]
        [RegularExpression("^[2-3]{1}[0-9]{13}$", ErrorMessage = "Not a valid NationalID.")]
        [Remote(action: "IsValidNationalID", controller: "Account", ErrorMessage = "NationalID is used before.")]
        public string? NationalID { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Not valid Email address.")]
        [Display(Name = "Email")]
        public string? EmailAddress { get; set;}
        [Required]
        [StringLength(8, ErrorMessage = "Password must be 8 characters.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        [StringLength(8, ErrorMessage = "Password must be 8 characters.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(11, ErrorMessage ="Not a valid PhoneNumber.")]
        [RegularExpression("^01[0-9]{9}$" , ErrorMessage = "Not a valid PhoneNumber.")]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Role")]
        public UserType UserRole { get; set; }


        public ApplicationUser ToApplicationUser()
        {
            return new ApplicationUser()
            {
                Email = EmailAddress,
                FirstName = FirstName,
                LastName = LastName,
                NationalID = NationalID,
                UserName = NationalID,
                PhoneNumber = PhoneNumber,
                
            };
        }


    }

    
}
