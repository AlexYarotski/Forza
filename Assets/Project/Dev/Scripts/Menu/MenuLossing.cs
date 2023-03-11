using System;
using Project.Dev.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLossing : MonoBehaviour
{
    private readonly string ScoreText = "Your Score \r\n {0}";
    
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
        SetChildrenActiveState(false);
        
        _menu.onClick.AddListener(Menu);
        _restart.onClick.AddListener(Restart);
    }
    
    private void Urus_Died(float score)
    {
        SetChildrenActiveState(true);
        
        _score.color = Color.yellow;
        
        _score.text = string.Format(ScoreText, (int)score);
    }
    
    private void SetChildrenActiveState(bool active)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }
    
    private void Restart()
    {
        var nameScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(nameScene);
    }

    private void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
