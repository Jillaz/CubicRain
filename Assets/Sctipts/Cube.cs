using System.Collections;
using Unity.VisualScripting;
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
    private bool _isColorChanged = false;
    public UnityAction<Cube> Release;

    private void Awake()
    {
        _painter = GetComponent<Painter>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetDefaults()
    {
        _isColorChanged = false;
        _painter.SetDefaultColor();
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform") == false)
        {
            return;
        }

        if (_isColorChanged == false)
        {
            _painter.SetRandomColor();
            _isColorChanged = true;
            _lifeTime = Random.Range(_minLifeTime, _maxLifeTime);
            StartCoroutine("ExecuteAfterTime");
        }
    }

    private IEnumerator ExecuteAfterTime()
    {
        yield return new WaitForSeconds(_lifeTime);

        Release.Invoke(this);
    }
}
