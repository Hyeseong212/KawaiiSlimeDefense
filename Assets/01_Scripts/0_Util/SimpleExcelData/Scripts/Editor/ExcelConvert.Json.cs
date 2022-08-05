using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.IO;
using OfficeOpenXml;
public partial class ExcelConvert
{
    
    //json
    ExcelJsonConvert jsonconvert = null;
    public void LoadExcelJson(string path)
    {
        FileInfo file = new FileInfo(path);
        ExcelPackage ep = new ExcelPackage(file);
        Excel xls = new Excel(ep.Workbook);

        jsonconvert = new ExcelJsonConvert(xls.Tables);
    }
    public void SaveUnitJson(string folder)
    {
        jsonconvert.SaveJson(folder);
    }
    public void SaveJson(string folder)
    {
        jsonconvert.SaveJson(folder);
    }
    public enum _TYPE
    {
        NULL,
        String,
        Int,
        Long,
        Float,
        Double,
        Bool,
        Array,
        Vector2,
        Vector3,
        Enum,
        Data,   //sheet
        DataClass,
        DataArray,
    }
    public class ColumType
    {
        public string key = string.Empty;
        public _TYPE type = _TYPE.String;
        public ColumType subType = null;

        public ColumType()
        {
            key = string.Empty;
            type = _TYPE.NULL;
        }
        public ColumType(string k, string v, _TYPE t)
        {
            key = k;
            type = t;
        }
        public void Set(string k, string v, _TYPE t)
        {
            key = k;
            type = t;
        }
    }
    public class ExcelJsonConvert
    {
        public string tablename { get { return listJson.Count == 0 ? string.Empty : listJson[0].tablename; } }

        public List<JsonConvert> listJson = new List<JsonConvert>();
        public List<JsonConvert> listArray = new List<JsonConvert>();
        public ExcelJsonConvert(List<ExcelTable> tables)
        {
            

            foreach (ExcelTable table in tables)
            {
                if (table.TableName.Length == 0 || table.TableName[0] == '#')
                {
                    continue;
                }
                listJson.Add(new JsonConvert(table));
            }

            foreach(JsonConvert convert in listJson)
            {
                if( convert.colums[0].key=="class"
                    || convert.colums[0].key == "array")
                {
                    listArray.Add(convert);
                }
            }
            foreach (JsonConvert convert in listArray)
            {
                listJson.Remove(convert);
            }
        }
        public void SaveUnitJson(string folder)
        {
            if (listJson.Count == 0)
            {
                Debug.LogError("ExelConvert:SaveJson listTable.Count == 0, Stop Save");
                return;
            }

            foreach (JsonConvert convert in listJson)
            {
                string file = string.Format("{0}/{1}.txt", folder, convert.tablename);
                File.WriteAllText(file, convert.ToString(), System.Text.Encoding.UTF8);

                Debug.Log("Save Json : " + file);
            }
            foreach (JsonConvert convert in listArray)
            {
                string file = string.Format("{0}/{1}.txt", folder, convert.tablename);
                File.WriteAllText(file, convert.ToString(), System.Text.Encoding.UTF8);

                Debug.Log("Save Json : " + file);
            }
        }
        public void SaveJson(string folder)
        {
            if (listJson.Count == 0)
            {
                Debug.LogError("ExelConvert:SaveJson listTable.Count == 0, Stop Save");
                return;
            }

            foreach (JsonConvert convert in listJson)
            {
                string file = string.Format("{0}/{1}.txt", folder, convert.tablename);
                File.WriteAllText(file, convert.ToString(listArray), System.Text.Encoding.UTF8);

                Debug.Log("Save Json : " + file);
            }
        }


        JArray ToArray(JsonConvert convert)
        {
            JArray array = convert.ToArray(listArray);
            
            return array;
        }
    }
    public class JsonConvert
    {
        public string tablename { get { return load.tablename; } }
        LoadExcel load;
        public ColumType[] colums = null;
        public JsonConvert(ExcelTable table)
        {
            load = new LoadExcel(table);


            List<string> list = load.listTable[0];

            List<ColumType> listData = new List<ColumType>();
            
            foreach(string s in list)
            {
                ColumType data = new ColumType();
                data.key = s;
                //
                listData.Add(data);
            }
            colums = listData.ToArray();

            string key = colums[0].key.ToLower();
            if (key == "class")
            {
                colums[0].type = _TYPE.DataClass;
            }else if(key == "array")
            {
                colums[0].type = _TYPE.DataArray;
            }

            
            list = load.listTable[1];

            string[] arr = list.ToArray();
            for (int i = 0; i < arr.Length; ++i)
            {
                string s = arr[i];
                string stype = string.IsNullOrEmpty(s) ? "string" : s.ToLower();

                ColumType colum = colums[i];

                if(i==0 && (colum.type == _TYPE.DataClass || colum.type == _TYPE.DataArray) )
                {
                    continue;
                }

                if (stype.Contains("array"))
                {
                    string[] r = s.Split(";".ToCharArray());

                    colum.type = _TYPE.Array;

                    colum.subType = new ColumType();
                    colum = colum.subType;
                    
                    if (r.Length > 1)
                    {
                        stype = r[1];
                    }
                }
                //
                if (stype.Contains("data"))
                {
                    string[] r = s.Split(";".ToCharArray());

                    colum.type = _TYPE.Data;
                    colum.subType = new ColumType();

                    colum.subType.type = _TYPE.String;
                    colum.subType.key = r.Length > 1 ? r[1] : string.Empty;
                }
                if (stype.Contains("enum"))
                {
                    string[] r = s.Split(";".ToCharArray());

                    colum.type = _TYPE.Enum;
                    colum.subType = new ColumType();

                    colum.subType.type = _TYPE.Int;
                    colum.subType.key = r.Length > 1 ? r[1] : string.Empty;
                }
                else if (stype == "vector2")
                {
                    colum.type = _TYPE.Vector2;
                }
                else if (stype == "vector3")
                {
                    colum.type = _TYPE.Vector3;
                }
                else if (stype == "string")
                {
                    colum.type = _TYPE.String;
                }
                else if (stype == "int")
                {
                    colum.type = _TYPE.Int;
                }
                else if (stype == "long")
                {
                    colum.type = _TYPE.Long;
                }
                else if (stype == "float" )
                {
                    colum.type = _TYPE.Float;
                }
                else if (stype == "double")
                {
                    colum.type = _TYPE.Double;
                }
                else if (stype == "bool")
                {
                    colum.type = _TYPE.Bool;
                }
            }
        }

        public override string ToString() 
        {

            string json = string.Empty;

            if( colums[0].type == _TYPE.DataClass
                || colums[0].type == _TYPE.DataArray)
            {
                json = ToObject().ToString();
            }
            else
            {
                json = ToArray().ToString();
            }

            return json;
        }
        public string ToString(List<JsonConvert> listDat)
        {
            return ToArray(listDat).ToString();
        }


        public JArray ToArray(List<JsonConvert> listData)
        {
            List<List<string>> list = load.listTable.GetRange(2, load.listTable.Count - 2);

            JArray array = new JArray();
            List<List<string>>.Enumerator row_e = list.GetEnumerator();
            while (row_e.MoveNext())
            {
                JObject objClass = new JObject();
                
                string ID = string.Empty;
                string[] r = row_e.Current.ToArray();

                for (int i=0; i<r.Length;++i)
                {
                    string colum = r[i];
                    if (colums[i].type == _TYPE.Data)
                    {
                        JsonConvert data = listData.Find(I => I.tablename == colums[i].subType.key);
                        if(data!=null)
                        {
                            Dictionary<string, JArray> dic = data.GetDataArray();
                            if (dic!=null && dic.ContainsKey(colum))
                            {
                                objClass.Add(colums[i].key, dic[colum]);
                                continue;
                            }
                        }
                        objClass.Add(colums[i].key, new JArray());
                    }
                    else
                    {
                        AddObjectType(ref objClass, colum, i);
                    }
                }
                array.Add(objClass);
            }
            return array;
        }
        Dictionary<string, JArray> dicArray =null;
        public Dictionary<string, JArray> GetDataArray()
        {
            if(dicArray==null)
            {
                List<List<string>> list = load.listTable.GetRange(2, load.listTable.Count - 2);

                dicArray = new Dictionary<string, JArray>();

                List<List<string>>.Enumerator row_e = list.GetEnumerator();
                while (row_e.MoveNext())
                {
                    if (colums[0].type == _TYPE.DataClass)
                    {
                        JObject objClass = new JObject();
                        int i = 0;
                        string ID = string.Empty;
                        foreach (string colum in row_e.Current)
                        {
                            if (i == 0)
                            {
                                ID = colum;
                            }
                            else
                            {
                                AddObjectType(ref objClass, colum, i);

                            }
                            //
                            ++i;
                        }
                        if (!dicArray.ContainsKey(ID))
                        {
                            dicArray.Add(ID, new JArray());
                        }
                        dicArray[ID].Add(objClass);
                    }
                    else if (colums[0].type == _TYPE.DataArray)
                    {
                        JArray array = null;
                        int i = 0;
                        string ID = string.Empty;
                        foreach (string colum in row_e.Current)
                        {
                            if (i == 0)
                            {
                                ID = colum;
                                if (!dicArray.ContainsKey(ID))
                                {
                                    dicArray.Add(ID, new JArray());
                                }
                                array = dicArray[ID];
                            }
                            else
                            {
                                if(string.IsNullOrEmpty(colum))
                                {
                                    break;
                                }
                                AddArrayType(ref array, colum, i);

                            }
                            //
                            ++i;
                        }
                    }
                }
            }
            return dicArray;
        }
        

        //
        public JArray ToArray()
        {
            List<List<string>> list = load.listTable.GetRange(2, load.listTable.Count - 2);

            JArray array = new JArray();
            List<List<string>>.Enumerator row_e = list.GetEnumerator();
            while (row_e.MoveNext())
            {
                JObject obj = GetObjects(row_e.Current, 0);
                array.Add(obj);
            }
            return array;
        }
        public JObject ToObject()
        {
            JObject jobj = new JObject();

            Dictionary<string, JArray> dic = GetDataArray();
 
            foreach( var kyp in dic)
            {
                jobj.Add(kyp.Key, kyp.Value);
            }

            return jobj;
        }
        JObject GetObjects(List<string> rows, int i)
        {
            JObject jobj = new JObject();
            foreach (string colum in rows)
            {
                AddObjectType(ref jobj, colum, i);
                //
                ++i;
            }
            return jobj;
        }
        void AddObjectType(ref JObject jobj ,string colum , int i)
        {
            switch (colums[i].type)
            {
                case _TYPE.NULL: break;
                case _TYPE.String: jobj.Add(colums[i].key, colum); break;
                case _TYPE.Int: jobj.Add(colums[i].key, int.Parse(colum)); break;
                case _TYPE.Long: jobj.Add(colums[i].key, long.Parse(colum)); break;
                case _TYPE.Float: jobj.Add(colums[i].key, float.Parse(colum)); break;
                case _TYPE.Double: jobj.Add(colums[i].key, double.Parse(colum)); break;
                case _TYPE.Bool: jobj.Add(colums[i].key, bool.Parse(colum)); break;
                case _TYPE.Array:
                    {
                        jobj.Add(colums[i].key, JArray.Parse(colum));
                    }
                    break;
                case _TYPE.Enum:
                    {
                        jobj.Add(colums[i].key, colum);
                    }
                    break;
                case _TYPE.Vector2:
                    {
                        JArray a = JArray.Parse(colum);
                        Vector2 v = new Vector2(a[0].ToObject<float>(), a[1].ToObject<float>());
                        JObject point = new JObject();
                        point.Add("x", v.x);
                        point.Add("y", v.y);
                        jobj.Add(colums[i].key, point);
                    }
                    break;
                case _TYPE.Vector3:
                    {
                        JArray a = JArray.Parse(colum);
                        Vector3 v = new Vector3(a[0].ToObject<float>(), a[1].ToObject<float>(), a[2].ToObject<float>());
                        JObject point = new JObject();
                        point.Add("x", v.x);
                        point.Add("y", v.y);
                        point.Add("z", v.z);
                        jobj.Add(colums[i].key, point);
                    }
                    break;
                case _TYPE.Data: jobj.Add(colums[i].key, colum); break;
            }
        }
        void AddArrayType(ref JArray jarray, string colum, int i)
        {
            switch (colums[i].type)
            {
                case _TYPE.NULL: break;
                case _TYPE.String: jarray.Add(colum); break;
                case _TYPE.Int: jarray.Add(int.Parse(colum)); break;
                case _TYPE.Long: jarray.Add(long.Parse(colum)); break;
                case _TYPE.Float: jarray.Add(float.Parse(colum)); break;
                case _TYPE.Double: jarray.Add( double.Parse(colum)); break;
                case _TYPE.Bool: jarray.Add( bool.Parse(colum)); break;
                case _TYPE.Array:
                    {
                        jarray.Add(JArray.Parse(colum));
                    }
                    break;
                case _TYPE.Vector2:
                    {
                        JArray a = JArray.Parse(colum);
                        Vector2 v = new Vector2(a[0].ToObject<float>(), a[1].ToObject<float>());
                        JObject point = new JObject();
                        point.Add("x", v.x);
                        point.Add("y", v.y);
                        jarray.Add(point);
                    }
                    break;
                case _TYPE.Vector3:
                    {
                        JArray a = JArray.Parse(colum);
                        Vector3 v = new Vector3(a[0].ToObject<float>(), a[1].ToObject<float>(), a[2].ToObject<float>());
                        JObject point = new JObject();
                        point.Add("x", v.x);
                        point.Add("y", v.y);
                        point.Add("z", v.z);
                        jarray.Add(point);
                    }
                    break;
                case _TYPE.Data: jarray.Add(colum); break;
            }
        }

    }
}
