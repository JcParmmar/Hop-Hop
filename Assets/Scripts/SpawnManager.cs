using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    
    public GameObject cubePrefab;// cube prefab ref
    public GameObject ballPrefab;// ball prefab ref
    public GameObject ballParent;// ball parent where ball will spawn
    public int numberOfSpawnedCubes;// number of total cube should stay in scene
    public float cubeOffsetFromBall;//Offset cube should start
    
    public float distanceBetweenCubeInForwardDirection = 1.5f;
    public float maxDistanceBetweenCubeInLeftRightDirection = 1.5f;
    public float mini = 5;// mini x direction it can go
    public float max = 5;// max x direction it can go

    private Vector3 _startLocation;//cube should move
    private float _oldValue;// last cube x value
    private readonly Vector3 _spawnLocation = new Vector3(1000,1000,1000);//spawn position where cube should spawn 
    private readonly List<GameObject> _alreadySpawnedCube = new List<GameObject>();// cube of pool

    private void Awake()
    {
        if(Instance) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        _startLocation = ballParent.transform.position;// set start location as ball parent position so first cube should just under the ball
        _startLocation.y += cubeOffsetFromBall;

        for (int i = 0; i < numberOfSpawnedCubes; i++)
        {
            //spawn all cube at one go so it don't give load to device for managing multiple cube at once 
            var newCube = Instantiate(cubePrefab, _spawnLocation, Quaternion.identity, transform);
            _alreadySpawnedCube.Add(newCube);
        }
        
        //set 1st cube just under the ball
        SpawnCubeAt(_startLocation);
        
        for (int i = 1; i < numberOfSpawnedCubes; i++)
        {
            //set all cube position 
            _oldValue = GetNewXPosition();
            _startLocation.z += distanceBetweenCubeInForwardDirection;
            SpawnCubeAt(_startLocation + new Vector3(_oldValue, 0, 0));
        }
        
        //spawn ball
        Instantiate(ballPrefab, ballParent.transform);
    }

    void SpawnCubeAt(Vector3 position)
    {
        if (_alreadySpawnedCube.Count <= 0)
        {
            //if cube is not in pool then spawn it
            Instantiate(cubePrefab, position, Quaternion.identity, transform);
            //_alreadySpawnedCube.Add(newCube);
        }
        else
        {
            //if cube is in pool then use that cube 
            var newCube = _alreadySpawnedCube[^1];
            newCube.transform.position = position;
            _alreadySpawnedCube.Remove(newCube);
        }
    }

    public void RemoveCubeFromScene(GameObject cube)
    {
        //when cube rich max point move then at last
        _alreadySpawnedCube.Add(cube);
        cube.transform.position = _spawnLocation;
        _oldValue = GetNewXPosition();
        SpawnCubeAt(_startLocation + new Vector3(_oldValue, 0, 0));
    }

    float GetNewXPosition()
    {
        for (int i = 0; i < 5; i++)
        {
            //try to find new x position for cube
            //just attempt 5 time so it don't go to infinite loop or give load to device each time it spawn cube
            var tempValue = _oldValue + Random.Range(-maxDistanceBetweenCubeInLeftRightDirection, maxDistanceBetweenCubeInLeftRightDirection);
            if(tempValue < max && tempValue > mini) return tempValue;
        }
        
        return _oldValue;
    }
}
