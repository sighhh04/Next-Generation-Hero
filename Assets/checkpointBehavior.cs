using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointBehavior : MonoBehaviour

{
    private int health = 4;
    private Renderer checkpointRenderer;
    private Collider2D checkpointCollider;
    private Vector2 originalPosition; // Original position of the checkpoint
    private SpriteRenderer spriteRenderer;
    

    // Start is called before the first frame update
    void Start()
    {
        // Get the renderer and collider components
        checkpointRenderer = GetComponent<Renderer>();
        checkpointCollider = GetComponent<Collider2D>();
        originalPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            // Toggle the visibility and interactability of the checkpoint
            if (checkpointRenderer != null)
                checkpointRenderer.enabled = !checkpointRenderer.enabled;

            if (checkpointCollider != null)
                checkpointCollider.enabled = !checkpointCollider.enabled;
        }
        
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Destroy(other.gameObject);
            health--;

            spriteRenderer.color = new Color(1f, 1f, 1f, spriteRenderer.color.a * 0.75f);
        }
        if (health == 0)
        {
            respawnCheckpoint();

        }
    }

    void respawnCheckpoint()
    {
   
        // Calculate a random position within the specified radius of the original position
        Vector2 respawnPosition = originalPosition + Random.insideUnitCircle * 15f;

        // Respawn the checkpoint at the new position
        transform.position = respawnPosition;

        // Update the original position to the new respawn position
        originalPosition = respawnPosition;

        // Reset hit points
        health = 4;

        // Restore opacity to 100%
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
}
