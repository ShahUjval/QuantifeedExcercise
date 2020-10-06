using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuantifeedUserAPI.Context;
using QuantifeedUserAPI.Interfaces;
using QuantifeedUserAPI.Models;

namespace QuantifeedUserAPI.Services
{
    /// <summary>
    /// Manager Service
    /// </summary>
    public class ManagerService : IManagerService
    {
        QuantifeedDBContext db;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_db"></param>
        public ManagerService(QuantifeedDBContext _db)
        {
            db = _db;
        }
            
        /// <summary>
        /// get Managers
        /// </summary>
        /// <returns></returns>
        public async Task<List<Manager>> GetManagers()
        {
            if (db != null)
            {
                return await db.Managers.ToListAsync();
            }
            return null;
        }
    }
}