using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f,
        pullForce = 100f,
        rotateSpeed = 360f;

    [SerializeField] private UIControllerScript uiControl;

    private GameObject closestTower,
        hookedTower;
    private Rigidbody2D rb2D;

    private bool isPulled = false;
    
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Move the object
        rb2D.velocity = -transform.up * moveSpeed;

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
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            //Hide game object
            gameObject.SetActive(false);
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
}
