using System;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public float tileSize = 50f;
    public float jumpTime = 2f;

    private float remainingTime = 0;
    private Vector3 direction;
    

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
                direction = Vector3.down;
            }


            return;
        }
        remainingTime -= Time.deltaTime;
        transform.position += direction * Time.deltaTime * (tileSize / jumpTime);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("car"))
        {
            transform.position = new Vector3(0, -4.75f, -0.1f);

        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {

    }
}
