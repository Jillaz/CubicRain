using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private Spawner _spawner;
    [SerializeField] float _cubeSpawnTime;

    private void Awake()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(_cube, SetSpawnPosition(), Quaternion.identity);
            yield return new WaitForSeconds(_cubeSpawnTime);
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
