using System;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Vector3 moveWithLog;

    private int Direction = 0;
    
    public float tileSize = 50f;
    public float jumpTime = 2f;

    private float remainingTime = 0;
    private Vector3 direction;

    private int isOnLog = 0;
    private bool isOnWater = false;
    
    public float waitTime = 10f;
    public float remainingWaitTime = 10f;

    public float speed = 1.3f;
    void Update()
    {
        
        if (remainingTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                remainingTime = jumpTime;
                direction = Vector3.up;
            }
            
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                remainingTime = jumpTime;
                direction = Vector3.left;
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                remainingTime = jumpTime;
                direction = Vector3.right;
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                remainingTime = jumpTime;
                direction = Vector3.down ;
            }


            return;
        }
        remainingTime -= Time.deltaTime;
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
            moveWithLog = new Vector3(1.5f * Direction, 0, 0);
            transform.position = moveWithLog;
        }
        if (isOnLog <= 0 && isOnWater && remainingWaitTime <= 0)
        {
            Debug.Log("frog drowned");
            //transform.position = new Vector3(0, 0, -0.1f);
            isOnWater = false;
            isOnLog = 0;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        if (other.gameObject.CompareTag("car"))
        {
            transform.position = new Vector3(0, -4.75f, -0.1f);
        }

        if (other.gameObject.CompareTag("log"))
        {
            isOnLog++;
            Debug.Log("on the log");
            if (other.gameObject.GetComponent<LogMovement>().toRight)
            {
                Direction = 1;
            }
            else
            {
                Direction = -1;
            }
        }

        if (other.gameObject.CompareTag("water"))
        {
            isOnWater = true;
            Debug.Log("on the water");
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("log"))
        {
            isOnLog--;
            Debug.Log("left the log");
            Direction = 0;
        }

        if (other.CompareTag("water"))
        {
            isOnWater = false;
            Debug.Log("left the water");
        }
    }
}
