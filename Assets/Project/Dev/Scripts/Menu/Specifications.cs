using System;
using Project.Dev.Scripts;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Specifications : MonoBehaviour
{
    [SerializeField]
    private Car _car = null;

    [SerializeField]
    private TextMeshProUGUI _name = null;

    [SerializeField]
    private TextMeshProUGUI _startSpeed = null;

    [SerializeField]
    private Slider _sliderStartSpeed = null;

    [SerializeField]
    private TextMeshProUGUI _maxSpeed = null;

    [SerializeField]
    private Slider _sliderMaxSpeed = null;

    [SerializeField]
    private Button _cancel = null;

    private void Awake()
    {
        _name.text = _car.name;
        _cancel.onClick.AddListener(Cancel);
        
        _startSpeed.text = Convert.ToString(_car.Speed);
        _maxSpeed.text = Convert.ToString(_car.MaxSpeed);
    }

    private void FixedUpdate()
    {
        var startSpeed = _car.Speed / 1000;
        var maxSpeed = _car.MaxSpeed / 1000;
        
        _sliderStartSpeed.value = startSpeed;
        _sliderMaxSpeed.value = maxSpeed;
    }

    private void Cancel()
    {
        SceneManager.LoadScene("Game");
    }
}
