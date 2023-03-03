using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetLoader<T>
{
    private T _asset;

    [Obsolete]
    public async Task<T> LoadAsync(string path)
    {
        // ??????????????????????????????????????
        var a = Addressables.LoadAsset<T>(path);
        if (a.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError("Key doesn't exist");
        }
        // ??????????????????????????????????????

        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(path);
        await handle.Task;
        _asset = handle.Result;
        return _asset;
    }

    public void Release()
    {
        if (_asset != null)
        {
            Addressables.Release(_asset);
        }
    }
}
