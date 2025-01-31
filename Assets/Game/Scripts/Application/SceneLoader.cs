using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace SampleGame
{
    public class SceneLoader
    {
        public AsyncOperationHandle<SceneInstance> LoadSceneAsync(string sceneName)
        {
            return Addressables.LoadSceneAsync(sceneName);
        }
    }
}