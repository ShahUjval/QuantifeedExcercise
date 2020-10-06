using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuantifeedUserAPI.Models
{
    /// <summary>
    /// User Model for the Quantifeed System
    /// </summary>
    public class User
    {
        /// <summary>
        /// User Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        
        /// <summary>
        /// User Name
        /// </summary>
        [Required(ErrorMessage = "User Name is required")]
        [StringLength(200, ErrorMessage = "Name can't be longer than 200 characters")]
        public string UserName { get; set; }
        
        /// <summary>
        /// Email
        /// </summary>
        [StringLength(1000, ErrorMessage = "Email can't be longer than 1000 characters")]
        public string Email { get; set; }
        
        /// <summary>
        /// Alias
        /// </summary>
        [StringLength(1000, ErrorMessage = "Alias can't be longer than 1000 characters")]
        public string Alias { get; set; }
        
        /// <summary>
        /// First Name
        /// </summary>
        [StringLength(1000, ErrorMessage = "First Name can't be longer than 1000 characters")]
        public string FirstName { get; set; }
        
        /// <summary>
        /// Last Name
        /// </summary>
        [StringLength(1000, ErrorMessage = "Last Name can't be longer than 1000 characters")]
        public string LastName { get; set; }
    }
}
