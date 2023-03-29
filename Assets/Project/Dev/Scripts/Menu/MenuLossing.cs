using System.Threading.Tasks;
using Project.Dev.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLossing : MonoBehaviour
{
    private const string MainMenu = "Menu";
    private const string GarageScene = "Garage";
    private const string Game = "Game";
    
    private readonly string ScoreText = "Your Score \r\n {0}";
    
    [SerializeField]
    private TextMeshProUGUI _score = null;

    [SerializeField]
    private Button _menu = null;
    
    [SerializeField]
    private Button _garage = null;
    
    [SerializeField]
    private Button _restart = null;

    [SerializeField]
    private Image _frame = null;
    
    private void Awake()
    {
        _menu.onClick.AddListener(Menu);
        _garage.onClick.AddListener(Garage);
        _restart.onClick.AddListener(Restart);
        
        _score.gameObject.SetActive(false);
        _menu.gameObject.SetActive(false);
        _garage.gameObject.SetActive(false);
        _restart.gameObject.SetActive(false);
        _frame.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Car.Died += Urus_Died;
    }

    private void OnDisable()
    {
        Car.Died -= Urus_Died;
    }
    
    private void Urus_Died(Vector3 position)
    {
        _score.gameObject.SetActive(true);
        _menu.gameObject.SetActive(true);
        _garage.gameObject.SetActive(true);
        _restart.gameObject.SetActive(true);
        _frame.gameObject.SetActive(true);
        
        _score.text = string.Format(ScoreText, (int)position.z);
    }
    
    private void Menu()
    {
        UploadSceneAsync(MainMenu);
    }

    private void Garage()
    {
        UploadSceneAsync(GarageScene);
    }
    
    private void Restart()
    {
        UploadSceneAsync(Game);
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
