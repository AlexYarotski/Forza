using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Dev.Scripts
{
    public class LoadScene : MonoBehaviour
    {
        private void OnEnable()
        {
            MainMenuWindow.PickedScene += MainMenu_PickedScene;
            PodiumInputManager.PickedScene += MainMenu_PickedScene;
            LossingWindow.PickedScene += MainMenu_PickedScene;
        }
        
        private void OnDisable()
        {
            MainMenuWindow.PickedScene -= MainMenu_PickedScene;
            PodiumInputManager.PickedScene -= MainMenu_PickedScene;
            LossingWindow.PickedScene -= MainMenu_PickedScene;
        }

        private void MainMenu_PickedScene(string scene)
        {
            UploadSceneAsync(scene);
        }
        
        private async void UploadSceneAsync(string sceneName)
        {
            var loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);

            while (!loadSceneAsync.isDone)
            {
                await Task.Yield();
            }
        }
    }
}