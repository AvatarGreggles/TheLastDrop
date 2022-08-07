using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerMovement playerMovement;

    [SerializeField] GameObject wingameScreen;

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
        wingameScreen.SetActive(false);
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
        currentWaterLevel -= amount;
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

    public void WinGame()
    {
        Debug.Log("You win the game");
        StartCoroutine(WinGameCoroutine());
    }

    IEnumerator WinGameCoroutine()
    {
        //yield return new WaitForSeconds(1f);
        wingameScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
    }
}
