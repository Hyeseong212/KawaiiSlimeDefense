using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StrMgr
{
    static public string GetStr(int ID) { return StringManager.i.GetStr(E_STRING.STRING, ID); }
    static public string GetUI(int ID) { return StringManager.i.GetStr(E_STRING.UI, ID); }
    static public string GetError(int ID) { return StringManager.i.GetStr(E_STRING.ERROR, ID); }
    static public string GetLocal(int ID) { return LocalManager.i.GetStr(ID); }

    static public string Get(E_STRING e, int ID) { return e == E_STRING.LOCAL ? LocalManager.i.GetStr(ID) : StringManager.i.GetStr(e, ID); }

}
public enum E_STRING
{
    STRING = 0,
    UI,
    ERROR,
    HELP,
    LOCAL,
    MAX
}
public class LocalManager : StaticSingleton<LocalManager>
{
    StringManager.LocalTable table;
    public string GetStr(int ID) { return table.GetString(ID); }
    public override void Init()
    {
        table = new StringManager.LocalTable();
        table.Load();
    }
}
public class StringManager : StaticSingleton<StringManager>
{
    StringTable stringTable = new StringTable();
    StringUITable stringUITable = new StringUITable();

    StringTable[] tables = new StringTable[(int)E_STRING.MAX];
    public StringTable this[int index]
    {
        get { return tables[index]; }
        set { tables[index] = value; }
    }
    public string GetStr(E_STRING e, int ID) { return this[(int)e].GetString(ID); }

    protected string address = "Table";
    bool _init = false;
    public override void Init()
    {
        if (_init) return;
        _init = true;

        tables[0] = stringTable;
        tables[1] = stringUITable;


        //if (GAddressablesManager.i.LoadPackedAssets(address))
        //{
            Debug.Log("string manager");
            stringTable.Load();
            stringUITable.Load();
        //}

    }
    public class LocalTable : StringTable
    {
        private string assetName = "Table/LocalTable";
        public override void Load()
        {
       
        }
    }

    public class StringTable
    {
        public Dictionary<int, string> _dicData = new Dictionary<int, string>();
        public string GetString(int ID)
        {
            return _dicData.ContainsKey(ID) ? _dicData[ID] : null;
        }

        protected string address = "Table";
        private string assetName = "StringTable";
        public virtual void Load()
        {
         
        }
    }
    public class StringUITable : StringTable
    {
        private string assetName = "StringUITable";
        public override void Load()
        {
         
        }
    }
 
}
