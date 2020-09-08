using UnityEngine;

public class TowerController : MonoBehaviour
{
    private PlayerController player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check player
        if (other.CompareTag("Player"))
        {
            // Get player
            player = other.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check player
        if (other.CompareTag("Player"))
        {
            // Set player to null
            player = null;
        }
    }

    /// <summary>
    /// On Mouse Down
    /// </summary>
    private void OnMouseDown()
    {
        // If there is player, ...
        if(player)
        {
            player.PullPlayer(); // Pull player
        }
    }

    /// <summary>
    /// On Mouse Up
    /// </summary>
    private void OnMouseUp()
    {
        // If there is player, ...
        if(player)
        {
            player.ReleasePlayer(); // Release player
        }
    }
}
