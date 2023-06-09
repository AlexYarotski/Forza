using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private TransitionWindow _transitionWindow = null;

    [SerializeField]
    private float _delay = 0;
    
    public void Load(string scene)
    {
        DontDestroyOnLoad(_transitionWindow);
        
        _transitionWindow.Show(() => UploadSceneAsync(scene));
    }

    private async void UploadSceneAsync(string sceneName)
    {
        var loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);

        StartCoroutine(DelayDownload(loadSceneAsync)); 
        
        _transitionWindow.Hide();
        
        await Task.Yield();
    }

    private IEnumerator DelayDownload(AsyncOperation loadSceneAsync)
    {
        var delay = new WaitForSeconds(_delay);
        loadSceneAsync.allowSceneActivation = false;
        
        while (!loadSceneAsync.isDone)
        {
            _transitionWindow.SetProgressBar(loadSceneAsync.progress);
            
            if (loadSceneAsync.progress >= 0.9f)
            {
                yield return delay;
            }
        }
    }
}
