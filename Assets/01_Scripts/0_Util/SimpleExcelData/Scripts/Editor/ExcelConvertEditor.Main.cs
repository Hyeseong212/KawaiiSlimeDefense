using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using System.IO;


public partial class ExcelConvertEditor : EditorWindow
{
    [MenuItem("Tools/Simple Excel Data")]
    static public void ToolExcelConvert()
    {
        ExcelConvertEditor window = EditorWindow.GetWindow<ExcelConvertEditor>("Simple Excel Data");
        window.Show();
    }
    [MenuItem("Assets/Simple Excel Data/ Window")]
    static public void AssetExcelConvert()
    {
        ExcelConvertEditor window = EditorWindow.GetWindow<ExcelConvertEditor>("Simple Excel Data");
        window.Show();
        
        window.DropObjects(Selection.objects);
    }
    static string scripts_path
    {
        get { return PlayerPrefs.HasKey("scripts_path") ? PlayerPrefs.GetString("scripts_path") : Application.dataPath; }
        set { PlayerPrefs.SetString("scripts_path", value); }
    }
    static string data_path
    {
        get { return PlayerPrefs.HasKey("data_path") ?  PlayerPrefs.GetString("data_path") : Application.dataPath; }
        set { PlayerPrefs.SetString("data_path", value); }
    }
    Vector2 scroll = Vector2.zero;
    List<Object> listObject = new List<Object>();
    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("scripts_path"))
        {
            PlayerPrefs.SetString("scripts_path", "Assets/01_Scripts/2_Data");
        }
        if (!PlayerPrefs.HasKey("data_path"))
        {
            PlayerPrefs.SetString("data_path", "Assets/02_GameResources/AssetData/Table");
        }        
    }
    private void OnDisable()
    {
        
    }
    Rect dragRect;
    float height = 40;
    void OnGUI()
    {


        scroll = GUILayout.BeginScrollView(scroll);

        GUI.skin.label.alignment = TextAnchor.MiddleLeft;
        foreach (Object o in listObject)
        {
            GUILayout.BeginHorizontal("Box");

            string path = AssetDatabase.GetAssetPath(o);
            GUILayout.Label(path);
            if( GUILayout.Button("X", EditorStyles.miniButtonRight, GUILayout.Width(24)))
            {
                listObject.Remove(o);
                GUILayout.EndHorizontal();
                break;
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();
        if (Event.current.type == EventType.Repaint)
        {
            dragRect = GUILayoutUtility.GetLastRect();
        }
        if (DragDropGUI(dragRect))
        {
            if (DragAndDrop.objectReferences.Length == 0)
            {
                List<Object> objects = new List<Object>();
                foreach(string path in DragAndDrop.paths)
                {
                    Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
                    if (obj == null) continue;
                    objects.Add(obj);
                }
                if(objects.Count > 0)
                {
                    DragAndDrop.objectReferences = objects.ToArray();
                }
            }
            DropObjects(DragAndDrop.objectReferences);
        }
        if (DragAndDrop.visualMode == DragAndDropVisualMode.Link)
        {
            Color color = Color.gray;
            color.a = 0.2f;
            EditorGUI.DrawRect(dragRect, color);
        }

        GUI.enabled = listObject.Count > 0;

        GUI.skin.button.alignment = TextAnchor.MiddleCenter;

        EditorGUILayout.BeginHorizontal();
        GUILayout.BeginVertical("Box");
        if (GUILayout.Button("Create Scriptable"))
        {
            if (!Directory.Exists(scripts_path)) scripts_path = Application.dataPath;
            string path = EditorUtility.OpenFolderPanel("Scripts Path", scripts_path, "");
            if (path.Length > 7)
            {
                scripts_path = path.Replace(Application.dataPath, "Assets");
                ExcelConvertEditor.ConvertData(listObject.ToArray());
                ExcelConvertEditor.CreateScriptable(scripts_path);
            }
        }
        if (GUILayout.Button("Save Data"))
        {
            if (!Directory.Exists(data_path)) data_path = Application.dataPath;
            string path = EditorUtility.OpenFolderPanel("Scripts Path", data_path, "");
            if (path.Length > 7)
            {
                data_path = path.Replace(Application.dataPath, "Assets");
                ExcelConvertEditor.ConvertData(listObject.ToArray());
                ExcelConvertEditor.SaveData(data_path);
            }
        }
        GUILayout.EndVertical();


        if (Event.current.type == EventType.Repaint)
        {
            Rect rc = GUILayoutUtility.GetLastRect();
            height = rc.height;
        }
        if( GUILayout.Button("Json", GUILayout.Height(height)) )
        {
            OnConvertJson(listObject.ToArray());
        }
        if( GUILayout.Button("Txt", GUILayout.Height(height)) )
        {
            ConvertTxt(listObject.ToArray());
        }
        if (GUILayout.Button("Enum", GUILayout.Height(height)))
        {
            ConvertEnum(listObject.ToArray());
        }
        EditorGUILayout.EndHorizontal();

        GUI.enabled = false;
    }
    void DropObjects( Object [] objects)
    {
        foreach (Object o in objects)
        {
            string path = AssetDatabase.GetAssetPath(o);
            if (Directory.Exists(path))
            {
                if (path[path.Length - 1] != '/')
                {
                    path += "/";
                }
                List<UnityEngine.Object> list = _util.ObjectSelect.ObjectSelects(path, new string[1] { "xlsx" });
                if (list.Count > 0)
                {
                    foreach(Object add in list)
                    {
                        if (listObject.Contains(add))
                            listObject.Remove(add);
                    }
                    
                    listObject.AddRange(list);
                }
            }
            else
            {
                Debug.Log("path : "+path);
                if (path.Substring(path.Length-5) == ".xlsx")
                {
                    if( listObject.Contains(o))
                        listObject.Remove(o);
                    listObject.Add(o);
                }
            }
        }
    }
    bool DragDropGUI(Rect view)
    {
        Vector2 mouseposition = Event.current.mousePosition;
        if (!view.Contains(mouseposition))
        {
            return false;
        }
        if (Event.current.type == EventType.DragExited)
        {

        }
        if (Event.current.type == EventType.DragUpdated)
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Link;
        }
        if (Event.current.type == EventType.DragPerform)
        {
            DragAndDrop.AcceptDrag();

            
            return true;
        }
        return false;
    }
}
