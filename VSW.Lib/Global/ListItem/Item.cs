using System;

namespace VSW.Lib.Global.ListItem
{
    public class Item
    {
        public Item(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}