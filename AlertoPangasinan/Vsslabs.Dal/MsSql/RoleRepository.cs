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
    public class RoleRepository : MssqlBaseRepository<Role>, IRoleRepository
    {
        #region Constructor

        public RoleRepository(IDbConnectionProvider dbConnectionProvider, IUserContext userContext)
            : base(TableNames.Role, dbConnectionProvider, userContext)
        {
        }

        #endregion

        #region IsUnique

        public bool IsUnique(int roleId, string roleName)
        {
            using (DbConnection con = Connection)
            {
                con.Open();

                return 0 == con.ExecuteScalar<int>(Resources.Role_IsUnique, new { Id = roleId, Name = roleName });
            }
        }

        public async Task<bool> IsUniqueAsync(int roleId, string roleName)
        {
            using (DbConnection con = Connection)
            {
                con.Open();

                var result = await con.ExecuteScalarAsync<int>(Resources.Role_IsUnique, new { Id = roleId, Name = roleName });

                return result == 0;
            }
        }
        #endregion

        #region Create

        public override async Task<int> CreateAsync(Role entity)
        {
            using (DbConnection dbConn = Connection)
            {
                dbConn.Open();

                int count = 0;
                using (DbTransaction trans = dbConn.BeginTransaction())
                {
                    try
                    {
                        var roles = new Table<Role>(dbConn, TableName, trans);
                        var rolePermissions = new Table<RolePermission>(dbConn, TableNames.RolePermission, trans);

                        entity.Id = (await roles.InsertAsync(entity));
                        count++;

                        foreach (var rolePermission in entity.RolePermissions)
                        {
                            rolePermission.RoleId = entity.Id;

                            var id = await rolePermissions.InsertAsync(rolePermission);
                            rolePermission.Id = id;

                            count++;
                        }

                        trans.Commit();

                        return count;
                    }
                    catch
                    {
                        trans.Rollback();

                        return 0;
                    }
                }
            }
        }

        public override int Create(Role entity)
        {
            using (DbConnection dbConn = Connection)
            {
                dbConn.Open();

                int count = 0;
                using (DbTransaction trans = dbConn.BeginTransaction())
                {
                    try
                    {
                        var roles = new Table<Role>(dbConn, TableName, trans);
                        var rolePermissions = new Table<RolePermission>(dbConn, TableNames.RolePermission, trans);

                        entity.Id = roles.Insert(entity);
                        count++;

                        foreach (var rolePermission in entity.RolePermissions)
                        {
                            rolePermission.RoleId = entity.Id;

                            rolePermission.Id = rolePermissions.Insert(rolePermission);

                            count++;
                        }

                        trans.Commit();

                        return count;
                    }
                    catch
                    {
                        trans.Rollback();

                        return 0;
                    }
                }
            }
        }
        #endregion

        #region Update
        public override int Update(Role entity)
        {
            using (DbConnection dbConn = Connection)
            {
                dbConn.Open();

                int count = 0;
                using (DbTransaction trans = dbConn.BeginTransaction())
                {
                    try
                    {
                        var roles = new Table<Role>(dbConn, TableName, trans);
                        var rolePermissions = new Table<RolePermission>(dbConn, TableNames.RolePermission, trans);

                        entity.Id = roles.Update(entity.Id, entity);
                        count++;

                        dbConn.Execute(string.Format(Resources.RolePermissions_DeleteByRoleId, TableNames.RolePermission),
                            new { RoleId = entity.Id }
                            , trans);

                        foreach (var rolePermission in entity.RolePermissions)
                        {
                            rolePermission.RoleId = entity.Id;

                            rolePermission.Id = rolePermissions.Insert(rolePermission);

                            count++;
                        }

                        trans.Commit();

                        return count;
                    }
                    catch
                    {
                        trans.Rollback();

                        return 0;
                    }
                }
            }
        }

        public override async Task<int> UpdateAsync(Role entity)
        {
            using (DbConnection dbConn = Connection)
            {
                dbConn.Open();

                int count = 0;
                using (DbTransaction trans = dbConn.BeginTransaction())
                {
                    try
                    {
                        var roles = new Table<Role>(dbConn, TableName, trans);
                        var rolePermissions = new Table<RolePermission>(dbConn, TableNames.RolePermission, trans);

                        entity.Id = (await roles.UpdateAsync(entity.Id, entity));
                        count++;

                        await dbConn.ExecuteAsync(Resources.RolePermissions_DeleteByRoleId,
                            new { RoleId = entity.Id }
                            , trans);

                        foreach (var rolePermission in entity.RolePermissions)
                        {
                            rolePermission.RoleId = entity.Id;

                            rolePermission.Id = (await rolePermissions.InsertAsync(rolePermission));

                            count++;
                        }

                        trans.Commit();

                        return count;
                    }
                    catch
                    {
                        trans.Rollback();

                        return 0;
                    }
                }
            }
        }
        #endregion

        #region GetById
        public override Role GetById(int id)
        {
            using (DbConnection dbConn = Connection)
            {
                dbConn.Open();

                using (var multiMap = dbConn.QueryMultiple(Resources.Roles_GetById, new { RoleId = id }))
                {
                    var role = multiMap.Read<Role>().FirstOrDefault();
                    if (role != null)
                        role.RolePermissions = multiMap.Read<RolePermission>()
                                                        .ToList();

                    return role;
                }

            }
        }
        public override async Task<Role> GetByIdAsync(int id)
        {
            using (DbConnection dbConn = Connection)
            {
                dbConn.Open();

                using (var multiMap = await dbConn.QueryMultipleAsync(Resources.Roles_GetById, new { RoleId = id }))
                {
                    var role = multiMap.Read<Role>().FirstOrDefault();
                    if (role != null)
                        role.RolePermissions = multiMap.Read<RolePermission>()
                                                        .ToList();

                    return role;
                }
            }
        }
        #endregion

        public override async Task<IEnumerable<Role>> GetAllAsync()
        {
            var sqlQuery = TableName.GetSelectStatment() + ";"
                + TableNames.RolePermission.GetSelectStatment() + ";"
                + TableNames.Tenant.GetSelectStatment();

            using (DbConnection dbConn = Connection)
            {
                dbConn.Open();

                using (var multiMap = await dbConn.QueryMultipleAsync(sqlQuery))
                {
                    var roles = multiMap.Read<Role>();
                    var rolePermissions = multiMap.Read<RolePermission>();

                    foreach (var role in roles)
                    {
                        role.RolePermissions = rolePermissions.Where(x => x.RoleId == role.Id)
                            .ToList();
                    }

                    return roles;
                }
            }
        }

        public async Task<IPagedList<Role>> SearchAsync(string filter, int page = 1, int pageSize = 25)
        {
            filter += "%";

            if (page > 0)
                page--;

            using (var dbConn = Connection)
            {
                dbConn.Open();

                var query = this.GetPageQuery(Resources.Roles_Search, Resources.Roles_Search_Count, "RoleName", page, pageSize);

                using (var multiMap = await dbConn.QueryMultipleAsync(query, new { filter, start = page * pageSize, pageSize }))
                {
                    var roles = multiMap.ReadAsync<Role>();
                    var total = multiMap.Read<int>().First();

                    return new PagedList<Role>(await roles, page, pageSize, total);
                }
            }
        }


        public async Task<System.Collections.Generic.IEnumerable<Role>> GetRolesByUserIdAsync(int userId)
        {
            using (var db = Connection)
            {
                const string sql = "SELECT a.* FROM [Roles] a " +
                                   "INNER JOIN [AccessControl] ON [AccessControl].RoleId = a.Id " +
                                   "WHERE [AccessControl].UserId = @userId";

                var query = await db.QueryAsync<Role>(sql, new { userId });
                return query;
            }
        }
    }
}
