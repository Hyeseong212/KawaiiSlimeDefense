using System;
using UnityEngine;
[Serializable]
public struct SlimeData 
{
 
    public SlimeData(int _index, string _name, string _type, string _attackType, float _attackpts, float _attackspeed, GameObject _slime)
    {
        this.Index = _index;
        this.Name = _name;
        this.Type = _type;
        this.Slime = _slime;
        this.attackType = _attackType;
        this.attackpts = _attackpts;
        this.attackspeed = _attackspeed;
    }//Pos�� ������ ������ ������ ����ü ������
    public SlimeData(int _index, string _name, string _type, GameObject _slime, string _attackType, float _attackpts, float _attackspeed)
    {
        this.Index = _index;
        this.Name = _name;
        this.Type = _type;
        this.Slime = _slime;
        this.attackType = _attackType;
        this.attackpts = _attackpts;
        this.attackspeed = _attackspeed;
    }//��� ����ü ������ ������

    public int Index;
    public string Name,Type;
    public GameObject Slime;
    public string attackType;
    public float attackpts;
    public float attackspeed;
}

