using System.Collections.Generic;
using UnityEngine;

public class WindowSwitcher : Singleton<WindowSwitcher>
{
    private readonly List<Window> WindowList = new List<Window>();

    private Window _currentWindow = null;
    
    private void Awake()
    {
        var windowArray = SceneContexts.Instance.SceneWindowSetting.GetWindows();

        for (var i = 0; i < windowArray.Length; i++)
        {
            windowArray[i].transform.SetParent(null);

            var newWindow = Instantiate(windowArray[i], transform);
            
            newWindow.gameObject.SetActive(false);
            
            WindowList.Add(newWindow);
        }
    }

    private void OnEnable()
    {
        Car.Died += Car_Died;
    }
    
    private void OnDisable()
    {
        Car.Died -= Car_Died;
    }

    public void Show<T>() where T : Window
    {
        var windowToShow = GetWindow<T>();

        if (_currentWindow != null && !windowToShow.IsPopUp)
        {
            _currentWindow.Hide();
        }

        if (!windowToShow.IsPopUp)
        {
            _currentWindow = windowToShow;
        }
        
        windowToShow.Show();
    }

    private void Car_Died(Vector3 position)
    {
        Show<LoseWindow>();
    }
    
    private Window GetWindow<T>() where T : Window
    {
        foreach(var window in WindowList)
        {
            if(window is T)
            {
                return window;
            }
        }

#if UNITY_EDITOR
       Debug.LogError($"Window type not found {typeof(T)}"); 
#endif
        
        return null;
    }
}