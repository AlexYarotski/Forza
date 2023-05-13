using Project.Dev.Scripts;
using Project.Dev.Scripts.Interface;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorSetting", menuName = "Settings/ColorSetting", order = 0)]
public class ColorSetting : ScriptableObject, IColorable
{
    [SerializeField]
    private ColorConfig[] _colorConfigs = null;

    public ColorConfig[] ColorConfigs => _colorConfigs;
    
    public Material SelectMaterial(Colors color)
    {
        for (int i = 0; i < ColorConfigs.Length; i++)
        {
            if (ColorConfigs[i].Colors == color)
            {
                return ColorConfigs[i].Material;
            }
        }
        
        Debug.LogError("There is no such color!");
        
        return null;
    }
}