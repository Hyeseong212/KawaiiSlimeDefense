using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using System.IO;


public partial class ExcelConvertEditor : EditorWindow
{
    static string _exelpath = string.Empty;
    static string folderExelPath  {
        get   {
            if (string.IsNullOrEmpty(_exelpath)) _exelpath = Application.dataPath;
            return _exelpath;
        }
        set { _exelpath = value; }
    }
    //
    static string _txtpath = string.Empty;
    static string folderTxtPath {
        get  {
            if (string.IsNullOrEmpty(_txtpath)) _txtpath = Application.dataPath;
            return _txtpath;
        }
        set { _txtpath = value; }
    }
    [MenuItem("Assets/Simple Excel Data/Exel->Enum")]
    public static void ConvertEnum()
    {
        ConvertEnum(Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets));
    }
    public static void ConvertEnum(Object[] objects)
    {
        if (PlayerPrefs.HasKey("folderTxtPath"))
        {
            folderTxtPath = PlayerPrefs.GetString("folderTxtPath");
        }

        string folder = EditorUtility.SaveFolderPanel("Save Resource", folderTxtPath, "");
        if (folder.Length == 0)
        {
            return;
        }

        List<TextAsset> list = new List<TextAsset>();

        foreach (Object o in objects)
        {
            string path = AssetDatabase.GetAssetPath(o);
            //Debug.Log("Selection file = " + path);
            ExcelConvert convert = new ExcelConvert();
            convert.LoadExcelEnum(path);

            convert.SaveEnum(folder);
        }
        folderTxtPath = folder;
        PlayerPrefs.SetString("folderTxtPath", folderTxtPath);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("SaveEnum .............");
    }

    [MenuItem("Assets/Simple Excel Data/Exel->Txt")]
    public static void ConvertTxt()
    {
        ConvertTxt(Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets));
    }
    public static void ConvertTxt(Object [] objects)
    {
        if (PlayerPrefs.HasKey("folderTxtPath"))
        {
            folderTxtPath = PlayerPrefs.GetString("folderTxtPath");
        }

        string folder = EditorUtility.SaveFolderPanel("Save Resource", folderTxtPath, "");
        if (folder.Length == 0)
        {
            return;
        }

        List<TextAsset> list = new List<TextAsset>();

        foreach (Object o in objects)
        {
            string path = AssetDatabase.GetAssetPath(o);
            //Debug.Log("Selection file = " + path);
            ExcelConvert convert = new ExcelConvert();
            convert.LoadExcelTxt( path );

            convert.SaveTxt(folder);
        }
        folderTxtPath = folder;
        PlayerPrefs.SetString("folderTxtPath", folderTxtPath);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("SaveTxt .............");
    }
    [MenuItem("Assets/Simple Excel Data/Exel->Json")]
    public static void ConvertJson()
    {
        OnConvertJson(Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets));
    }
    public static void OnConvertJson(Object [] objects)
    {
        if (PlayerPrefs.HasKey("folderTxtPath"))
        {
            folderTxtPath = PlayerPrefs.GetString("folderTxtPath");
        }

        string folder = EditorUtility.SaveFolderPanel("Save Resource", folderTxtPath, "");
        if (folder.Length == 0)
        {
            return;
        }
        string data_path = folder.Replace(Application.dataPath, "Assets");

        List<TextAsset> list = new List<TextAsset>();

        foreach (Object o in objects)
        {
            string path = AssetDatabase.GetAssetPath(o);
            //Debug.Log("Selection file = " + path);
            ExcelConvert convert = new ExcelConvert();
            convert.LoadExcelJson(path);

            convert.SaveJson(data_path);
        }
        folderTxtPath = folder;
        PlayerPrefs.SetString("folderTxtPath", folderTxtPath);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("SaveJson .............");
    }


    static List<ExcelConvert> listConvert = null;
    public static void ConvertData(Object[] selection)
    {
        Debug.Log("selection.Length = " + selection.Length.ToString());
        listConvert = new List<ExcelConvert>();

        foreach (Object o in selection)
        {
            string path = AssetDatabase.GetAssetPath(o);
            //Debug.Log("Selection file = " + path);
            ExcelConvert convert = new ExcelConvert();
            convert.LoadExcelData(path);

            listConvert.Add(convert);
        }
    }
    public static void ConvertData()
    {
        Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        ConvertData(selection);
    }
    static System.Action createAction = delegate { };
    public static void CreateScriptable(string folder)
    {
        if(listConvert==null || listConvert.Count==0)
        {
            ConvertData();
        }

        foreach (ExcelConvert convert in listConvert)
        {
            convert.CreateScriptable(folder);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    public static void SaveData(string folder)
    {
        foreach (ExcelConvert convert in listConvert)
        {
            convert.SaveData(folder);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Save Data");
    }

    /*
    [RuntimeInitializeOnLoadMethod]
    static void RuntimeInitializeOnLoadMethod()
    {
        Debug.Log("RuntimeInitializeOnLoadMethod");
    }*/


    /*
    [UnityEditor.Callbacks.DidReloadScripts]
    static void DidReloadScripts()
    {
    }*/
}
