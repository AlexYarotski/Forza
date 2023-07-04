using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private TransitionWindow _transitionWindow = null;

    public static SceneLoader Instance
    {
        get; 
        private set;
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance == this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void Load(string scene)
    {
        _transitionWindow.Show(() => UploadSceneAsync(scene));
    }

    private async void UploadSceneAsync(string sceneName)
    {
        var loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);

        while (!loadSceneAsync.isDone)
        {
            _transitionWindow.SetProgressBar(loadSceneAsync.progress);

            await Task.Yield();
        }

        _transitionWindow.Hide();
    }
}
