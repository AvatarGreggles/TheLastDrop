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
    float currentWaterLevel;

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
        currentWaterLevel = maxWaterLevel;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            GainWater();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            LoseWater();
        }
    }

    void GainWater()
    {
        currentWaterLevel += 0.01f;
        if (currentWaterLevel > maxWaterLevel)
        {
            currentWaterLevel = maxWaterLevel;
        }

        UpdatePlayerWaterBodySprite();
    }

    void LoseWater()
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
