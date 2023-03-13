using System;
using Project.Dev.Scripts.Menu;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button _garageButton = null;
    
    [SerializeField]
    private Button _playButton = null;
    
    [SerializeField]
    private Button _settingButton = null;
    
    [SerializeField]
    private Image _background = null;

    [SerializeField]
    private Setting _setting = null;
    
    private void Awake()
    {
        _setting.SetChildrenActiveState(false);
        
        _playButton.onClick.AddListener(PlayGame);
        _settingButton.onClick.AddListener(Setting);
    }

    private void FixedUpdate()
    {
        if (!_setting.IsActive)
        {
            EnableButtons(true);
        }
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void Setting()
    {
        EnableButtons(false);
        _setting.SetChildrenActiveState(true);
    }

    private void EnableButtons(bool enable)
    {
        _garageButton.gameObject.SetActive(enable);
        _playButton.gameObject.SetActive(enable);
        _settingButton.gameObject.SetActive(enable);
    }
}
