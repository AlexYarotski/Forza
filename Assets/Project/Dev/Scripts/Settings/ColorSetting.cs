using UnityEngine;

[CreateAssetMenu(fileName = "ColorSetting", menuName = "Settings/ColorSetting", order = 0)]
public class ColorSetting : ScriptableObject, IColorable
{
    [SerializeField]
    private ColorConfig[] _colorConfigs = null;

    public ColorConfig[] ColorConfigs => _colorConfigs;
    
    public Material SelectMaterial(ColorName colorName)
    {
        for (int i = 0; i < ColorConfigs.Length; i++)
        {
            if (ColorConfigs[i].ColorName == colorName)
            {
                return ColorConfigs[i].Material;
            }
        }
        
        Debug.LogError("There is no such color!");
        
        return null;
    }

    public bool CheckColor(ColorName colorName)
    {
        for (int i = 0; i < ColorConfigs.Length; i++)
        {
            if (ColorConfigs[i].ColorName == colorName)
            {
                return true;
            }
        }

        return false;
    }
}