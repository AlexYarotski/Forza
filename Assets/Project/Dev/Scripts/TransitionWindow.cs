using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TransitionWindow : MonoBehaviour
{
    private readonly Vector3 StartSize = new Vector3(1, 1, 1);
    
    [SerializeField]
    private Slider _progressBar = null;

    [SerializeField]
    private float _timeAppearance = 0;

    private void Awake()
    {
        gameObject.SetActive(false);
        
        transform.localScale = Vector3.zero;
    }

    public void SetProgressBar(float progress)
    {
        _progressBar.value = progress;
    }
    
    public void Show(Action callback)
    {
        gameObject.SetActive(true);
        transform.DOScale(StartSize, _timeAppearance)
            .OnComplete(callback.Invoke);
    }

    public void Hide()
    {
        transform.DOScale(Vector3.zero, _timeAppearance);
        DOTween.PauseAll();
        
        gameObject.SetActive(false);
    }
}