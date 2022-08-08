using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(PopupShowButton))]
[CanEditMultipleObjects]
public class PopupShowButtonEditor : Editor
{
    SerializedProperty popupProperty;

    PopupShowButton showbutton;

    void OnEnable()
    {
        showbutton = (PopupShowButton)target;
        popupProperty = serializedObject.FindProperty("POPUP");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(popupProperty);
        serializedObject.ApplyModifiedProperties();
        GUILayout.Space(4);
        if (GUILayout.Button("Show Popup"))
        {
            showbutton.ShowPopup();
        }
        GUILayout.Space(4);
        if (GUILayout.Button("Close Popup"))
        {
            showbutton.Close();
        }
    }
}

#endif

public class PopupShowButton : MonoBehaviour
{
    public _type.E_POPUP POPUP = _type.E_POPUP.NONE;
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(delegate {

                PopupManager.i.ShowPopup(POPUP);
            });
        }
    }
    public void ShowPopup()
    {
        PopupManager.i.ShowPopup(POPUP);
    }
    public void Close()
    {
        PopupManager.i.BackPopup(POPUP);
    }


}
