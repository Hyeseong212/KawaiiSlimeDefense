using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

[Serializable]
public struct UserData
{
    public string Name;
    public string Money;
    public string Level;
}

public class UserDataController : MonoSingleton<UserDataController>
{
    void Start()
    {
        UserData userData = new UserData();
        userData.Name = "123";
        userData.Money = "150";
        userData.Level = "99";
    }
    public void CreateNewKey(string _id ,string _key)
    {
        UserData userData = new UserData();
        userData.Name = _id;
        userData.Money = "0";
        userData.Level = "1";
        string json = JsonUtility.ToJson(userData);
        string EncryptData = EncryptClass.Encrypt(json, _key);

        File.WriteAllText(Application.dataPath+"/UserData"+"/"+_id+"Data.json", EncryptData);
    }
    public void UserDataInit(string _id ,string _key)
    {
        string UserData = File.ReadAllText(Application.dataPath + "/UserData" + "/" + _id + "Data.json");

        string DecryptData = EncryptClass.Decrypt(UserData, _key);
        Debug.Log(DecryptData);
    }
}
