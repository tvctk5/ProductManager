using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    public class SysResourceController : CPController
    {
        public SysResourceController()
        {
            //khoi tao Service
            DataService = WebResourceService.Instance;
            //CheckPermissions = false;
        }

        public void ActionIndex(SysResourceModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort, "[Code]");

            // tao danh sach
            var dbQuery = WebResourceService.Instance.CreateQuery()
                                .Where(o => o.LangID == model.LangID)
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Code.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionUpload(SysResourceModel model)
        {
            CPViewPage.Script("Redirect", "VSWRedirect('Import')");
        }

        public void ActionImport(SysResourceModel model)
        {
            ViewBag.Model = model;
        }

        public void ActionAdd(SysResourceModel model)
        {
            if (model.RecordID > 0)
            {
                item = WebResourceService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new WebResourceEntity();

                // khoi tao gia tri mac dinh khi insert
                item.LangID = model.LangID;
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(SysResourceModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(SysResourceModel model)
        {
            if (model.Type > 0)
            {
                if (model.Type == 1 && !string.IsNullOrEmpty(model.Value1))
                {
                    string[] Arr = model.Value1.Split('\n');
                    for (int i = 0; i < Arr.Length; i++)
                    {
                        string s = Arr[i].Trim();

                        if (string.IsNullOrEmpty(s))
                            continue;

                        if (s.StartsWith("//"))
                            continue;

                        int index = s.IndexOf('=');
                        if (index == -1)
                            continue;

                        string key = s.Substring(0, index).Trim();
                        string value = s.Substring(index + 1).Trim();

                        if (WebResourceService.Instance.CP_HasExists(key, model.LangID1))
                        {
                            if (model.Overwrite1)
                            {
                                var rs = WebResourceService.Instance.CreateQuery()
                                           .Where(o => o.LangID == model.LangID1 && o.Code == key)
                                           .ToSingle();

                                rs.Value = value;

                                WebResourceService.Instance.Save(rs, o => o.Value);
                            }
                        }
                        else
                        {
                            WebResourceService.Instance.Save(new WebResourceEntity 
                            {
                                LangID = model.LangID1,
                                Code = key,
                                Value = value
                            });
                        }
                    }
                }
                else if (model.Type == 2)
                {
                    if (model.FLangID2 != model.TLangID2)
                    {
                        var listRS = WebResourceService.Instance.CreateQuery()
                                           .Where(o => o.LangID == model.FLangID2)
                                           .ToList();

                        for (int i = 0; listRS != null && i < listRS.Count; i++)
                        {
                            string key = listRS[i].Code;
                            string value = listRS[i].Value;

                            if (WebResourceService.Instance.CP_HasExists(key, model.TLangID2))
                            {
                                if (model.Overwrite2)
                                {
                                    var rs = WebResourceService.Instance.CreateQuery()
                                               .Where(o => o.LangID == model.TLangID2 && o.Code == key)
                                               .ToSingle();

                                    rs.Value = value;

                                    WebResourceService.Instance.Save(rs, o => o.Value);
                                }
                            }
                            else
                            {
                                WebResourceService.Instance.Save(new WebResourceEntity
                                {
                                    LangID = model.TLangID2,
                                    Code = key,
                                    Value = value
                                });
                            }
                        }
                    }
                }
                else if (model.Type == 3)
                {
                    //var listRS = WebResourceService.Instance.CreateQuery()
                    //                .Where(o => o.LangID == model.LangID3)
                    //                .ToList();

                    //for (int i = 0; listRS != null && i < listRS.Count; i++)
                    //{
                    //    string key = listRS[i].Code;
                    //    string value = GetTranslate(listRS[i].Value, model.FLangID3, model.TLangID3);

                    //    if (value != string.Empty)
                    //    {
                    //        listRS[i].Value = value;

                    //        WebResourceService.Instance.Save(listRS[i], o => o.Value);
                    //    }
                    //}
                }

                if ((model.Type == 1 && !string.IsNullOrEmpty(model.Value1)) || model.Type == 2 || model.Type == 3)
                {
                    CPViewPage.SetMessage("Thông tin đã cập nhật.");

                    return;
                }
            }

            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(SysResourceModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        public override void ActionCancel()
        {
            CPViewPage.Response.Redirect(CPViewPage.Request.RawUrl.Replace("Add.aspx", "Index.aspx")
                .Replace("Import.aspx", "Index.aspx"));
        }

        #region private func

        private WebResourceEntity item = null;

        private bool ValidSave(SysResourceModel model)
        {
            TryUpdateModel(item);

            ViewBag.Data = item;
            ViewBag.Model = model;

            CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;

            //kiem tra ma
            if (item.Code.Trim() == string.Empty)
                CPViewPage.Message.ListMessage.Add("Nhập mã.");

            //kiem tra ton tai
            if (model.RecordID < 1 && WebResourceService.Instance.CP_HasExists(item.Code, item.LangID))
                CPViewPage.Message.ListMessage.Add("Mã đã tồn tại.");

            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                try
                {
                    //save
                    WebResourceService.Instance.Save(item);
                }
                catch (Exception ex)
                {
                    Global.Error.Write(ex);
                    CPViewPage.Message.ListMessage.Add(ex.Message);
                    return false;
                }

                return true;
            }

            return false;
        }

        //private string GetTranslate(string text, string source, string target)
        //{
        //    string s = GetHtml("https://www.googleapis.com/language/translate/v2?key=AIzaSyBn-CG2UNWbtRMShHZ2b-VhbMQESX8hFEM&q=" + text + "&source=" + source + "&target=" + target + "&callback=handleResponse&prettyprint=false");

        //    if (s == string.Empty || !s.Contains("handleResponse"))
        //        return string.Empty;

        //    return s.Replace("handleResponse({\"data\":{\"translations\":[{\"translatedText\":\"", "")
        //            .Replace("\"}]}});", "")
        //            .Replace("// API callback", "").Trim();
        //}

        //private string GetHtml(string url)
        //{
        //    string html = string.Empty;

        //    try
        //    {
        //        System.Net.WebClient a = new System.Net.WebClient();
        //        html = System.Text.Encoding.UTF8.GetString(a.DownloadData(url));
        //    }
        //    catch { }

        //    return DecodeEncodedNonAsciiCharacters(html);
        //}

        //private string DecodeEncodedNonAsciiCharacters(string value)
        //{
        //    return System.Text.RegularExpressions.Regex.Replace(
        //        value,
        //        @"\\u(?<Value>[a-zA-Z0-9]{4})",
        //        m =>
        //        {
        //            return ((char)int.Parse(m.Groups["Value"].Value, System.Globalization.NumberStyles.HexNumber)).ToString();
        //        });
        //}

        #endregion
    }

    public class SysResourceModel : DefaultModel
    {
        private int _LangID = 1;
        public int LangID
        {
            get { return _LangID; }
            set { _LangID = value; }
        }

        public string SearchText { get; set; }


        private int _Type = 1;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private int _LangID1 = 1;
        public int LangID1
        {
            get { return _LangID1; }
            set { _LangID1 = value; }
        }
        public bool Overwrite1 { get; set; }
        public string Value1 { get; set; }

        public bool Overwrite2 { get; set; }
        private int _FLangID2 = 1;
        public int FLangID2
        {
            get { return _FLangID2; }
            set { _FLangID2 = value; }
        }
        private int _TLangID2 = 2;
        public int TLangID2
        {
            get { return _TLangID2; }
            set { _TLangID2 = value; }
        }

        private int _LangID3 = 2;
        public int LangID3
        {
            get { return _LangID3; }
            set { _LangID3 = value; }
        }
        private string _FLangID3 = "vi";
        public string FLangID3
        {
            get { return _FLangID3; }
            set { _FLangID3 = value; }
        }
        private string _TLangID3 = "en";
        public string TLangID3
        {
            get { return _TLangID3; }
            set { _TLangID3 = value; }
        }
    }
}
