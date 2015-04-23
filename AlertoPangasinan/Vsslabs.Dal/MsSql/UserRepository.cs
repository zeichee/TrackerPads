using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Cirrus.Data;
using Oosa.Paging;
using Oosa.Repository;
using Oosa.Repository.Dapper;
using Vsslabs.Dal.Contracts;
using Vsslabs.Dal.Properties;
using Vsslabs.Data;

namespace Vsslabs.Dal.MsSql
{
    public class UserRepository : MssqlBaseRepository<User>, IUserRepository
    {
        #region Constructor
        public UserRepository(IDbConnectionProvider dbConnectionProvider, IUserContext userContext)
            : base(TableNames.User, dbConnectionProvider, userContext)
        {
        }
        #endregion


        public bool IsUnique(int userId, string username)
        {
            using (var dbConn = Connection)
            {
                var result = dbConn.Query(TableName.GetSelectStatment("Id <> @userId AND UserName = @username"),
                    new { userId, username })
                    .FirstOrDefault();

                return result == null;
            }
        }

        public async Task<IPagedList<User>> SearchAsync(string filter, int page = 1, int pageSize = 25)
        {
            if (page > 0)
                page--;

            filter += "%";
            using (var dbConn = Connection)
            {
                dbConn.Open();
                string query = this.GetPageQuery(Resources.Users_Search, Resources.Users_Search_Count, "UserName", page, pageSize);

                using (var multiMap = await dbConn.QueryMultipleAsync(query, new { filter, start = page * pageSize, pageSize }))
                {
                    var users = multiMap.ReadAsync<User>();
                    var total = multiMap.Read<int>().First();

                    return new PagedList<User>(await users, page, pageSize, total);
                }
            }
        }

        public async Task<IList<User>> SearchAsync(string filter)
        {
            filter += "%";
            using (var dbConn = Connection)
            {
                dbConn.Open();
                using (var multiMap = await dbConn.QueryMultipleAsync(Resources.Users_Search, new { filter }))
                {
                    var users = multiMap.ReadAsync<User>();
                    var total = multiMap.Read<int>().First();

                    return (await users).ToList();
                }
            }
        }

        /// <summary>
        /// get all the user permissions
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IList<Permission>> GetUserPermissionsAsync(int userId)
        {
            using (var dbConn = Connection)
            {
                dbConn.Open();

                var result = await dbConn.QueryAsync<Permission>(Resources.Users_GetUserPermissions, new { userId });

                return await Task.Run(() => result.ToList());
            }
        }

        public Task<User> GetByUsernameAsync(string userName)
        {
            return Connection.Get<User>(TableName.GetSelectStatment("UserName=@UserName"), new { userName });
        }

        public Task<User> GetByEmailAsync(string email)
        {
            return Connection.Get<User>(TableName.GetSelectStatment("Email=@Email"), new { email });
        }
        public Task<User> GetByEmailAndUsernameAsync(string username, string email)
        {
            return Connection.Get<User>(TableName.GetSelectStatment("Email=@Email AND UserName=@Username"), new { email, username });
        }

        public async Task<User> GetByLoginInfoAsync(string loginProvider, string providerKey)
        {
            using (var db = Connection)
            {
                var query = await db.Get<User>(TableName.GetSelectStatment("LoginProvider = @loginProvider && ProviderKey = @providerKey"), new { loginProvider, providerKey });

                return query;
            }
        }

        public override async Task<int> CreateAsync(User entity)
        {
            using (var db = Connection)
            {
                db.Open();
                using (DbTransaction trans = db.BeginTransaction())
                {
                    try
                    {
                        var userTable = new Table<User>(db, TableNames.User, trans);
                        var userId = await userTable.InsertAsync(entity);

                        if (userId <= 0)
                        {
                            trans.Rollback();
                            return 0;
                        }

                        //continue adding roles
                        var acTable = new Table<AccessControl>(db, TableNames.AccessControl, trans);
                        var acId = await acTable.InsertAsync(new AccessControl { RoleId = entity.Role.Id, UserId = userId });

                        if (acId <= 0)
                        {
                            trans.Rollback();
                            return 0;
                        }

                        trans.Commit();

                        return userId;
                    }
                    catch
                    {
                        trans.Rollback();
                    }
                    return 0;
                }
            }
        }

        public async override Task<int> UpdateAsync(User entity)
        {
            using (var db = Connection)
            {
                db.Open();
                using (DbTransaction trans = db.BeginTransaction())
                {
                    try
                    {
                        var userTable = new Table<User>(db, TableNames.User, trans);
                        var status = await userTable.UpdateAsync(entity.Id, entity);

                        if (entity.Role != null)
                        {
                            //continue updating roleid in accesscontrol table
                            await db.ExecuteAsync(string.Format(Resources.AccessControl_UpdateRoleByUserId, TableNames.AccessControl),
                                new { RoleId = entity.Role.Id, UserId = entity.Id }, trans);
                        }
                        trans.Commit();

                        return status;
                    }
                    catch
                    {
                        trans.Rollback();
                    }
                    return 0;
                }
            }
        }

        public override async Task<int> DeleteAsync(int id)
        {
            using (var db = Connection)
            {
                db.Open();
                using (DbTransaction trans = db.BeginTransaction())
                {
                    try
                    {
                        //delete access control by user id
                        await db.ExecuteAsync(string.Format(Resources.AccessControl_DeleteByUserId, TableNames.AccessControl),
                            new { UserId = id }, trans);

                        var userTable = new Table<User>(db, TableNames.User, trans);
                        await userTable.DeleteAsync(id);

                        trans.Commit();
                        return 1;
                    }
                    catch
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
        }

        public async Task<IEnumerable<User>> GetAllInIdsAsync(int[] userIds)
        {
            using (var dbConn = Connection)
            {
                return await dbConn.QueryAsync<User>(TableName.GetSelectStatment("Id IN @ids"), new { ids = userIds });
            }

        }

        
        public async Task<IPagedList<User>> GetAllByTenantAsync(int tenantId, string filter, int page = 1, int pageSize = 25)
        {
            if (page > 0)
                page--;

            using (var dbConn = Connection)
            {
                filter = "%" + filter + "%";
                dbConn.Open();
                var sql = string.Format("{0} AND [TenantId] = @tenantId", Resources.Users_Search);
                var sqlCount = string.Format("{0} AND [TenantId] = @tenantId", Resources.Users_Search_Count);
                string query = this.GetPageQuery(sql, sqlCount, "[Users].Id", page, pageSize);

                using (var multiMap = await dbConn.QueryMultipleAsync(query, new { filter, tenantId, start = page * pageSize, pageSize }))
                {
                    var users = multiMap.ReadAsync<User>();
                    var total = multiMap.Read<int>().First();

                    return new PagedList<User>(await users, page, pageSize, total);
                }
            }
        }

        public async Task<IEnumerable<User>> GetAllByRoleAsync(int roleId)
        {
            using (var db = Connection)
            {
                db.Open();

                const string sql = "SELECT a.*FROM [Users] a " +
                                   "INNER JOIN [AccessControl] c ON a.Id = c.UserId " +
                                   "WHERE c.RoleId = @roleId";

                var users = await db.QueryAsync<User>(sql, new { roleId });

                return users;
            }
        }
    }
}