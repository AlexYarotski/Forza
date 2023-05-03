using UnityEngine;

public class Lights : MonoBehaviour
{
    [SerializeField]
    private Light[] _headLights = null;

    private void OnEnable()
    {
        ChangingLight.NightCome += ChangingLight_NightCome;
    }
    
    private void OnDisable()
    {
        ChangingLight.NightCome -= ChangingLight_NightCome;
    }

    private void ChangingLight_NightCome(bool on)
    {
        OnHeadlights(on);
    }
    
    private void OnHeadlights(bool on)
    {
        for (int i = 0; i < _headLights.Length; i++)
        {
            _headLights[i].gameObject.SetActive(on);
        }
    }
}