using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingWindow : Window
{
    public const float SizeWindow = 1;
    public const float OpenDuration = 0.2f;
    private const float CloseDuration = 0.2f;

    [SerializeField]
    private Button _cancel = null;

    private void Awake()
    {
        gameObject.SetActive(false);

        transform.localScale = Vector3.zero;
        _cancel.AddListener(Hide);
    }

    public override void Show()
    {
        base.Show();

        transform.DOScale(new Vector3(SizeWindow, SizeWindow), OpenDuration);

        Time.timeScale = 0;
    }

    public override void Hide()
    {
        transform.DOScale(Vector3.zero, CloseDuration)
            .OnComplete(() => gameObject.SetActive(false));

        Time.timeScale = 1;
    }
}
