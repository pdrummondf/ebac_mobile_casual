using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChange : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public float duration = 1f;
    public Color startColor = Color.white;

    private Color _correctColor;

    private void OnValidate()
    {
        meshRenderer = GetComponent<MeshRenderer>();    
    }

    private void Start()
    {
        _correctColor = meshRenderer.materials[0].GetColor("_Color");
        LerpColor();
    }

    private void LerpColor()
    {
        meshRenderer.materials[0].SetColor("_Color",startColor);
        meshRenderer.materials[0].DOColor(_correctColor, duration);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            LerpColor();
        }
    }
}
