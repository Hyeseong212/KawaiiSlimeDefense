using System;
using UnityEngine;
[Serializable]
public struct SlimeData 
{
    public SlimeData(int _index, string _name, string _type, GameObject _slime)
    {
        this.Index = _index;
        this.Name = _name;
        this.Type = _type;
        this.Slime = _slime;
    }
    public int Index;
    public string Name,Type;
    public GameObject Slime;
}
