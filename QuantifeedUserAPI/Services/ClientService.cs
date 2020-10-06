using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuantifeedUserAPI.Context;
using QuantifeedUserAPI.Interfaces;
using QuantifeedUserAPI.Models;

namespace QuantifeedUserAPI.Services
{
    /// <summary>
    /// Client Service
    /// </summary>
    public class ClientService : IClientService
    {
        QuantifeedDBContext db;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_db"></param>
        public ClientService(QuantifeedDBContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// get Clients
        /// </summary>
        /// <returns></returns>
        public async Task<List<Client>> GetClients()
        {
            if (db != null)
            {
                return await db.Clients.ToListAsync();
            }

            return null;
        }

        /// <summary>
        /// For a specified Manager username, retrieve a list of its clients
        /// </summary>
        /// <param name="managerName"></param>
        /// <returns></returns>
        public async Task<List<Client>> GetClients(string managerName)
        {
            // step 1 : retrieve the User ID from the Users 
            if (db?.Users == null) return null;
            var userId = db.Users.FirstOrDefault(x => x.UserName.Equals(managerName)).UserId;
                
            // step 2 : retrieve manager from user Id 
            var managerId = await db.Managers.FirstOrDefaultAsync(x => x.UserId == userId);

            return await db.Clients.Where(x => x.ManagerId == managerId.ManagerId).ToListAsync();

        }
    }
}