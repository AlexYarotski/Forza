using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TransitionWindow : MonoBehaviour
{
    private readonly Vector3 StartPosition = new Vector3(0, 2000, 0);
    
    [SerializeField]
    private Slider _progressBar = null;

    [SerializeField]
    private float _timeAppearance = 0;

    private void Awake()
    {
        transform.position = StartPosition;
        
        DontDestroyOnLoad(gameObject);
    }

    public void SetProgressBar(float progress)
    {
        _progressBar.value = progress;
    }
    
    public void Show(Action callback)
    {
        gameObject.SetActive(true);
        transform.DOMove(Vector3.zero, _timeAppearance);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        transform.position = StartPosition;
    }
}