using UnityEngine;

[CreateAssetMenu(fileName = "ColorButtonSetting", menuName = "Settings/ColorButtonSetting", order = 0)]
public class ColorButtonSetting : ScriptableObject
{
    [SerializeField]
    private ColorsButton[] _colorButton = null;

    public ColorsButton[] ColorButton => _colorButton;

    public Color GetButtonColor(ColorName colorName)
    {
        for (int i = 0; i < _colorButton.Length; i++)
        {
            if (_colorButton[i].ColorName == colorName)
            {
                return _colorButton[i].Color;
            }
        }

        Debug.LogError("There is no such color!");

        return default;
    }
}
