using Project.Dev.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Colors : MonoBehaviour
{
    private readonly string Yellow = "yellow";
    
    [SerializeField]
    private Car _prefab = null;
    [SerializeField]
    private Car _model = null;
    
    [Header("Buttons")]
    [SerializeField]
    private Button _yellow = null;
    [SerializeField]
    private Button _green = null;
    [SerializeField]
    private Button _pirple = null;
    [SerializeField]
    private Button _red = null;
    [SerializeField]
    private Button _white = null;

    [Header("Materials")]
    [SerializeField]
    private Material _yellowMat = null;
    [SerializeField]
    private Material _greenMat = null;
    [SerializeField]
    private Material _pirpleMat = null;
    [SerializeField]
    private Material _redMat = null;
    [SerializeField]
    private Material _whiteMat = null;

    private void Awake()
    {
        _yellow.onClick.AddListener(SetYellow);
        _green.onClick.AddListener(SetGreen);
        _pirple.onClick.AddListener(SetPirple);
        _red.onClick.AddListener(SetGRed);
        _white.onClick.AddListener(SetWhite);
    }
    
    private void SetYellow()
    {
        SetColor(_yellowMat);
    }
    
    private void SetGreen()
    {
        SetColor(_greenMat);
    }

    private void SetPirple()
    {
        SetColor(_pirpleMat);
    }
    
    private void SetGRed()
    {
        SetColor(_redMat);
    }
    
    private void SetWhite()
    {
        SetColor(_whiteMat);
    }
    
    private void SetColor(Material material)
    {
        Renderer[] prefabRenders = _prefab.GetComponentsInChildren<Renderer>();
        Renderer[] renders = _model.GetComponentsInChildren<Renderer>();

        for (int i = 0; i < renders.Length; i++)
        {
            if (prefabRenders[i].GetComponent<MeshFilter>().name == Yellow)
            {
                prefabRenders[i].material = material;
                renders[i].material = material;
            }
        } 
    }
}