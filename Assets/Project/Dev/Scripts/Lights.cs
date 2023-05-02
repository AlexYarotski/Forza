using UnityEngine;

public class Lights : MonoBehaviour
{
    [SerializeField]
    private Light[] _headLights = null;

    private void OnEnable()
    {
        ChangingLight.NightCome += ChangingLightNightCome;
    }
    
    private void OnDisable()
    {
        ChangingLight.NightCome -= ChangingLightNightCome;
    }

    private void ChangingLightNightCome(bool isNight)
    {
        OnHeadlights(isNight);
    }
    
    private void OnHeadlights(bool on)
    {
        for (int i = 0; i < _headLights.Length; i++)
        {
            _headLights[i].gameObject.SetActive(on);
        }
    }
}