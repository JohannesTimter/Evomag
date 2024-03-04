using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject meatPrefab;

    public int maxHealth = 5;
    int currentHealth;

    public float speed = 2.0f;
    public float rotationspeed = 200.0f;

    public bool isCarnivore;

    Rigidbody2D rb;

    //Patroling
    private Vector2 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 10;

    //States
    public float sightRange = 5;
    private GameObject[] desiredFoods;

    //Abort if cant reach walkpoint
    public float abortTime = 10.0f;
    float timer;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    AudioSource audioSource;
    public AudioClip hurtSound;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        timer = abortTime;
        walkPointSet = false;
        updateDesiredFoodList();

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            SearchRandomWalkPoínt();
            timer = abortTime;
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    void FixedUpdate()
    {
        if (!walkPointSet)
            lookForFood();

        if (!walkPointSet)
            SearchRandomWalkPoínt();
            
        goToWalkPoint();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    private void updateDesiredFoodList()
    {
        if (isCarnivore)
            desiredFoods = GameObject.FindGameObjectsWithTag("Meat");
        else
            desiredFoods = GameObject.FindGameObjectsWithTag("Plant");
    }

    public void ConsumeFood()
    {
        ChangeHealth(1);
        updateDesiredFoodList();
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
            PlaySound(hurtSound);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        Debug.Log("New Health: " + currentHealth);

        if(currentHealth <= 0)
        {
            Instantiate(meatPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Instantiate(meatPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }

    private void lookForFood()
    {
        foreach (GameObject food in desiredFoods)
        {
            if(food == null)
            {
                updateDesiredFoodList();
                continue;
            }
            float distance = (transform.position - food.transform.position).magnitude;

            if (food.activeSelf && distance <= sightRange)
            {
                walkPoint = new Vector2(food.transform.position.x, food.transform.position.y);
                walkPointSet = true;
                break;
            }
        }
    }

    private void SearchRandomWalkPoínt()
    {
        //Calculate Random Point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector2(randomX, randomZ);
        walkPointSet = true;
    }

    private void goToWalkPoint()
    {
        Vector2 distance = walkPoint - rb.position;

        if (distance.magnitude < 0.5f)
        {
            walkPointSet = false;
            timer = abortTime;
        }
        else
        {
            Vector2 position = rb.position;
            Vector2 direction = distance.normalized;
            transform.position = position + speed * direction * Time.deltaTime;

            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationspeed * Time.deltaTime);
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


}