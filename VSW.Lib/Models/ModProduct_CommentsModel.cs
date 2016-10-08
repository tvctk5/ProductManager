using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_CommentsEntity : EntityBase
    {

        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int ProductInfoId { get; set; }

        [DataInfo]
        public int UserId { get; set; }

        [DataInfo]
        public int CustomersId { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Address { get; set; }

        [DataInfo]
        public string PhoneNumber { get; set; }

        [DataInfo]
        public string Email { get; set; }

        [DataInfo]
        public string Content { get; set; }

        [DataInfo]
        public bool Approved { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        [DataInfo]
        public DateTime ModifiedDate { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion

    }

    public class ModProduct_CommentsService : ServiceBase<ModProduct_CommentsEntity>
    {

        #region Autogen by VSW

        private ModProduct_CommentsService()
            : base("[Mod_Product_Comments]")
        {

        }

        private static ModProduct_CommentsService _Instance = null;
        public static ModProduct_CommentsService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_CommentsService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_CommentsEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}