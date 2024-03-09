using E_Bus.Entities.Entities;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;


namespace ServiceContracts.DTOs
{
    public class LoginDTO
    {

        [Required(ErrorMessage = "{0} field is required.")]
        [StringLength(14, ErrorMessage ="NationalID is 14 digits.")]
        [RegularExpression("^[2-3]{1}[0-9]{13}$", ErrorMessage = "Not a valid NationalID.")]
        public string? NationalID { get; set; }

        [Required(ErrorMessage = "{0} field is required.")]
        [StringLength(8, ErrorMessage = "Password must be 8 characters.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }
        
    }

    
}
