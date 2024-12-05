using System;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public GameObject usedLilypad;
    
    private Vector3 moveWithLog;

    private bool isOnLilypad;
    private bool isAlive = true;
    
    public float tileSize = 50f;
    public float jumpTime = 2f;

    private float remainingTime = 0;
    private Vector3 direction;

    private int isOnLog = 0;
    private bool isOnWater = false;
    
    public float waitTime = 10f;
    public float remainingWaitTime = 10f;

    public float speed = 1.3f;

    public float amoountOfLivesLeft = 5;
    void Update()
    {
        if (transform.position.x > 10 || transform.position.x < -10 || transform.position.y > 5 ||
            transform.position.y < -5)
        {
            transform.position = new Vector3(0, -4.75f, -0.1f);
            isOnWater = false;
            isAlive = false;
            amoountOfLivesLeft--;
            isOnLog = 0;
        }
        
        
        if (remainingTime <= 0)
        {
            transform.position += moveWithLog * Time.deltaTime * 1.5f;
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                remainingTime = jumpTime;
                direction = Vector3.up;
                isAlive = true;
            }
            
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                remainingTime = jumpTime;
                direction = Vector3.left;
                isAlive = true;
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                remainingTime = jumpTime;
                direction = Vector3.right;
                isAlive = true;
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                remainingTime = jumpTime;
                direction = Vector3.down ;
                isAlive = true;
            }


            return;
        }
        remainingTime -= Time.deltaTime;
        if (!isAlive)
        {
            
            direction = new Vector3(0,0,0);
            remainingTime = -1;
        }
        transform.position += direction * Time.deltaTime * (tileSize / jumpTime);

    }

    private void FixedUpdate()
    {
        if (isOnWater)
        {
            remainingWaitTime -= Time.deltaTime;
        }
        if (isOnLog >= 1)
        {
            remainingWaitTime = waitTime;
        }
        if (isOnLog <= 0 && isOnWater && remainingWaitTime <= 0)
        {
            Debug.Log("frog drowned");
            transform.position = new Vector3(0, -4.75f, -0.1f);
            isOnWater = false;
            isAlive = false;
            amoountOfLivesLeft--;
            remainingWaitTime = waitTime;
            isOnLog = 0;
            moveWithLog = new Vector3(0, 0, 0);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        if (other.gameObject.CompareTag("car"))
        {
            transform.position = new Vector3(0, -4.75f, -0.1f);
            isAlive = false;
            amoountOfLivesLeft--;
        }

        if (other.gameObject.CompareTag("log"))
        {
            isOnLog++;
            Debug.Log("on the log");
            if (other.gameObject.GetComponent<LogMovement>().toRight)
            {
                moveWithLog = new Vector3(1, 0, 0);
            }
            else if(!other.gameObject.GetComponent<LogMovement>().toRight)
            {
                moveWithLog = new Vector3(-1, 0, 0);
            }
        }

        if (other.gameObject.CompareTag("water"))
        {
            isOnWater = true;
            Debug.Log("on the water");
        }

        if (other.gameObject.CompareTag("lilypad"))
        {
            moveWithLog = new Vector3(0, 0, 0);
            var position = other.transform.position;
            transform.position = position;
            transform.position = new Vector3(0, -4.75f, -0.1f);
            isAlive = false;
            amoountOfLivesLeft--;
            Instantiate(usedLilypad, position, new Quaternion(0,0,0,0)); 
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("usedLilypad"))
        {
            transform.position = other.transform.position + new Vector3(1, 0, 0);
        }

        if (other.gameObject.CompareTag("Respawn"))
        {
            isAlive = true;
        }

        if (other.gameObject.CompareTag("alignPad"))
        {
            //transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
        }
        
    }



    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("log"))
        {
            isOnLog --;
            Debug.Log("left the log");
            //moveWithLog = new Vector3(0, 0, 0);

        }

        if (other.CompareTag("water"))
        {
            isOnWater = false;
            Debug.Log("left the water");
            
        }
    }
}
