using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    
    public float speed = 10f;
    public float mHeroRotateSpeed = 90f / 2f; // 90-degrees in 2 seconds
    public bool mFollowMousePosition = true;
    public GameObject projectilePrefab;
    public spawnManager mSpawnController = null;
    public Text controlMode;
    
    public float cooldown = .2f;
    private float time = 0f;
    public Slider cooldownSlider;

    // Start is called before the first frame update
    void Start()
    {
        mSpawnController = FindFirstObjectByType<spawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.M))
        {
            mFollowMousePosition = !mFollowMousePosition;
        }
        
        Vector3 pos = transform.position;

        if (mFollowMousePosition)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;  // <-- this is VERY IMPORTANT!
            controlMode.text = "Hero: Drive(Mouse)";
        }
        else
        {
            controlMode.text = "Hero: Drive(Keyboard)";
            if (Input.GetKey(KeyCode.W))
            {
                pos += ((speed * Time.smoothDeltaTime) * transform.up);
            }

            if (Input.GetKey(KeyCode.S))
            {
                pos -= ((speed * Time.smoothDeltaTime) * transform.up);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(transform.forward, -mHeroRotateSpeed * Time.smoothDeltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(transform.forward, mHeroRotateSpeed * Time.smoothDeltaTime);
            }
        }
        if (Input.GetKey(KeyCode.Space) && time > cooldown)
        {   
            
            time = 0f;
            spawnProjectile();

        }

        cooldownSlider.value = Mathf.Clamp01(time / cooldown);
        transform.position = pos;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn")) {
            Destroy(collision.gameObject);
            mSpawnController.EnemyDestroyed();
            mSpawnController.mPlanesTouched++;
            mSpawnController.mEnemyTouched.text = "Planes touched(" + mSpawnController.mPlanesTouched + ")";
            mSpawnController.mPlanesDestroyed++;
            
        }

 
    }

    private void spawnProjectile()
    {
        GameObject e = Instantiate(projectilePrefab);
        e.transform.localPosition = transform.localPosition;
        e.transform.rotation = transform.rotation;
        Debug.Log("Spawn Eggs:" + e.transform.localPosition);
    }
}
