using UnityEditor;

namespace SampleGame
{
    public sealed class ApplicationExiter
    {
        public static void ExitApp()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit(0);
#endif
        }
    }
}