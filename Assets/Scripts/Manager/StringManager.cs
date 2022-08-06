using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringManager : StaticSingleton<StringManager>
{
    StringTable stringTable = new StringTable();

    public class StringTable
    {
        public Dictionary<int, string> _dicData = new Dictionary<int, string>();
        public string GetString(int ID)
        {
            return _dicData.ContainsKey(ID) ? _dicData[ID] : null;
        }

        protected string address = "Table";
        private string assetName = "StringTable";
    }
}
