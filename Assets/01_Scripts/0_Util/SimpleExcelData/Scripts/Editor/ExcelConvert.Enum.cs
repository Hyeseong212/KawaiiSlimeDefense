using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;

using OfficeOpenXml;
public partial class ExcelConvert
{
    //
    List<ExelEnumConvert> listEnum = new List<ExelEnumConvert>();
    public void LoadExcelEnum(string path)
    {
        listEnum.Clear();

        FileInfo file = new FileInfo(path);
        ExcelPackage ep = new ExcelPackage(file);
        Excel xls = new Excel(ep.Workbook);

        foreach (ExcelTable table in xls.Tables)
        {
            if (table.TableName.Length == 0 || table.TableName[0] == '#')
            {
                continue;
            }

            listEnum.Add(new ExelEnumConvert(table));
        }
    }
    public void SaveEnum(string folder)
    {
        if (listEnum.Count == 0)
        {
            Debug.LogError("ExelConvert:SaveEnum listTable.Count == 0, Stop Save");
            return;
        }

        foreach (ExelEnumConvert convert in listEnum)
        {
            string file = string.Format("{0}/{1}.cs", folder, convert.tablename);
            string src = convert.ToString();
            try
            {
                //File.WriteAllText(file, src, System.Text.Encoding.UTF8);
                byte[] StrByte = System.Text.Encoding.UTF8.GetBytes(src);
                File.WriteAllBytes(file, StrByte);

                Debug.Log("Save Enum : " + file);
            }
            catch(System.Exception ex)
            {
                Debug.Log("SaveEnum error : " + ex.Message);
            }
        }
    }

    //class
    public class ExelEnumConvert
    {
        public string tablename { get { return load.tablename; } }
        LoadExcel load;
        public ExelEnumConvert(ExcelTable table)
        {
            load = new LoadExcel(table);
            
        }
        /*static string[] texts = new string[3]  {
        "using UnityEngine;\n\nnamespace _type {\n\n",
        "\tpublic enum NAME_TYPE {\n",
        "\n}\n"
        };*/
        static string[] texts = new string[4]  {
        "using UnityEngine;\n\n",
        "public enum NAME_TYPE {\n",
        "}\n",
        ""
        };

        public override string ToString()
        {
            string text = texts[0];

            for (int row = 0; row < load.row; row+=2)
            {
                int col = 0;
                if(load.listTable[col].Count<=row)
                {
                    break;
                }
                string enumName = load.listTable[col][row];
                if(string.IsNullOrEmpty(enumName))
                {
                    break;
                }

                text += texts[1].Replace("NAME_TYPE", enumName);

                col = 1;
                for (; col<load.col; ++col)
                {
                    if(load.listTable.Count<=col)
                    {
                        break;
                    }
                    List<string> list = load.listTable[col];
                    string s = list[row];
                    string n = list.Count<=row+1  ? string.Empty : list[row+1];
                    if(string.IsNullOrEmpty(s))
                    {
                        //text += "\n";
                    }
                    else if(string.IsNullOrEmpty(n))
                    {
                        text += string.Format("\t\t {0},\n", s);
                    }else
                    {
                        text += string.Format("\t\t {0} = {1},\n", s, n);
                    }
                }
                text += texts[2];
            }
            text += texts[3];
            return text;
        }
        string GetRowTable(List<string> list)
        {
            string table = string.Empty;
            List<string>.Enumerator e = list.GetEnumerator();
            if (e.MoveNext())
            {
                table += e.Current;
            }
            while (e.MoveNext())
            {
                table += "\t" + e.Current;
            }
            return table;
        }

    }
}
