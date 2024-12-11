using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject ballParent;
    public int numberOfSpawnedCubes;
    public float groundOffset;
    
    public float distanceBetweenCubeInForwardDirection = 1.5f;
    public float maxDistanceBetweenCubeInLeftRightDirection = 1.5f;
    public float mini = 5;
    public float max = 5;

    private Vector3 _startLocation;
    private float _oldValue;
    private readonly Vector3 _spawnLocation = new Vector3(1000,1000,1000);
    private List<GameObject> _alreadySpawnedCube = new List<GameObject>();
    private void Start()
    {
        _startLocation = ballParent.transform.position;
        _startLocation.y += groundOffset;

        for (int i = 0; i < numberOfSpawnedCubes; i++)
        {
            var newCube = Instantiate(cubePrefab, _spawnLocation, Quaternion.identity, transform);
            _alreadySpawnedCube.Add(newCube);
        }
        
        SpawnCubeAt(_startLocation);
        for (int i = 1; i < numberOfSpawnedCubes; i++)
        {
            _oldValue = GetNewXPosition();
            SpawnCubeAt(_startLocation + new Vector3(_oldValue, 0, 0));
            _startLocation.z += distanceBetweenCubeInForwardDirection;
        }
    }

    void SpawnCubeAt(Vector3 position)
    {
        if (_alreadySpawnedCube.Count <= 0)
        {
            var newCube = Instantiate(cubePrefab, position, Quaternion.identity, transform);
            //_alreadySpawnedCube.Add(newCube);
        }
        else
        {
            var newCube = _alreadySpawnedCube[^1];
            newCube.transform.position = position;
            _alreadySpawnedCube.Remove(newCube);
        }
    }

    void RemoveCubeFromScene(GameObject cube)
    {
        _alreadySpawnedCube.Add(cube);
        cube.transform.position = _spawnLocation;
    }

    float GetNewXPosition()
    {
        for (int i = 0; i < 5; i++)
        {
            var tempValue = _oldValue + Random.Range(-maxDistanceBetweenCubeInLeftRightDirection, maxDistanceBetweenCubeInLeftRightDirection);
            print(tempValue);
            if(tempValue < max && tempValue > mini) return tempValue;
        }

        return _oldValue;
    }
}
