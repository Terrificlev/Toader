using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.Serialization;

public class CarManager : MonoBehaviour
{
    public GameObject carPrefab;
    private Unity.Mathematics.Random _randomGenerator = new Unity.Mathematics.Random();
    
    private float randomHold = -1;
    public float x = 0;
    public float y = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _randomGenerator.InitState();
        Instantiate(carPrefab,new Vector3(x,y,-0.1f), new Quaternion(0,0,0,0), transform);
        
    }
    // Update is called once per frame
    void Update()
    {
        while (randomHold <= 0)
        {
            var random = _randomGenerator.NextFloat(1f, 3.5f);
            randomHold = random;
        }

        randomHold -= Time.deltaTime;

        if (randomHold <= 0)
        {
            Instantiate(carPrefab,new Vector3(x,y,-0.1f), new Quaternion(0,0,0,0), transform);
            
        }

    }
}
