using UnityEngine;
using UnityEngine.Serialization;

public class CarMovementrighttoleft : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public float speed = 3F;

    public bool toRight = true;
    private int Direction => toRight ? 1 : -1;
    
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * Direction, 0, 0) * Time.deltaTime;
        
        if (transform.position.x < -11)
        { 
            Destroy(gameObject);
        }   
        
    }
}
