using System;
using UnityEngine;
[Serializable]
public class Options
{
    public float mapMoveSpeed;
    public float width;
    public float height;
    public float cameraYvalue;
    public bool isPopup = false;
}
public class GlobalOptions : MonoSingleton<GlobalOptions>
{
    public Options options;
}