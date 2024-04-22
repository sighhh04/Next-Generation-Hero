using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileBehavior : MonoBehaviour
{
    
    public float kEggSpeed = 40f;
    public const int kLifeTime = 3000; // Alife for this number of cycles
    public float verticalBound = 100;
    public float horizontalBound = 178f;
    public spawnManager mSpawnController = null;

    private int mLifeCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        mLifeCount = kLifeTime;
        mSpawnController = FindFirstObjectByType<spawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (kEggSpeed * Time.smoothDeltaTime);
        mLifeCount--;
        if (mLifeCount <= 0)
        {
            Destroy(transform.gameObject);  // kills self
        }


        if (transform.position.y > verticalBound || transform.position.y < -verticalBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.x > horizontalBound || transform.position.x < -horizontalBound) 
        {
            Destroy(gameObject);
        }

        
    }

}
