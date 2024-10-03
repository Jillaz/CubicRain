using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class Painter : MonoBehaviour
{
    [SerializeField] private Color _color = Color.white;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetRandomColor()
    {
        _renderer.material.SetColor("_Color", Random.ColorHSV());
    }

    public void SetDefaultColor()
    {
        _renderer.material.SetColor("_Color", _color);
    }
}
