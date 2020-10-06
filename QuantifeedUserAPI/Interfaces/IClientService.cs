using System.Collections.Generic;
using System.Threading.Tasks;
using QuantifeedUserAPI.Models;

namespace QuantifeedUserAPI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// get all clients
        /// </summary>
        /// <returns></returns>
        public Task<List<Client>> GetClients(); 
        
        /// <summary>
        /// For a specified Manager username, retrieve a list of its clients
        /// </summary>
        /// <returns></returns>
        public Task<List<Client>> GetClients(string managerName); 
    }
}