using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : Tin tức", Code = "MNews", Order = 2)]
    public class MNewsController : Controller
    {
        [VSW.Core.MVC.PropertyInfo("Chuyên mục", "Type|News")]
        public int MenuID;

        //[VSW.Core.MVC.PropertyInfo("Vị trí", "ConfigKey|Mod.NewsState")]
        public int State;

        [VSW.Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 10;

        public void ActionIndex(MNewsModel model)
        {
            if (ViewPage.CurrentPage.MenuID > 0)
                MenuID = ViewPage.CurrentPage.MenuID;

            var dbQuery = ModNewsService.Instance.CreateQuery()
                            .Where(o => o.Activity == true)
                            .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("News", MenuID, ViewPage.CurrentLang.ID))
                            .OrderByDesc(o => o.Order)
                            .Take(PageSize)
                            .Skip(PageSize * model.Page);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            model.PageSize = PageSize;
            ViewBag.Model = model;
        }

        public void ActionDetail(string endCode)
        {
            if (ViewPage.CurrentPage.MenuID > 0)
                MenuID = ViewPage.CurrentPage.MenuID;

            var item = ModNewsService.Instance.CreateQuery()
                            .Where(o => o.Activity == true && o.Code == endCode)
                            .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("News", MenuID, ViewPage.CurrentLang.ID))
                            .ToSingle();


            if (item != null)
            {
                // Tăng số lượt xem
                item.CountViewed = item.CountViewed + 1;
                ModNewsService.Instance.Save(item);

                MNewsModel model = new MNewsModel();
                TryUpdateModel(model);

                //var dbQuery = ModCommentService.Instance.CreateQuery()
                //        .Where(o => o.Activity == true && o.NewsID == item.ID && o.ReplyByID == 0)
                //        .OrderByDesc(o => o.ID)
                //        .Take(PageSize)
                //        .Skip(PageSize * model.Page);

                //ViewBag.Comment = dbQuery.ToList();
                //model.TotalRecord = dbQuery.TotalRecord;
                //model.PageSize = PageSize;
                //ViewBag.Model = model;

                ViewBag.Other = ModNewsService.Instance.CreateQuery()
                                    .Where(o => o.Activity == true && o.MenuID == item.MenuID && o.ID != item.ID)// && o.Order < item.Order
                    //.WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("News", MenuID, ViewPage.CurrentLang.ID))
                                    .OrderByDesc(o => o.Published)
                    //.OrderByDesc(o => o.Order)
                                    .Take(PageSize)
                                    .ToList();

                ViewBag.Data = item;
                // Lấy danh sách các bình luận
                ViewBag.ListComment = BinhLuan(item);

                ViewPage.CurrentPage.PageTitle = string.IsNullOrEmpty(item.PageTitle) ? item.Name : item.PageTitle;
                ViewPage.CurrentPage.PageDescription = string.IsNullOrEmpty(item.PageDescription) ? item.Summary : item.PageDescription;
                ViewPage.CurrentPage.PageKeywords = item.PageKeywords;
            }
            else
            {
                ViewPage.Error404();
            }
        }

        public void ActionAddCommentPOST(ModCommentEntity entity)
        {
            entity.Email = Global.Utils.GetEmailAddress(entity.Email);
            entity.Content = Global.Data.RemoveAllTag(entity.Content);

            if (entity.Name.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập : Họ và tên.");

            if (entity.Email.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập : Email.");

            if (entity.Content.Trim() == string.Empty)
                ViewPage.Message.ListMessage.Add("Nhập : Nội dung.");

            if (entity.Content.Length > 500)
                ViewPage.Message.ListMessage.Add("Nội dung quá dài (Nhiều hơn 500 ký tự).");

            //hien thi thong bao loi
            if (ViewPage.Message.ListMessage.Count > 0)
            {
                string message = @"Các thông tin nhập còn thiếu hoặc chưa chính xác: \r\n";

                for (int i = 0; i < ViewPage.Message.ListMessage.Count; i++)
                    message += @"\r\n + " + ViewPage.Message.ListMessage[i];

                ViewPage.Alert(message);
            }
            else
            {
                entity.ID = 0;
                entity.IP = VSW.Core.Web.HttpRequest.IP;
                entity.Created = DateTime.Now;
                entity.NewsID = ViewBag.Data.ID;
                entity.Activity = true;

                ModCommentService.Instance.Save(entity);

                #region Cập nhật số lượt bình luận cho bài viết
                ModNewsEntity itemNews = ModNewsService.Instance.GetByID(ViewBag.Data.ID);
                // Tăng số lượt bình luận
                itemNews.CountComment = itemNews.CountComment + 1;
                ModNewsService.Instance.Save(itemNews);
                #endregion

                Global.Cookies.SetValue("Web.Comment.Name", entity.Name, 5, true);
                Global.Cookies.SetValue("Web.Comment.Email", entity.Email, 5, true);

                // xoa trang
                entity = new ModCommentEntity();

                ViewPage.Alert("Cảm ơn bạn đã bình luận !");
                //ViewPage.RefreshPage();
            }

            ViewBag.AddComment = entity;
        }

        /// <summary>
        /// Lấy các thông tin bình luận
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string BinhLuan(ModNewsEntity item)
        {
            string sData = "<div class='div-no-comment'>Chưa có bình luận nào cho sản phẩm</div>";
            if (item == null)
                return sData;

            // LẤy các comment đã được duyệt
            var lstDataComment = ModCommentService.Instance.CreateQuery().Where(o => o.NewsID == item.ID && o.Activity == true)
                                                              .OrderByDesc(o => o.Created).ToList();

            if (lstDataComment == null || lstDataComment.Count <= 0)
                return sData;

            sData = string.Empty;

            foreach (var itemComment in lstDataComment)
            {
                sData += "<div class='div-comment-group'>";
                sData += "<div class='div-comment-group-info-user'><span class='div-comment-group-info'>" + itemComment.Name + "</span><span class='div-comment-group-info-email'>" + (string.IsNullOrEmpty(itemComment.Email) ? string.Empty : " - ") + itemComment.Email + "</span>";
                sData += "<span class='div-comment-group-info-date'>(" + itemComment.Created.ToString("dd/MM/yyyy HH:mm") + ")</span></div>";

                // nội dung comment
                sData += "<div class='div-comment-content'>";
                sData += itemComment.Content.Replace("\n", "<br />");
                sData += "</div>";
                sData += "</div>";
            }

            return sData;
        }
    }

    public class MNewsModel
    {
        private int _Page = 0;
        public int Page
        {
            get { return _Page; }
            set { _Page = value - 1; }
        }

        public int TotalRecord { get; set; }
        public int PageSize { get; set; }
    }
}
