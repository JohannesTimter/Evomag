using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 5;
    int currentHealth;

    public int maxEnergy = 100;
    public int energyDrainRate = 1;
    int currentEnergy;

    public int maxEvolutionPoints = 100;
    public int evolutionpointsPerFood = 3;
    int currentEvolutionPoints;

    public TextMeshProUGUI statsText;

    float horizontal;
    float vertical;
    Vector3 mousePosition;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    public float speed = 2.0f;
    public float rotationSpeed = 200.0f;

    public GameObject meatPrefab;

    AudioSource audioSource;
    public AudioClip hurtSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        ChangeEvolutionPoints(50);
        ChangeHealth(0);
        ChangeEnergy(0);
        updateStats();

        InvokeRepeating("ReduceEnergy", 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        transform.position = position;

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }

    public void ConsumeFood()
    {
        ChangeHealth(1);
        ChangeEnergy(20);
        ChangeEvolutionPoints(evolutionpointsPerFood);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void changeMaxHealt(int change)
    {
        maxHealth += change;
    }

    public void changeMaxEnery(int change)
    {
        maxEnergy += change;
    }

    public void changeEnergyDrainRate(int change)
    {
        energyDrainRate += change;
        updateStats();
    }

    public void changeSpeed(int change)
    {
        speed += change;
        updateStats();
    }

    public void changeRotationSpeed(int change)
    {
        rotationSpeed += change;
        updateStats();
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            PlaySound(hurtSound);
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Instantiate(meatPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Instantiate(meatPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
            PanelController.instance.OpenDeathPanel();
        }
    }

    public void ChangeEnergy(int amount)
    {
        if (currentEnergy <= 0)
        {
            ChangeHealth(-1);
        }

        currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, maxEnergy);
        UIEnergyBar.instance.SetValue(currentEnergy, maxEnergy);
    }

    public void ChangeEvolutionPoints(int amount)
    {
        currentEvolutionPoints = Mathf.Clamp(currentEvolutionPoints + amount, 0, maxEvolutionPoints);
        UIEvolutionBar.instance.SetValue(currentEvolutionPoints, maxEvolutionPoints);

        Debug.Log("CurrentEvoPoints: " + currentEvolutionPoints + " MaxEvoPoints: " + maxEvolutionPoints);
    }

    private void updateStats()
    {
        statsText.text = speed + "\n" + rotationSpeed + "\n" + energyDrainRate;
    }

    private void ReduceEnergy()
    {
        ChangeEnergy(-energyDrainRate);
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public int getCurrentEnergy()
    {
        return currentEnergy;
    }

    public int getCurrentEvoPoints()
    {
        return currentEvolutionPoints;
    }


}
