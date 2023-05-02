using System;
using TMPro;
using UnityEngine;

namespace Project.Dev.Scripts.Menu
{
    public class Lock : MonoBehaviour
    {
        private const string score = "score {0} points to open";
        
        [SerializeField]
        private TextMeshProUGUI _text = null;

        public void Score(int score)
        {
            _text.text = String.Format(Lock.score, score);
        }
    }
}