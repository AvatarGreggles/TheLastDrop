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

    [SerializeField] Dialog startDialog;
    [SerializeField] Dialog startDialog2;

    [SerializeField] Dialog startDialog3;

    [SerializeField] Dialog startDialog4;
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

        StartCoroutine(StartGame());
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

    IEnumerator StartGame()
    {
        PlayerManager.instance.playerMovement.DoHangTime();
        DialogManager.Instance.dialogBox.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        DialogManager.Instance.lettersPerSecond = 30;
        yield return DialogManager.Instance.ShowDialog(startDialog);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return DialogManager.Instance.ShowDialog(startDialog2);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return DialogManager.Instance.ShowDialog(startDialog3);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return DialogManager.Instance.ShowDialog(startDialog4);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return DialogManager.Instance.ShowDialog(startDialog);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        DialogManager.Instance.CloseDialog();
        PlayerManager.instance.playerMovement.StopHangTime();
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
