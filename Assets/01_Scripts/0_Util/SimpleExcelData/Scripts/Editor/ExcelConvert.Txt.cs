using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;

using OfficeOpenXml;
public partial class ExcelConvert
{
    //
    
    List<ExelTxtConvert> listTxt = new List<ExelTxtConvert>();
    public void LoadExcelTxt(string path)
    {
        listTxt.Clear();

        FileInfo file = new FileInfo(path);
        ExcelPackage ep = new ExcelPackage(file);
        Excel xls = new Excel(ep.Workbook);

        foreach (ExcelTable table in xls.Tables)
        {
            if (table.TableName.Length == 0 || table.TableName[0] == '#')
            {
                continue;
            }

            listTxt.Add(new ExelTxtConvert(table));
        }
    }
    public void SaveTxt(string folder)
    {
        if (listTxt.Count == 0)
        {
            Debug.LogError("ExelConvert:SaveTxt listTable.Count == 0, Stop Save");
            return;
        }
        foreach (ExelTxtConvert convert in listTxt)
        {
            string file = string.Format("{0}/{1}.txt", folder, convert.tablename);
            try
            {
                File.WriteAllText(file, convert.ToString(), System.Text.Encoding.UTF8);
                Debug.Log("Save Txt : " + file);
            }
            catch (System.Exception ex)
            {
                Debug.Log("SaveTxt error : " + ex.Message);
            }
        }
    }

    //class
    public class ExelTxtConvert
    {
        public string tablename { get { return load.tablename; } }
        LoadExcel load;
        public ExelTxtConvert(ExcelTable table)
        {
            load = new LoadExcel(table);
            
        }

        public override string ToString()
        {
            string table = string.Empty;

            List<List<string>>.Enumerator row_e = load.listTable.GetEnumerator();
            if (row_e.MoveNext())
            {
                table += GetRowTable(row_e.Current);
            }
            while (row_e.MoveNext())
            {
                table += "\n" + GetRowTable(row_e.Current);
            }
            return table;
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
