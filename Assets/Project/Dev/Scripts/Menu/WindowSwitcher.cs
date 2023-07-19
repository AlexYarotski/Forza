using System.Collections.Generic;
using UnityEngine;

public class WindowSwitcher : MonoBehaviour
{
    private readonly List<Window> WindowList = new List<Window>();

    public static WindowSwitcher Instance
    {
        get; 
        private set;
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance == this)
        {
            Destroy(gameObject);
        }
        
        var windowArray = SceneContexts.Instance.SceneWindowSetting.GetWindows();

        for (var i = 0; i < windowArray.Length; i++)
        {
            windowArray[i].transform.SetParent(null);

            var newWindow = Instantiate(windowArray[i], transform);
            
            newWindow.gameObject.SetActive(false);
            
            WindowList.Add(newWindow);
        }
    }
    
    public Window GetWindow<T>() where T : Window
    {
        foreach(var window in WindowList)
        {
            if(window is T)
            {
                return window;
            }
        }
        
        return null;
    }
    
    public void Show<T>() where T : Window
    {
        var windowToShow = GetWindow<T>();
        
        windowToShow.Show();
    }
}