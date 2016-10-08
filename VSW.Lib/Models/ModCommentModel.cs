using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModCommentEntity : EntityBase
    {
        
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int NewsID { get; set; }

        [DataInfo]
        public int ReplyByID { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Email { get; set; }

        [DataInfo]
        public string Content { get; set; }

        [DataInfo]
        public bool HasReply { get; set; }

        [DataInfo]
        public string IP { get; set; }

        [DataInfo]
        public DateTime Created { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion      
  
        private ModNewsEntity _oNews = null;
        public ModNewsEntity getNews()
        {
            if (_oNews == null && NewsID > 0)
                _oNews = ModNewsService.Instance.GetByID(NewsID);

            if (_oNews == null)
                _oNews = new ModNewsEntity();

            return _oNews;
        }

        private ModCommentEntity _oReplyBy = null;
        public ModCommentEntity getReplyBy()
        {
            if (_oReplyBy == null && ReplyByID > 0)
                _oReplyBy = ModCommentService.Instance.GetByID(ReplyByID);

            if (_oReplyBy == null)
                _oReplyBy = new ModCommentEntity();

            return _oReplyBy;
        }

    }

    public class ModCommentService : ServiceBase<ModCommentEntity>
    {

        #region Autogen by VSW

        private ModCommentService()
            : base("[Mod_Comment]")
        {

        }

        private static ModCommentService _Instance = null;
        public static ModCommentService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModCommentService();

                return _Instance;
            }
        }

        #endregion

        public ModCommentEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

    }
}