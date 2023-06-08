using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Dev.Scripts
{
    public class SceneLoader : MonoBehaviour
    {
        public static void Load(string scene)
        {
            UploadSceneAsync(scene);
        }
        
        private static async void UploadSceneAsync(string sceneName)
        {
            var loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);

            while (!loadSceneAsync.isDone)
            {
                await Task.Yield();
            }
        }
    }
}