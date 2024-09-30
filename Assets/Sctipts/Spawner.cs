using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefabCube;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameObject _spawnZone;
    [SerializeField] private float _cubeSpawnTime;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_prefabCube),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void ActionOnGet(GameObject cube)
    {
        cube.transform.position = SetSpawnPosition();
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cube.SetActive(true);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0f, _cubeSpawnTime);
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Cube cube))
        {
            _pool.Release(collision.gameObject);
        }
    }

    private Vector3 SetSpawnPosition()
    {
        float defaultLenghtFromCenter = 5f;
        float scaleFactorX;
        float scaleFactorZ;
        float minPositionX;
        float maxPositionX;
        float postitionX;
        float minPositionZ;
        float maxPositionZ;
        float postitionZ;
        float postitionY;

        scaleFactorX = _spawnZone.transform.localScale.x;
        scaleFactorZ = _spawnZone.transform.localScale.z;

        minPositionX = _spawnZone.transform.position.x - defaultLenghtFromCenter * scaleFactorX;
        maxPositionX = _spawnZone.transform.position.x + defaultLenghtFromCenter * scaleFactorX;

        minPositionZ = _spawnZone.transform.position.z - defaultLenghtFromCenter * scaleFactorZ;
        maxPositionZ = _spawnZone.transform.position.z + defaultLenghtFromCenter * scaleFactorZ;

        postitionX = Random.Range(minPositionX, maxPositionX);
        postitionZ = Random.Range(minPositionZ, maxPositionZ);
        postitionY = _spawnZone.transform.position.y;

        return new Vector3(postitionX, postitionY, postitionZ);
    }
}
