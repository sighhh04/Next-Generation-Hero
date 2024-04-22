using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{

    public int health = 4;
    public spawnManager mSpawnController = null;
    public float speed;
    private int currentCheckpointIndex;
    //public GameObject[] checkpoints;
    //private bool isSequential = true;
    //public Text waypointsMode = null;

    // Start is called before the first frame update
    void Start()
    {
        mSpawnController = FindFirstObjectByType<spawnManager>();
        currentCheckpointIndex = Random.Range(0, 6);
    

    }

    // Update is called once per frame
    void Update()
    {
     
        
        // Get the current target checkpoint
        Transform targetCheckpoint = GameObject.FindGameObjectWithTag(mSpawnController.checkpoints[currentCheckpointIndex].name).transform;

        flyToCheckPoint(targetCheckpoint);
        // Check if the plane has reached the current checkpoint
        if (Vector2.Distance(transform.position, targetCheckpoint.position) < 0.1f)
        {
            if (mSpawnController.isSequential)
            {
                // Move to the next checkpoint or wrap around to the start
                currentCheckpointIndex = (currentCheckpointIndex + 1) % mSpawnController.checkpoints.Length;
                
            }
            else
            {
                // Pick a random checkpoint to go to next
                currentCheckpointIndex = Random.Range(0, 6);
                
            }
        }
    }

    public void flyToCheckPoint(Transform targetCheckpoint)
    {
        

        // Calculate the direction towards the current checkpoint
        Vector2 direction = targetCheckpoint.position - transform.position;
        direction.Normalize();

        // Ensure the plane is facing towards the current checkpoint
        //transform.LookAt(targetCheckpoint);

        // Move the plane forward
        transform.Translate(direction * speed * Time.deltaTime);

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Destroy(other.gameObject);
            health--;
           
            float r = gameObject.GetComponent<Renderer>().material.color.r;
            float g = gameObject.GetComponent<Renderer>().material.color.g;
            float b = gameObject.GetComponent<Renderer>().material.color.b;
            float transparent = gameObject.GetComponent<Renderer>().material.color.a;
            transparent -= .25f;

            gameObject.GetComponent<Renderer>().material.color = new Color(r, g, b, transparent);
        }
        if (health == 0)
        {
            Destroy(gameObject);
            mSpawnController.EnemyDestroyed();
            mSpawnController.mPlanesDestroyed++;
        } 
    }

}
