using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingWindow : Window
{
    private const float SizeWindow = 1;
    private const float OpenDuration = 0.2f;
    private const float CloseDuration = 0.2f;

    [SerializeField]
    private Button _garage = null;
    
    [SerializeField]
    private Button _restart = null;
    
    [SerializeField]
    private Button _cancel = null;

    private Tweener _tweener = null;

    public override bool IsPopUp
    {
        get => true;
    }

    private void Awake()
    {
        gameObject.SetActive(false);

        transform.localScale = Vector3.zero;
        
        _garage.AddListener(OpenGarage);
        _restart.AddListener(Restart);
        _cancel.AddListener(Hide);
    }

    public override void Show()
    {
        base.Show();

        CheckTweener();
        
        _tweener = transform.DOScale(new Vector3(SizeWindow, SizeWindow), OpenDuration)
            .OnComplete(() => Time.timeScale = 0);
    }

    public override void Hide()
    {
        CheckTweener();
        
        _tweener = transform.DOScale(Vector3.zero, CloseDuration);
        
        base.Hide();
            
        Time.timeScale = 1;
    }

    private void OpenGarage()
    {
        Hide();
        
        SceneLoader.Instance.LoadGarage();
    }

    private void Restart()
    {
        Hide();
        
        SceneLoader.Instance.LoadGame();
    }
    
    private void CheckTweener()
    {
        if (_tweener != null)
        {
            _tweener.Complete();
            _tweener.Rewind();
        }
    }
}
