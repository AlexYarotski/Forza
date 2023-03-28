using UnityEngine;
using UnityEngine.UI;

namespace Project.Dev.Scripts.Menu
{
    public class Setting : UIWindow
    {
        [SerializeField]
        private Button _cancel = null;

        private void Awake()
        {
            gameObject.SetActive(false);
            
            _cancel.onClick.AddListener(Cancel);
        }

        private void Cancel()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}