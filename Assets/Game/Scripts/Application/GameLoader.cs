using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace SampleGame
{
    public sealed class GameLoader
    {
        private AsyncOperationHandle<SceneInstance> loadGameHandle;

        public void UnloadGame()
        {
            Addressables.UnloadSceneAsync(loadGameHandle);
        }

        public void LoadGame()
        {
            loadGameHandle = Addressables.LoadSceneAsync(SceneNames.GAME);
        }
    }
}