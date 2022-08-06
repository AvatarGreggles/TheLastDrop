using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int currentSprite = 0;

    [SerializeField] private float reviveNeededPerLevel = 1f;
    float currentReviveLevel = 0f;

    public bool isAlive = false;
    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[currentSprite];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckForRevive(float reviveAmount)
    {
        currentReviveLevel += reviveAmount;

        if (currentReviveLevel >= reviveNeededPerLevel)
        {
            currentReviveLevel = 0f;
            StartCoroutine(Revive());
        }
    }

    public IEnumerator Revive()
    {
        yield return new WaitForSeconds(0.25f);
        // Splash
        // GetComponent<AudioSource>().Play();
        // Change sprite

        if (currentSprite + 1 < sprites.Length)
        {
            currentSprite++;
            spriteRenderer.sprite = sprites[currentSprite];
        }
        else
        {
            isAlive = true;
        }

        // Clear bubbles

    }
}
