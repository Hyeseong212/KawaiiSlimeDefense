using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using System.IO;
using OfficeOpenXml;
public partial class ExcelConvert
{
    

    //data
    //json
    List<ExcelDataConvert> listData = new List<ExcelDataConvert>();
    public string excelName { get; set; }
    public void LoadExcelData(string path)
    {
        listData.Clear();

        FileInfo file = new FileInfo(path);
        ExcelPackage ep = new ExcelPackage(file);
        Excel xls = new Excel(ep.Workbook);

        Debug.Log("excel file : " + ep.File.Name);
        excelName = ep.File.Name.Replace(".xlsx","");

        listData.Add(new ExcelDataConvert(xls.Tables, excelName));        
    }
    public void CreateScriptable(string folder)
    {
        foreach(ExcelDataConvert convert in listData)
        {
            convert.CreateScriptable(folder);
        }
    }
    public void SaveData(string folder)
    {
        if (listData.Count == 0)
        {
            Debug.LogError("ExelConvert:SaveData listData.Count == 0, Stop Save");
            return;
        }

        foreach (ExcelDataConvert convert in listData)
        {
            convert.Save(folder);
        }
    }



    public class ExcelDataConvert
    {
        static TextAsset textasset;
        static void CheckParserText()
        {
            if (textasset == null || textasset.text.Length == 0)
            {
                string[] str2 = AssetDatabase.GetAllAssetPaths();
                foreach (string path in str2)
                {
                    string txtName = "scriptable.txt";
                    string res = path.Length > txtName.Length ? path.Substring(path.Length-txtName.Length) : string.Empty;
                    if (res == "scriptable.txt")
                    {
                        textasset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                    }
                }
            }
        }

        public string dataName { get { return _dataName; } }
        string _dataName;
        ExcelJsonConvert json_convert = null;
        public ExcelDataConvert(List<ExcelTable> tables, string Name)
        {
            json_convert = new ExcelJsonConvert(tables);
            _dataName = Name;
            
        }

        public void CreateScriptable(string folder)
        {
            if (json_convert.listJson.Count == 0)
            {
                Debug.LogWarning("json_convert.listJson.Count == 0");
                return;
            }
            CheckParserText();


            foreach(JsonConvert con in json_convert.listJson)
            {
                CreateScriptable(con, folder);
            }
        }

        void GetClassData(ref string tex, JsonConvert convert, string srcClass)
        {
            //class
            string str = srcClass.Replace("BaseData", convert.tablename);
            string[] array = str.Split(new string[1] { "//" }, System.StringSplitOptions.None);
            if (array.Length < 3)
            {
                return;
            }

            string tab = array[1];
            array[1] = string.Empty;
            foreach (ColumType colum in convert.colums)
            {
                if (colum.type == _TYPE.DataClass)
                {
                    continue;
                }
                if (colum.type == _TYPE.Data)
                {
                    JsonConvert con = json_convert.listArray.Find(I => I.tablename == colum.subType.key);

                    if (con.colums[0].type == _TYPE.DataClass)
                    {
                        array[1] += tab + string.Format("public {0} [] {1};", GetType(colum), colum.key);
                    }
                    else if (con.colums[0].type == _TYPE.DataArray)
                    {
                        array[1] += tab + string.Format("public {0} [] {1};", GetType(con.colums[1]), colum.key);
                    }
                }
                else
                {
                    array[1] += tab + string.Format("public {0} {1};", GetType(colum), colum.key);
                }

            }
            foreach (string s in array)
            {
                tex += s;
            }
            tex += "\n";

            //subclass
            foreach (ColumType colum in convert.colums)
            {
                if (colum.type != _TYPE.Data)
                {
                    continue;
                }
                JsonConvert con = json_convert.listArray.Find(I => I.tablename == colum.subType.key);
                if (con == null || con.colums[0].type!=_TYPE.DataClass)
                {
                    continue;
                }
                
                GetClassData(ref tex, con, srcClass);
            }
        }
        public void CreateScriptable(JsonConvert convert, string folder)
        {
            string file = string.Format("{0}/{1}ScriptableObject.cs", folder, convert.tablename);
            if( Directory.Exists(file) )
            {
                Directory.Delete(file);
            }

            string[] r = textasset.text.Split( new string[1]{ "//class", } , System.StringSplitOptions.None);

            if(r.Length<3)
            {
                return;
            }

            r[0] = r[0].Replace("BaseData", convert.tablename);
            r[2] = r[2].Replace("BaseData", convert.tablename);


            string srcClass = r[1];
            r[1] = string.Empty;

            //class
            GetClassData(ref r[1], convert, srcClass);


            //
            string src = string.Empty;
            foreach (string s in r)
            {
                src += s;
            }

            string filePath = string.Format("{0}/{1}ScriptableObject.cs", folder, convert.tablename);
            File.WriteAllText(filePath, src, System.Text.Encoding.UTF8);

            Debug.Log("Create Scriptable : " + file);
        }

        string GetType(ColumType colum)
        {
            string stype = string.Empty;
            switch(colum.type)
            {
                case _TYPE.NULL: break;
                case _TYPE.String: 
                case _TYPE.Int: 
                case _TYPE.Long: 
                case _TYPE.Float: 
                case _TYPE.Double: 
                case _TYPE.Bool: stype = colum.type.ToString().ToLower(); break;
                case _TYPE.Array: stype = colum.subType.type.ToString().ToLower()+" []"; break;
                case _TYPE.Vector2: stype = "Vector2"; break;
                case _TYPE.Vector3: stype = "Vector3"; break;
                case _TYPE.Enum:
                    {
                        stype = colum.subType.key;
                    }
                    break;
                case _TYPE.Data:
                    {
                        stype = colum.subType.key;
                    }
                    break;
            }
            return stype;
        }

        public void Save(string folder)
        {
            if (json_convert.listJson.Count == 0)
            {
                return;
            }

 
            foreach(JsonConvert convert in json_convert.listJson)
            {
                string scriptableName = string.Format("{0}ScriptableObject", convert.tablename);

                BaseScriptableObject scriptable = (BaseScriptableObject)ScriptableObject.CreateInstance(scriptableName);

                scriptable.SetData("{ \"datas\":" + convert.ToString(json_convert.listArray) + "}");

                AssetDatabase.CreateAsset(scriptable, string.Format("{0}/{1}.asset", folder, convert.tablename));
            }
            
        }
    }
}