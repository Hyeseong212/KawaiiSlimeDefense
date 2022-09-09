using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using _type;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(PopupManager))]
[CanEditMultipleObjects]
public class PopupManagerEditor : Editor
{

    PopupManager _this;
    string[] layers;
    void OnEnable()
    {
        _this = (PopupManager)target;

        layers = new string[SortingLayer.layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i] = SortingLayer.layers[i].name;
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = _this.useSortingLayer;
        _this.layer = EditorGUILayout.Popup("Sorting Layer", _this.layer, layers);
        _this.order = EditorGUILayout.IntField("Order in layer", _this.order);
        GUI.enabled = true;
    }
}

#endif
public class PopupManager : MonoSingleton<PopupManager>
{

    static public bool mouseOn = false;


    public class PopupItem
    {
        public E_POPUP type = E_POPUP.NONE;
        public GameObject objPopup = null;
        public object data = null;
        public bool bEscapeLock = false;
    }
    public string Popup_Path = "UI/Popup/";
    public Transform window = null;
    public bool useSortingLayer = false;
    [HideInInspector] public int layer = 0;
    [HideInInspector] public int order = 0;


    Stack<PopupItem> stackPopup = new Stack<PopupItem>();
    public bool Empty { get { return stackPopup.Count == 0; } }
    [HideInInspector] public E_POPUP selectType = E_POPUP.NONE;
    public int Count { get { return stackPopup.Count; } }
    public bool isPopup { get { return Count > 0; } }

    CanvasScaler scaler = null;
    public Vector2 resolution { get { return scaler.referenceResolution; } }
    RectTransform rt_win;

    public override void Init()
    {
        if (window == null)
        {
            //window = transform;
            window = (RectTransform)transform;
        }
    }
    // Use this for initialization
    void Start()
    {
        rt_win = GetComponent<RectTransform>();

    }
    bool _escape = false;
#if UNITY_ANDROID
    bool _escapeOnce = true;
    IEnumerator PopupCoolTime(float cool)
    {
        while (cool > 0.1f)
        {
            cool -= Time.deltaTime;
            if(cool < 0.1f)
            {
                _escapeOnce = true;
            }
            yield return new WaitForFixedUpdate();
        }
        
    }
#endif
    private void Update()
    {
#if UNITY_ANDROID
        if (UnityPluginManager.i.patchIsDone)
        {
            if (Keyboard.current.escapeKey.isPressed && !_escape)
            {
                if (stackPopup.Count > 0)
                {
                    PopupItem item = stackPopup.Peek();
                    if (!item.bEscapeLock && _escapeOnce)
                    {
                        _escapeOnce = false;
                        StartCoroutine(PopupCoolTime(0.5f));
                        BackPopup();
                    }
                }
                else
                {
                    /*E_POPUP popup = GSceneManager.i.scene_type == GSceneManager.SCENE_TYPE.World ? E_POPUP.POPUP_WORLD_MENU : E_POPUP.POPUP_MAIN_MENU;
                    if (!PopupManager.i.IsPopup(popup))
                        PopupManager.i.ShowPopup(popup);*/
                }
            }
        }
#else 
        if (Keyboard.current.escapeKey.isPressed && !_escape)
        {
            if (stackPopup.Count > 0)
            {
                PopupItem item = stackPopup.Peek();
                if (!item.bEscapeLock)
                {
                    BackPopup();
                }
            }
            else
            {
                /*E_POPUP popup = GSceneManager.i.scene_type == GSceneManager.SCENE_TYPE.World ? E_POPUP.POPUP_WORLD_MENU : E_POPUP.POPUP_MAIN_MENU;
                if (!PopupManager.i.IsPopup(popup))
                    PopupManager.i.ShowPopup(popup);*/
            }
        }
        _escape = Keyboard.current.escapeKey.isPressed;
#endif
        /*
        if( Input.GetKeyDown(KeyCode.Escape))
        {
            PopupItem item = stackPopup.Peek();
            if( !item.bEscapeLock)
            {
                BackPopup();
            }
        }*/

    }
    public bool IsPopup(E_POPUP type)
    {
        bool value = false;
        foreach (PopupItem item in stackPopup)
        {
            if (item.type != type) continue;

            value = true;
            break;
        }
        return value;
    }
    public PopupItem GetPopupItem(E_POPUP type)
    {
        PopupItem popupitem = null;
        foreach (PopupItem item in stackPopup)
        {
            if (item.type != type) continue;

            popupitem = item;
            break;
        }
        return popupitem;
    }
    public GameObject GetPopupObject(E_POPUP type)
    {
        PopupItem item = GetPopupItem(type);
        return item == null ? null : item.objPopup;
    }
    public T GetPopupData<T>(E_POPUP type) where T : Object
    {
        PopupItem item = GetPopupItem(type);
        return (item == null ? null : (T)item.data);
    }
    void RemovePopup(E_POPUP type)
    {
        Stack<PopupItem> stack = new Stack<PopupItem>();
        while (stackPopup.Count > 0)
        {
            PopupItem item = stackPopup.Pop();
            if (item.type == type)
            {
                break;
            }
            stack.Push(item);
        }
        while (stack.Count > 0)
        {
            PopupItem item = stack.Pop();
            if (item == null) continue;
            stackPopup.Push(item);
        }
    }
    void RemovePopup(GameObject obj)
    {
        Stack<PopupItem> stack = new Stack<PopupItem>();
        while (stackPopup.Count > 0)
        {
            PopupItem item = stackPopup.Pop();
            if (item.objPopup == obj)
            {
                break;
            }
            stack.Push(item);
        }
        while (stack.Count > 0)
        {
            PopupItem item = stack.Pop();
            if (item == null) continue;
            stackPopup.Push(item);
        }
    }

    public void ShowPopup()
    {
        ShowPopup(selectType);
    }

    public T ShowPopup<T>(E_POPUP type, bool bNew = false, bool bEscapeLock = false, object data = null)
    {
        bEscapeLock = false;
        GameObject obj = ShowPopup(type, bNew, bEscapeLock, data);
        return obj.GetComponent<T>();
    }
    public GameObject ShowPopup(E_POPUP type, bool bNew = false, bool bEscapeLock = false, object data = null)
    {
        GlobalOptions.i.options.isPopup = true;
        GameObject obj = null;


        Vector3 localPosition = Vector3.zero;
        PopupItem item = bNew ? null : GetPopupItem(type);
        if (item != null)
        {
            obj = item.objPopup;
            RemovePopup(type);
        }
        else
        {
            GameObject newobj = Resources.Load<GameObject>(Popup_Path + type.ToString());
            obj = GameObject.Instantiate(newobj, newobj.transform.localPosition, Quaternion.identity);
            localPosition = newobj.transform.localPosition;
        }
        if (obj == null) return null;

        item = new PopupItem();
        item.type = type;
        item.objPopup = obj;
        item.data = data;
        item.bEscapeLock = bEscapeLock;
        RectTransform rect = obj.GetComponent<RectTransform>();
        if (rect != null)
        {
            Vector2 min = rect.offsetMin;
            Vector2 max = rect.offsetMax;

            rect.SetParent(window);
            if (rt_win != null)
            {
                rect.anchoredPosition = rt_win.anchoredPosition;
            }
            rect.localScale = Vector3.one;
            rect.offsetMin = min;
            rect.offsetMax = max;
        }
        else
        {
            Transform tr = obj.GetComponent<Transform>();
            tr.parent = window;
            tr.localScale = Vector3.one;
            tr.localPosition = localPosition;
        }

        if (useSortingLayer)
        {
            Canvas canvas = item.objPopup.GetComponent<Canvas>();
            if (canvas == null)
            {
                canvas = item.objPopup.AddComponent<Canvas>();
            }
            canvas.overrideSorting = true;
            canvas.sortingLayerID = layer;
            canvas.sortingOrder = order;
        }

        stackPopup.Push(item);
        obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, 0);
        return obj;
    }

    public void BackPopup()
    {
        GlobalOptions.i.options.isPopup = false;
        if (stackPopup.Count == 0) return;

        PopupItem item = stackPopup.Pop();

        item.objPopup.transform.SetParent(null);
        GameObject.DestroyImmediate(item.objPopup);

        // if (stackPopup.Count == 0) ActiveListManager.SetActiveList(true);   

    }
    public void BackPopup(E_POPUP type)
    {
        GlobalOptions.i.options.isPopup = false;
        //Debug.Log("타입 " + type);
        if (stackPopup.Count == 0) return;

        PopupItem item = GetPopupItem(type);
        RemovePopup(type);
        if (item != null)
        {
            //Debug.Log("팝업삭제 " + type);
            GameObject.DestroyImmediate(item.objPopup);
        }
        // if (stackPopup.Count == 0) ActiveListManager.SetActiveList(true);
    }
    public void BackPopup(GameObject obj)
    {
        GlobalOptions.i.options.isPopup = false;
        if (stackPopup.Count == 0) return;

        if (obj != null)
        {
            RemovePopup(obj);
            GameObject.DestroyImmediate(obj);
        }
        //  if (stackPopup.Count == 0) ActiveListManager.SetActiveList(true);
    }
    public void BackPopupAsync(E_POPUP type, float time)
    {
        if (stackPopup.Count == 0)
        {
            return;
        }
        if (time <= 0)
        {
            BackPopup(type);
        }
        else
        {
            StartCoroutine(coroutineBack(type, time));
        }
    }
    IEnumerator coroutineBack(E_POPUP type, float time)
    {
        yield return new WaitForSeconds(time);

        BackPopup(type);
    }
}

