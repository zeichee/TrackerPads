using Cirrus.Data;
using Oosa.Repository;
using Vsslabs.Dal.Contracts;
using Vsslabs.Data;

namespace Vsslabs.Dal.MsSql
{
    public class PermissionRepository : MssqlBaseRepository<Permission>, IPermissionRepository
    {
        #region Constructor

        public PermissionRepository(IDbConnectionProvider dbConnectionProvider, IUserContext userContext)
            : base(TableNames.Permission, dbConnectionProvider, userContext)
        {
        }

        #endregion
    }
}
