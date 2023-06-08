using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    private const string KeyScore = "Score";

    public static event Action<float> Boost = delegate { };

    [SerializeField]
    private float _speedBoostScore = 0;

    private float _score = 0;

    private void OnEnable()
    {
        Car.Drove += Car_Drove;
        Car.Died += Car_Died;
    }

    private void OnDisable()
    {
        Car.Drove -= Car_Drove;
        Car.Died -= Car_Died;
    }

    private void FixedUpdate()
    {
        if (_speedBoostScore <= _score)
        {
            SpeedUp();
        }
    }

    private void Car_Drove(Vector3 drove)
    {
        _score = drove.z;
    }

    private void SpeedUp()
    {
        _speedBoostScore *= 2;

        Boost(_score);
    }

    private void Car_Died(Vector3 position)
    {
        var bestScore = PlayerPrefs.GetInt(KeyScore);

        if (_score > bestScore)
        {
            PlayerPrefs.SetInt(KeyScore, (int)_score);
        }
    }
}
