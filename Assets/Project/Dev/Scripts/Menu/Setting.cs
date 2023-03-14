using UnityEngine;
using UnityEngine.UI;

namespace Project.Dev.Scripts.Menu
{
    public class Setting : MonoBehaviour
    {
        [SerializeField]
        private Image _background = null;

        [SerializeField]
        private MusicMenu _musicMenu = null;

        [SerializeField]
        private Button _cancel = null;

        public bool IsActive
        {
            get;
            private set;
        }

        private void Awake()
        {
            _cancel.onClick.AddListener(Cancel);
        }

        public void SetChildrenActiveState(bool active)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(active);
                IsActive = active;
            }
        }

        private void Cancel()
        {
            SetChildrenActiveState(false);
            Time.timeScale = 1;
        }
    }
}