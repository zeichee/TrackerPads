using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Oosa.Repository;
using Oosa.Repository.Dapper;

namespace Vsslabs.Dal.MsSql
{
    internal static class MssqlBaseRepositoryExtensions
    {
        internal static string GetPageQuery(this IRepository repository, string selectSql, string countSql, string orderByField, int page, int pageSize)
        {
            var q = " {0} " +
                    " ORDER BY {1}" +
                    " OFFSET @start ROWS" +
                    " FETCH NEXT @pageSize ROWS ONLY" +
                    " {2}";

            return string.Format(q, selectSql, orderByField, countSql);
        }

        internal static string GetSelectStatment(this string tableName, string where = null, string orderby = null)
        {
            var builder = new SqlBuilder();

            if (string.IsNullOrEmpty(where) == false)
                builder.Where(where);
            if (string.IsNullOrEmpty(orderby) == false)
                builder.OrderBy(orderby);

            var template = builder.AddTemplate(string.Format("SELECT * FROM [{0}] /**where**/ /**orderby**/", tableName));

            return template.RawSql;
        }

        internal static string GetJoinStatment(this string tableName, string selectFields = null, string[] innerJoin = null, string where = null, string orderby = null)
        {
            var builder = new SqlBuilder();

            if (string.IsNullOrEmpty(selectFields) == false)
                builder.Select(selectFields);

            if (string.IsNullOrEmpty(where) == false)
                builder.Where(where);

            if (string.IsNullOrEmpty(orderby) == false)
                builder.OrderBy(orderby);

            if (innerJoin != null)
            {
                foreach (var join in innerJoin)
                {
                    builder.InnerJoin(join);
                }
            }

            var template = builder.AddTemplate(string.Format("SELECT /**select**/ FROM [{0}] /**innerjoin**/ /**where**/ /**orderby**/", tableName));

            return template.RawSql;
        }

        internal static async Task<T> Get<T>(this DbConnection dbConn, string sql, dynamic param = null)
            where T : new()
        {
            using (dbConn)
            {
                if (dbConn.State != ConnectionState.Open)
                    dbConn.Open();

                IEnumerable<T> task = await SqlMapper.QueryAsync<T>(dbConn, sql, param);

                return task.FirstOrDefault();
            }
        }

        //internal static string DeleteStatment<T>(this string tableName, Expression<Func<T, dynamic>> columnSelector, string paramater = null, bool isArray = false)
        //{
        //    var member = columnSelector.Body as MemberExpression;
        //    if (member != null && member.Member is PropertyInfo)
        //    {
        //        var property = member.Member as PropertyInfo;
        //        var columnName = property.Name;
        //        var assigningOperator = "=";

        //        if (isArray)
        //            assigningOperator = "IN";

        //        return tableName.DeleteStatment(string.Format("{0} {1} @{2}", columnName, assigningOperator, 
        //            paramater ?? columnName));
        //    }

        //    throw new ArgumentException("Expression is not a Property", "columnSelector");
        //}

        internal static string DeleteStatment(this string tableName, string where = null)
        {
            var builder = new SqlBuilder();

            if (string.IsNullOrEmpty(where) == false)
                builder.Where(where);

            var template = builder.AddTemplate(string.Format("DELETE [{0}] /**where**/", tableName));

            return template.RawSql;
        }

        /*
        //public static string PageQuery(this IRepository repository, string tableName, string columns, string where, string orderBy, object parameters, int page, int pageSize)
        //{
        //    var pageQueryTemplate = string.Format("SELECT {0} FROM (SELECT {0}, ROW_NUMBER() OVER (ORDER BY /**orderby**/
        //) //AS RowNum  FROM [{1}] /**where**/) AS Paged WHERE BETWEEN @start AND @finish",
        //        columns, tableName);

        //    var totalQueryTemplate = string.Format("SELECT COUNT(*) FROM [{0}] /**where**/", tableName);

        //    var builder = new SqlBuilder();

        //    var start = (page - 1) * pageSize + 1;
        //    var finish = page * pageSize;

        //    var selectTemplate = builder.AddTemplate(pageQueryTemplate, new { start, finish });
        //    var countTemplate = builder.AddTemplate(totalQueryTemplate);

        //    builder.Where(where, parameters);

        //    builder.OrderBy(orderBy);

        //    return builder.ToString()
        //}

        //public static string PageQuery<T>(this IRepository repository, string tableName, string where, string orderBy = "ID")
        //{
        //    //            //columns, pageSize, orderBy, TableName, where

        //    //            private string pagedQuery = @"SELECT * FROM (SELECT *, ROW_NUMBER() OVER (/**orderby**/) AS RowNumber FROM (
        //    //            SELECT d.*, COUNT(r.DinnerID) AS RsvpCount 
        //    //            FROM Dinners d LEFT OUTER JOIN RSVP r ON d.DinnerID = r.DinnerID 
        //    //            /**where**/
        //    //            GROUP BY d.DinnerID, d.Title, d.EventDate, d.Description, d.HostedById, d.HostedBy, d.ContactPhone, d.Address, d.Country, d.Latitude, d.Longitude
        //    //            ) as X ) as Y
        //    //            WHERE RowNumber BETWEEN @start AND @finish";
        //    var paramNames = ParamNameCache.GetParamNames(Activator.CreateInstance<T>());
        //    var columns = string.Join(",", paramNames.Select(p => "[" + p + "]"));
        //    return PageQuery(repository, tableName, columns);
        //}


    }
}
