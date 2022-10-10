using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using Photon.Realtime;
using Photon.Pun;

[Serializable]
public struct UserData
{
    public string Name;
    public string Money;
    public string EXP;
    public string Level;
}
[Serializable]
public struct ExpTotalData
{
    public string Level;
    public string Exp;
}

public class UserDataController : MonoSingleton<UserDataController>
{
    public UserData userData1;
    public string userDatastr;
    public List<ExpTotalData> expTotalDataList;
    private void Start()
    {
		List<Dictionary<string, object>> data = CSVReader.Read("UserExpData");
        ExpTotalData expdata = new ExpTotalData();
        for (var i = 0; i < data.Count; i++)
        {
            expdata.Level = data[i]["Level"].ToString();
            expdata.Exp = data[i]["Exp"].ToString();
            expTotalDataList.Add(expdata);
        }
        ///
        /// 테스트 용 코드
        ///
        UserDataInit("netrogold", "Sjh011009!");
    }
    public void CreateNewKey(string _id ,string _key)
    {
        UserData userData = new UserData();
        userData.Name = _id;
        userData.Money = "0";
        userData.EXP = "0";
        userData.Level = "1";
        string json = JsonUtility.ToJson(userData);
        string EncryptData = EncryptClass.Encrypt(json, _key);

        File.WriteAllText(Application.dataPath+"/UserData"+"/"+_id+"Data.json", EncryptData);
    }
    public void UserDataInit(string _id ,string _key)
    {
        string UserData = File.ReadAllText(Application.dataPath + "/UserData" + "/" + _id + "Data.json");

        string DecryptData = EncryptClass.Decrypt(UserData, _key);
        userDatastr = DecryptData;
        userData1 = JsonUtility.FromJson<UserData>(DecryptData);
        SendToNetWorkManager();
        //GSceneManager.i.MoveSceneAsync(GSceneManager.SCENE_TYPE.MainMenu);
    }


    public void SendToNetWorkManager()
    {
        LobbyNetwork.i.PlayerListBinding(userDatastr);
    }
}
