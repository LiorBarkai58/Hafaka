using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers {
    public class SceneLoader : MonoBehaviour
    {
        public void RestartLevel() {
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            SceneManager.LoadSceneAsync(sceneIndex);
        }

        public void QuitGame() {
            Application.Quit();
        }
    }
}
