using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerMovement playerMovement;

    [SerializeField] public Image playerWaterBodySprite;
    float maxWaterLevel = 1f;
    public float currentWaterLevel;

    private void Awake()
    {
        instance = this;

        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentWaterLevel = 0f;
        UpdatePlayerWaterBodySprite();
    }

    void Update()
    {
        // For debuggin
        // if (Input.GetKey(KeyCode.W))
        // {
        //     GainWater(0.01f);
        // }

        // if (Input.GetKey(KeyCode.Q))
        // {
        //     LoseWater(0.01f);
        // }
    }

    public void GainWater(float amount)
    {
        currentWaterLevel += amount;
        if (currentWaterLevel > maxWaterLevel)
        {
            currentWaterLevel = maxWaterLevel;
        }

        UpdatePlayerWaterBodySprite();
    }

    public void LoseWater(float amount)
    {
        currentWaterLevel -= 0.01f;
        if (currentWaterLevel < 0)
        {
            currentWaterLevel = 0;
        }

        UpdatePlayerWaterBodySprite();
    }

    void UpdatePlayerWaterBodySprite()
    {
        playerWaterBodySprite.fillAmount = currentWaterLevel;
    }
}
