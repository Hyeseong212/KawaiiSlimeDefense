using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StringTableScriptableObject : BaseScriptableObject
{
/*#if UNITY_EDITOR
    [MenuItem("Assets/Create Scriptable/InfoData Scriptable")]
    public static void CreateSelectScriptable()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);

        string folder = EditorUtility.SaveFilePanel("Save Resource", path, "InfoData", "asset");
        if (folder.Length == 0)
        {
            return;
        }

        path = "Assets"+ folder.Substring(Application.dataPath.Length);

        ScriptableObject scriptable = CreateInstance("InfoDataScriptableObject");
        AssetDatabase.CreateAsset(scriptable, path);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    public static ErrorTableScriptableObject GetSelectScriptable()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        return AssetDatabase.LoadAssetAtPath<ErrorTableScriptableObject>
                               (path + "/ErrorTable.asset");
    }
#endif*/
    public override void SetData(string jsonArray)
    {
        DATA data = JsonUtility.FromJson<DATA>(jsonArray);
        datas = data.datas;
    }

	[System.Serializable]
    public class DATA
    {
        public StringTable[] datas;
    }
	
    [System.Serializable]
    public class StringTable
    {
		
		public int ID;
		public string TEXT;
    }
	

    public StringTable[] datas;
}
