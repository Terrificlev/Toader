using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LogManager : MonoBehaviour
{
    //public CarMovementrighttoleft carPrefab;
    public LogMovement logPrefab;
    
    private Unity.Mathematics.Random _randomGenerator = new Unity.Mathematics.Random();
    
    public bool toRight1 = false;
    private int Direction => toRight1 ? -1 : 1;
    
    private float randomHold = -1;
    public float startX = 11f;

    public float minDistance = 1.5f;
    public float maxDistance = 3f;

    public float minWaitTime = 0.25f;
    public float maxWaitTime = 1f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startX *= Direction;
        var newX = startX;
        var newCar1 = Instantiate(logPrefab, transform, true);
        newCar1.transform.localPosition = new Vector3(newX, 0, -0.01f);
        newCar1.toRight = toRight1;
        _randomGenerator.InitState((uint)Random.Range(0, 1000000));
        if (Direction == 1)
        {
            while (newX > -11 * Direction)
            {
                var randomDistance = _randomGenerator.NextFloat(minDistance, maxDistance);
                var newCar2 = Instantiate(logPrefab, transform, true);
                newCar2.transform.localPosition = new Vector3(newX - randomDistance * Direction, 0, -0.01f);
                newCar2.toRight = toRight1;
                newX -= randomDistance * Direction;
            }
        }

        if (Direction == -1)
        {
            while (newX < -11 * Direction)
            {
                var randomDistance = _randomGenerator.NextFloat(minDistance, maxDistance);
                var newCar2 = Instantiate(logPrefab, transform, true);
                newCar2.transform.localPosition = new Vector3(newX - randomDistance * Direction, 0, -0.01f);
                newCar2.toRight = toRight1;
                newX -= randomDistance * Direction;
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        while (randomHold <= 0)
        {
            var random = _randomGenerator.NextFloat(minWaitTime, maxWaitTime);
            randomHold = random;
        }

        randomHold -= Time.deltaTime;

        if (randomHold <= 0)
        {
            var newCar1 = Instantiate(logPrefab, transform, true);
            newCar1.transform.localPosition = new Vector3(startX, 0, -0.01f);
            newCar1.toRight = toRight1;
        }

    }
}
