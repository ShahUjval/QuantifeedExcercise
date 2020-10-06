using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuantifeedUserAPI.Context;
using QuantifeedUserAPI.Interfaces;
using QuantifeedUserAPI.Models;

namespace QuantifeedUserAPI.Services
{
    /// <summary>
    /// User Service
    /// </summary>
      public class UserService: IUserService
        {

            QuantifeedDBContext db;
            public UserService(QuantifeedDBContext _db)
            {
                db = _db;
            }
            
            /// <summary>
            /// get users
            /// </summary>
            /// <returns></returns>
            public async Task<List<User>> GetUsers()
            {
                if (db != null)
                {
                    return await db.Users.ToListAsync();
                }
                return null;
            }

            /// <summary>
            /// Add Users
            /// </summary>
            /// <param name="user">User</param>
            /// <returns></returns>
            public async Task<int> AddUser(User user)
            {
                if (db != null)
                {
                    await db.Users.AddAsync(user);
                    await db.SaveChangesAsync();

                    return user.UserId;
                }
                return 0;
            }

            /// <summary>
            /// Update User
            /// </summary>
            /// <param name="user">User/param>
            /// <returns></returns>
            public async Task UpdateUser(User user)
            {
                if (db != null)
                {
                    //Delete that user
                    db.Users.Update(user);
                    //Commit the transaction
                    await db.SaveChangesAsync();
                }
            }

            /// <summary>
            /// Delete User
            /// </summary>
            /// <param name="id">id of the User to delete</param>
            /// <returns></returns>
            public async Task<int> DeleteUser(int id)
            {
                int result = 0;

                if (db != null)
                {
                    //Find the user for specific user id
                    var user = await db.Users.FirstOrDefaultAsync(x => x.UserId == id);

                    if (user != null)
                    {
                        //Delete that user
                        db.Users.Remove(user);

                        //Commit the transaction
                        result = await db.SaveChangesAsync();
                    }
                    return result;
                }

                return result;
            }
        }
}