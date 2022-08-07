using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddresablesDataManager : MonoSingleton<AddresablesDataManager>
{
    AsyncOperationHandle Handle;
    public Dictionary<int, string> _dicData = new Dictionary<int, string>();
    private void Awake()
    {
        AssetLoad();
    }
    public void AssetLoad()
    {
        Addressables.LoadAssetAsync<StringTableScriptableObject>("StringTable").Completed +=
         (AsyncOperationHandle<StringTableScriptableObject> data) =>
         {
             Handle = data;
             foreach (StringTableScriptableObject.StringTable d in data.Result.datas)
             {
                 if (_dicData.ContainsKey(d.ID))
                 {
                     _dicData[d.ID] = d.TEXT;
                     Debug.LogError("StringTable reduplication data");
                 }
                 else
                 {
                     _dicData.Add(d.ID, d.TEXT);
                 }
             }
         };
    }
    public void AssetUnLoad()
    {
        Addressables.Release(Handle);
    }
    public string GetString(int ID)
    {
        return _dicData.ContainsKey(ID) ? _dicData[ID] : null;
    }
}
