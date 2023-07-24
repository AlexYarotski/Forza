using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LockCarSetting", menuName = "Settings/LockCarSetting", order = 0)]
public class LockCarSetting : ScriptableObject
{
    private const string KeyScore = "Score";

    [Serializable]
    public class LockCar
    {
        [field: SerializeField]
        public CarModelType CarModelType
        {
            get;
            private set;
        }

        [field: SerializeField]
        public int UnlockScore
        {
            get;
            private set;
        } 
    }

    [SerializeField]
    private LockCar[] _lockCars = null;

    public int? GetUnlockScore(CarModelType carModelType)
    {
        for (var i = 0; i < _lockCars.Length; i++)
        {
            if (_lockCars[i].CarModelType == carModelType)
            {
                return _lockCars[i].UnlockScore;
            }
        }

        return null;
    }
    
    public bool IsCarUnlocked(CarModelType carModelType)
    {
        var currentScore = PlayerPrefs.GetInt(KeyScore);
        
        for (int i = 0; i < _lockCars.Length; i++)
        {
            if (_lockCars[i].CarModelType == carModelType)
            {
                return currentScore >= GetUnlockScore(carModelType);
            }
        }

        return false;
    }
}
