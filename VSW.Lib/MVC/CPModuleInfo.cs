using System;

namespace VSW.Lib.MVC
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CPModuleInfo : Attribute
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public int Access { get; set; }

        public int Order { get; set; }

        public int State { get; set; }

        public bool ShowInMenu { get; set; }

        public string CssClass { get; set; }

        /// <summary>
        /// 1: Danh mục | 2: Bài viết | 3: Sản phẩm | 4: Tiện ích
        /// </summary>
        public int MenuGroupId { get; set; }
    }
}

