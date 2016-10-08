using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data.Linq;

namespace VSW.Lib.LinqToSql
{
    public class DbExecute
    {
        DbExecute() { }

        /// <summary>
        /// Lấy connection String từ webconfig
        /// </summary>
        /// <returns></returns>
        private static string getConnectionString()
        {
            try
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            }
            catch (Exception)
            {
                // Chuỗi kết nối không tồn tại
                throw;
            }
        }

        /// <summary>
        /// Create Db connection to Query
        /// </summary>
        /// <returns></returns>
        public static DbDataContext Create()
        {
            DbDataContext dbCreate = new DbDataContext(getConnectionString());
            return dbCreate;
        }

        /// <summary>
        /// Create Db connection to Query attach LazyLoad property
        /// </summary>
        /// <param name="bolLazyLoad">LazyLoad property to Enable or Disable</param>
        /// <returns></returns>
        public static DbDataContext Create(bool bolLazyLoad)
        {
            DbDataContext dbCreate = new DbDataContext(getConnectionString());
            dbCreate.DeferredLoadingEnabled = bolLazyLoad;
            return dbCreate;
        }

        /// <summary>
        /// Set lazy load in linq to sql to False
        /// </summary>
        /// <param name="dbInput"></param>
        /// <returns></returns>
        public static DbDataContext SetDeferredLoadingToDisabled(DbDataContext dbInput)
        {
            dbInput.DeferredLoadingEnabled = false;
            return dbInput;
        }

        /// <summary>
        /// Set lazy load in linq to sql to True
        /// </summary>
        /// <param name="dbInput"></param>
        /// <returns></returns>
        public static DbDataContext SetDeferredLoadingToEnabled(DbDataContext dbInput)
        {
            dbInput.DeferredLoadingEnabled = true;
            return dbInput;
        }

    }
}
