using UnityEngine.AddressableAssets;

namespace SampleGame
{
    public sealed class MenuLoader
    {
        public void LoadMenu()
        {
            Addressables.LoadSceneAsync(SceneNames.MENU);
        }
    }
}