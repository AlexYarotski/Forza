using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button _garage = null;
    
    [SerializeField]
    private Button _play = null;
    
    [SerializeField]
    private Button _setting = null;


    private void Awake()
    {
        _play.onClick.AddListener(PlayGame);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
}
