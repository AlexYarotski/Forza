using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    private const string Game = "Game";
    private const string Garage = "Garage";
    private const string Main = "Main";
    
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
        
        _transitionWindow.Show(() => UploadSceneAsync(Main));
        
        _windowSwitcher.Show<MainWindow>();

        AudioManager.Instance.SetClip(AudioName.Main, true);
    }

    public void LoadGarage()
    {
        DOTween.KillAll();
        
        _transitionWindow.Show(() => UploadSceneAsync(Garage));
        
        _windowSwitcher.Show<GarageWindow>();
        
        AudioManager.Instance.Stop();
    }

    public void LoadGame()
    {
        DOTween.KillAll();
        
        _transitionWindow.Show(() => UploadSceneAsync(Game));
        
        _windowSwitcher.Show<GameWindow>();
        
        AudioManager.Instance.SetClip(AudioName.Game, true);
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
