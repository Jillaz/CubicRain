using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Painter))]
public class Cube : MonoBehaviour
{
    private Painter _painter;
    private Collider _collider;
    private bool _isColorChanged = false;

    private void Awake()
    {
        _painter = GetComponent<Painter>();
        _collider = GetComponent<Collider>();
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
            SelfDestruction();
        }
    }

    private void SelfDestruction()
    {
        float minLifeTime = 2;
        float maxLifeTime = 5;
        float lifeTime;

        lifeTime=Random.Range(minLifeTime, maxLifeTime);

        Destroy(gameObject, lifeTime);
    }
}
