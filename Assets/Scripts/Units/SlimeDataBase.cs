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
        //this.SlimeMiniMapPosVec3 = Slime.GetComponent<Transform>().position;
        SlimeMiniMapPos = null;
        SlimeMiniMapPosVec2 = new Vector2(0,0);
    }
    public SlimeData(int _index, string _name, string _type, GameObject _slime, GameObject _slimeMiniMapPos)
    {
        this.Index = _index;
        this.Name = _name;
        this.Type = _type;
        this.Slime = _slime;
        //this.SlimeMiniMapPosVec3 = Slime.GetComponent<Transform>().position;
        this.SlimeMiniMapPos = _slimeMiniMapPos;
        SlimeMiniMapPosVec2 = SlimeMiniMapPos.GetComponent<RectTransform>().anchoredPosition;
    }
    public int Index;
    public string Name,Type;
    public GameObject Slime;
    //public Vector3 SlimeMiniMapPosVec3;
    public GameObject SlimeMiniMapPos;
    public Vector2 SlimeMiniMapPosVec2;
}
