using System.Collections.Generic;
using System.Threading.Tasks;
using QuantifeedUserAPI.Models;

namespace QuantifeedUserAPI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IManagerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<List<Manager>> GetManagers();
    }
}