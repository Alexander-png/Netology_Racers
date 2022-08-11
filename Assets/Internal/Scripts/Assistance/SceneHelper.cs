using UnityEngine.SceneManagement;

namespace Cars_5_5.Assistance
{
    public class SceneHelper
    {
        public static void SwitchScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
