using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class Painter : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetRandomColor()
    {
        _renderer.material.SetColor("_Color", Random.ColorHSV());
    }
}
