using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using System.IO;
using OfficeOpenXml;
public partial class ExcelConvert
{
    public class LoadExcel
    {
        public string tablename {
            get { return TableName; }
        }
        public int col { get { return width; } }
        public int row { get { return height; } }
        string TableName = string.Empty;
        int width = 0;
        int height = 0;
        public List<List<string>> listTable = new List<List<string>>();
        public List<string> rows = new List<string>();

        public LoadExcel(ExcelTable table)
        {
            TableName = table.TableName;
            width = table.NumberOfColumns;
            height = table.NumberOfRows;


            bool[] idxs = new bool[table.NumberOfColumns + 1];
            List<string> list = new List<string>();
            int row = 1;
            for (int column = 2; column <= table.NumberOfColumns; column++)
            {
                string s = (string)table.GetValue(row, column);

                idxs[column] = s.Length == 0 || (s[0] == '#');
                if (idxs[column])
                {
                    continue;
                }
                list.Add(s);
            }
            listTable.Add(list);


            for (row = 2; row <= table.NumberOfRows; row++)
            {
                string s = (string)table.GetValue(row, 1);
                if (s.Length>0 && s[0] == '#') continue;
                s = (string)table.GetValue(row, 2);
                if (s.Length == 0) continue;

                list = new List<string>();
                for (int column = 2; column <= table.NumberOfColumns; column++)
                {
                    if (idxs[column])
                    {
                        continue;
                    }
                    s = (string)table.GetValue(row, column);
                    if (column == 1)
                    {
                        rows.Add(s);
                    }
                    else
                    {
                        list.Add(s);
                    }
                }
                listTable.Add(list);
            }
        }
    }


    

    
}
