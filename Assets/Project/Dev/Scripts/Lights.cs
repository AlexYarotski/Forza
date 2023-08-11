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

    private void Start()
    {
        OnHeadlights(false);
    }

    private void ChangingLight_NightCome(bool on)
    {
        OnHeadlights(on);
    }
    
    private void OnHeadlights(bool on)
    {
        for (var i = 0; i < _headLights.Length; i++)
        {
            if (on)
            {
                _headLights[i].intensity = 200;

            }
            else
            {
                _headLights[i].intensity = 0;
            }
        }
    }
}