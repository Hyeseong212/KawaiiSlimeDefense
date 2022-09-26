using System;
using UnityEngine;
[Serializable]
public class Options
{
    public PlayerNumber Player;
    public float mapMoveSpeed;
    public float width;
    public float height;
    public float cameraYvalue;
    public bool isPopup = false;
    public int Difficulty = 0;
}
public class GlobalOptions : MonoSingleton<GlobalOptions>
{
    public Options options;
}