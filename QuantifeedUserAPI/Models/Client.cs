using System.ComponentModel.DataAnnotations;

namespace QuantifeedUserAPI.Models
{
    /// <summary>
    /// Clients
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Client Id
        /// </summary>
        [Key]
        public int ClientId { get; set; } 
        
        /// <summary>
        /// Level
        /// </summary>
        public int Level { get; set; }
        
        /// <summary>
        /// Usr Id : FK
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// Manager of the Client
        /// </summary>
        public int ManagerId { get; set; } 
        
        /// <summary>
        /// Navigational Property
        /// </summary>
        public Manager Manager { get; set; }
    }
}