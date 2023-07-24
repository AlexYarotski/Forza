using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    private const string Game = "Game";
    private const string Garage = "Garage";
    private const string MainMenu = "Menu";
    
    [SerializeField]
    private TransitionWindow _transitionWindow = null;

    [SerializeField]
    private WindowSwitcher _windowSwitcher = null;

    public void Load(string scene)
    {
        DOTween.KillAll();
        
        _transitionWindow.Show(() => UploadSceneAsync(scene));
    }

    public void LoadMain()
    {
        DOTween.KillAll();
        
        _transitionWindow.Show(() => UploadSceneAsync(MainMenu));
        
        _windowSwitcher.Show<MainWindow>();
    }

    public void LoadGarage()
    {
        DOTween.KillAll();
        
        _transitionWindow.Show(() => UploadSceneAsync(Garage));
        
        _windowSwitcher.Show<GarageWindow>();
    }

    public void LoadGame()
    {
        DOTween.KillAll();
        
        _transitionWindow.Show(() => UploadSceneAsync(Game));
        
        _windowSwitcher.Show<GameWindow>();
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
