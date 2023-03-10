using System;
using Project.Dev.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLossing : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _score = null;

    [SerializeField]
    private Button _menu = null;
    
    [SerializeField]
    private Button _garage = null;
    
    [SerializeField]
    private Button _restart = null;

    private void OnEnable()
    {
        Urus.Died += Urus_Died;
    }

    private void OnDisable()
    {
        Urus.Died -= Urus_Died;
    }

    private void Awake()
    {
        gameObject.SetActive(false);
        
        _menu.onClick.AddListener(Menu);
        _restart.onClick.AddListener(Restart);
    }

    private void Urus_Died(float score)
    {
        gameObject.SetActive(true);
        
        _score.text = Convert.ToString((int)score);
    }

    public void Restart()
    {
        var nameScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(nameScene);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
