using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILock : MonoBehaviour
{
    private const string Score = "Score {0} points to open";
    private const string KeyScore = "Score";
    
    [SerializeField]
    private TextMeshProUGUI _text = null;
    
    [SerializeField]
    private Image _lock = null;
    [SerializeField]
    private Image _background = null;

    private void OnEnable()
    {
        CarViewPlaceholder.CarChanged += CarViewPlaceholder_CarChanged;
    }

    private void OnDisable()
    {
        CarViewPlaceholder.CarChanged -= CarViewPlaceholder_CarChanged;
    }

    private void Start()
    {
        _text.gameObject.SetActive(false);
        _lock.gameObject.SetActive(false);
        _background.gameObject.SetActive(false);
    }
    
    private void CarViewPlaceholder_CarChanged(CarModelType carModelType)
    {
        var lockCar = SceneContexts.Instance.LockCarSetting;
        
        if (PlayerPrefs.HasKey(KeyScore))
        {
            var score = PlayerPrefs.GetInt(KeyScore);

            if (score < lockCar.GetUnlockScore(carModelType))
            {
                _text.gameObject.SetActive(true);
                _lock.gameObject.SetActive(true);
                _background.gameObject.SetActive(true);
                
                _text.text = String.Format(Score, lockCar.GetUnlockScore(carModelType));
            }
            else
            {
                _text.gameObject.SetActive(false);
                _lock.gameObject.SetActive(false);
                _background.gameObject.SetActive(false);
            }
        }
    }
}
