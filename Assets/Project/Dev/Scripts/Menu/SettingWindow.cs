using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingWindow : Window
{
    private const float SizeWindow = 1;
    private const float OpenDuration = 0.2f;
    private const float CloseDuration = 0.2f;

    [SerializeField]
    private Button _cancel = null;

    public override bool IsPopUp
    {
        get => true;
    }

    private void Awake()
    {
        gameObject.SetActive(false);

        transform.localScale = Vector3.zero;
        _cancel.AddListener(Hide);
    }

    public override void Show()
    {
        base.Show();

        transform.DOScale(new Vector3(SizeWindow, SizeWindow), OpenDuration)
            .OnComplete(() => Time.timeScale = 0);
    }

    public override void Hide()
    {
        transform.DOScale(Vector3.zero, CloseDuration)
            .OnComplete(() => base.Hide());

        Time.timeScale = 1;
    }
}
