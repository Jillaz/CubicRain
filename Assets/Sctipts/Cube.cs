using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Painter))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;
    private float _lifeTime;
    private Painter _painter;
    private Rigidbody _rigidbody;
    private bool _isHitPlatform = false;
    public event UnityAction<Cube> Release;

    private void Awake()
    {
        _painter = GetComponent<Painter>();
        _rigidbody = GetComponent<Rigidbody>();
    }    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform) == false)
        {
            return;
        }

        if (_isHitPlatform == false)
        {
            _painter.SetRandomColor();
            _isHitPlatform = true;
            _lifeTime = Random.Range(_minLifeTime, _maxLifeTime);
            StartCoroutine(ExecuteAfterTime());
        }
    }

    private IEnumerator ExecuteAfterTime()
    {
        yield return new WaitForSeconds(_lifeTime);

        Release.Invoke(this);
    }

    public void SetDefaults()
    {
        _isHitPlatform = false;
        _painter.SetDefaultColor();
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
