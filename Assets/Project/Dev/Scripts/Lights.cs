using UnityEngine;

public class Lights : MonoBehaviour
{
    [SerializeField]
    private Light _leftHeadlight = null;
    
    [SerializeField]
    private Light _rightHeadlight = null;
    
    private void OnEnable()
    {
        ChangingLight.Nightfall += ChangingLight_Nightfall;
        ChangingLight.OnsetOfDay += ChangingLight_OnsetOfDay;
    }

    private void OnDisable()
    {
        ChangingLight.Nightfall -= ChangingLight_Nightfall;
        ChangingLight.Nightfall -= ChangingLight_OnsetOfDay;
    }

    private void ChangingLight_Nightfall(float obj)
    {
        _leftHeadlight.GetComponent<Light>().enabled = true;
        _rightHeadlight.GetComponent<Light>().enabled = true;
    }
    
    private void ChangingLight_OnsetOfDay(float obj)
    {
        _leftHeadlight.GetComponent<Light>().enabled = false;
        _rightHeadlight.GetComponent<Light>().enabled = false;
    }
}
