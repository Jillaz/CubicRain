using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefabCube;
    [SerializeField] private float _cubeSpawnTime;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefabCube),
            actionOnGet: (cube) => OnActionGet(cube),
            actionOnRelease: (cube) => OnActionRelease(cube),
            actionOnDestroy: (gameObject) => Destroy(gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void Start()
    {
        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        var delay = new WaitForSeconds(_cubeSpawnTime);

        while (true)
        {
            _pool.Get();
            yield return delay;
        }
    }

    private void OnActionGet(Cube cube)
    {
        cube.transform.position = GetSpawnPosition();
        cube.gameObject.SetActive(true);
        cube.Release += ReleaseCube;
    }

    private void ReleaseCube(Cube cube)
    {
        _pool.Release(cube);
    }

    private void OnActionRelease(Cube cube)
    {
        cube.Release -= ReleaseCube;
        cube.SetDefaults();
        cube.gameObject.SetActive(false);
    }

    private Vector3 GetSpawnPosition()
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

        scaleFactorX = transform.localScale.x;
        scaleFactorZ = transform.localScale.z;

        minPositionX = transform.position.x - defaultLenghtFromCenter * scaleFactorX;
        maxPositionX = transform.position.x + defaultLenghtFromCenter * scaleFactorX;

        minPositionZ = transform.position.z - defaultLenghtFromCenter * scaleFactorZ;
        maxPositionZ = transform.position.z + defaultLenghtFromCenter * scaleFactorZ;

        postitionX = Random.Range(minPositionX, maxPositionX);
        postitionZ = Random.Range(minPositionZ, maxPositionZ);
        postitionY = transform.position.y;

        return new Vector3(postitionX, postitionY, postitionZ);
    }
}
