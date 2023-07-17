using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
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
        _cancel.AddListener(CloseWindow);
    }

    private void CloseWindow()
    {
        transform.DOScale(Vector3.zero, CloseDuration)
            .OnComplete(() => gameObject.SetActive(false));

        Time.timeScale = 1;
    }
}
