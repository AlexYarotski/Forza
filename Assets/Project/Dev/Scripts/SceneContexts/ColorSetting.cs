using UnityEngine;

[CreateAssetMenu(fileName = "ColorSetting", menuName = "Settings/ColorSetting", order = 0)]
public class ColorSetting : ScriptableObject
{
    [SerializeField]
    private ColorConfig[] _colorConfigs = null;
    
    public ColorConfig[] ColorConfigs => _colorConfigs;
}