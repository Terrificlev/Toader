using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.Serialization;

public class CarManager : MonoBehaviour
{
    //public CarMovementrighttoleft carPrefab;
    public GameObject carPrefab;
    private Unity.Mathematics.Random _randomGenerator = new Unity.Mathematics.Random();
    
    private float randomHold = -1;
    public float startX = 11f;

    public float minDistance = 1.5f;
    public float maxDistance = 3f;

    public float minWaitTime = 0.25f;
    public float maxWaitTime = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var newX = startX;
        var newCar1 = Instantiate(carPrefab, transform, true);
        newCar1.transform.localPosition = new Vector3(newX, 0, -0.1f);
        _randomGenerator.InitState();
        while (newX > -11)
        {
            var randomDistance = _randomGenerator.NextFloat(minDistance, maxDistance);
            var newCar2 = Instantiate(carPrefab, transform, true);
            newCar2.transform.localPosition = new Vector3(newX - randomDistance, 0, -0.1f);
            newX -= randomDistance;
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
        var newCar1 = Instantiate(carPrefab, transform, true);
        newCar1.transform.localPosition = new Vector3(startX, 0, -0.1f);
        }

    }
}
