using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILock : MonoBehaviour
{
    private const string Score = "Score {0} points to open";
    private const string KeyScore = "Score";
    private const string KeyCar = "Car";
    
    [SerializeField]
    private TextMeshProUGUI _text = null;
    
    [SerializeField]
    private Image _lock = null;
    [SerializeField]
    private Image _background = null;

    [Header("LockSetting")]
    [SerializeField]
    private Vector3 _punchPosition = new Vector3();
    [SerializeField]
    private float _duration = 0f;
    [SerializeField]
    private int _vibrato = 0;

    private Tweener _tweener = null;
    
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

        CarViewPlaceholder_CarChanged((CarModelType)PlayerPrefs.GetInt(KeyCar));
    }

    public void ActivateLock()
    {
        if (_tweener != null)
        {
            _tweener.Complete();
            _tweener.Rewind();
        }
            
        _tweener = _lock.rectTransform.DOPunchPosition(_punchPosition, _duration, _vibrato);
    }

    private void CarViewPlaceholder_CarChanged(CarModelType carModelType)
    {
        var score = PlayerPrefs.GetInt(KeyScore);
        var lockCar = SceneContexts.Instance.LockCarSetting;
        var isCarOpen = score < lockCar.GetUnlockScore(carModelType);

        _text.gameObject.SetActive(isCarOpen);
        _lock.gameObject.SetActive(isCarOpen);
        _background.gameObject.SetActive(isCarOpen);

        _text.text = String.Format(Score, lockCar.GetUnlockScore(carModelType));
    }
}
