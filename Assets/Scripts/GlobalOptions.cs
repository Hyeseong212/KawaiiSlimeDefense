using System;
using UnityEngine;
[Serializable]
public class Options
{
    public float mapMoveSpeed;
    public float width;
    public float height;
}
public class GlobalOptions : MonoSingleton<GlobalOptions>
{
    public Options options;
    private void Start()
    {
    }
}