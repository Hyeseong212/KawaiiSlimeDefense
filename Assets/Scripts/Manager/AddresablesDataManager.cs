using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddresablesDataManager : MonoSingleton<AddresablesDataManager>
{
    AsyncOperationHandle Handle;
    [SerializeField] StringTableScriptableObject.StringTable[] stringTables; 
    private void Awake()
    {
        AssetLoad();
    }
    public void AssetLoad()
    {
        Addressables.LoadAssetAsync<StringTableScriptableObject>("StringTable").Completed +=
         (AsyncOperationHandle<StringTableScriptableObject> Obj) =>
         {
             Handle = Obj;
             stringTables = Obj.Result.datas;
             Debug.Log(Obj.PercentComplete);
         };
    }
    public void AssetUnLoad()
    {
        Addressables.Release(Handle);
    }
}
