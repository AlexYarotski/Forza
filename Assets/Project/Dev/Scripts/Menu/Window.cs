using System;
using UnityEngine;

public abstract class Window : MonoBehaviour
{
    public event Action ChoseSetting = delegate {  };
    
    public abstract bool IsPopUp
    {
        get;
    }
    
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
    
    protected void OpenSetting()
    {
        ChoseSetting();
    }
}