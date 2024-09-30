using UnityEngine;

[RequireComponent(typeof(Painter))]
public class Cube : MonoBehaviour
{
    private Painter _painter;
    private bool _isColorChanged = false;

    private void Awake()
    {
        _painter = GetComponent<Painter>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Cube cube) == true)
        {
            return;
        }

        if (_isColorChanged == false)
        {
            _painter.SetRandomColor();
            _isColorChanged = true;
        }
    }
}
