﻿using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f,
        pullForce = 100f,
        rotateSpeed = 360f;

    [SerializeField] private UIControllerScript uiControl;

    private AudioSource audio;
    private GameObject closestTower,
        hookedTower;
    private Rigidbody2D rb2D;
    private Vector3 startPosition;
    
    private bool isPulled = false,
        isCrashed = false;
    
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();

        startPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z) && !isPulled)
        {
            if (closestTower && !hookedTower)
            {
                hookedTower = closestTower;
            }

            if (hookedTower)
            {
                float distance = Vector2.Distance(transform.position, hookedTower.transform.position);
                
                // Gravitation toward tower
                Vector3 pullDirection = (hookedTower.transform.position - transform.position).normalized;
                float newPullForce = Mathf.Clamp(pullForce / distance, 20, 50);
                rb2D.AddForce(pullDirection * newPullForce);
                
                // Angular velocity
                rb2D.angularVelocity = -rotateSpeed / distance;
                
                isPulled = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            isPulled = false;
            hookedTower = null;
            rb2D.angularVelocity = 0;
        }

        if (isCrashed)
        {
            if (!audio.isPlaying)
            {
                // Restart scene
                RestartPosition();
            }
        }
        else
        {
            // Move the object
            rb2D.velocity = -transform.up * moveSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            if (!isCrashed)
            {
                // Play SFX
                audio.Play();
                rb2D.velocity = Vector2.zero;
                rb2D.angularVelocity = 0f;
                isCrashed = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            Debug.Log("Level clear");
            uiControl.EndGame();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Tower"))
        {
            closestTower = other.gameObject;
            
            // Change tower's color back to green as indicator
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(isPulled) return;

        if (other.CompareTag("Tower"))
        {
            closestTower = null;
            hookedTower = null;
            
            // Change tower's color back to normal
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void RestartPosition()
    {
        // Set to start position
        transform.position = startPosition;
        
        // Restart rotation
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        
        // Set isCrashed to false
        isCrashed = false;

        if (closestTower)
        {
            closestTower.GetComponent<SpriteRenderer>().color = Color.white;
            closestTower = null;
        }
    }
}
