using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuantifeedUserAPI.Models
{
    /// <summary>
    /// Manager
    /// </summary>
    public class Manager
    {
        /// <summary>
        /// Manager Id
        /// </summary>
        [Key]
        public int ManagerId { get; set; } 
        
        /// <summary>
        /// Position : Junior / Senior
        /// </summary>
        public Position Position { get; set; }
        
        /// <summary>
        /// User Id : FK
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// Manager has List of Clients
        /// </summary>
        public List<Client> Clients { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public enum Position
    {
        /// <summary>
        /// Junior Position
        /// </summary>
        Junior,
        /// <summary>
        /// Senior Position
        /// </summary>
        Senior
    }
}