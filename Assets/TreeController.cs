using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] public int currentSprite = 0;

    [SerializeField] Slider revivalSlider;


    [SerializeField] private float reviveNeededPerLevel = 1f;
    float currentReviveLevel = 0f;

    public bool isAlive = false;
    // Start is called before the first frame update
    void Start()
    {

        revivalSlider.maxValue = sprites.Length - 1;
        revivalSlider.value = currentSprite;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[currentSprite];

        if (CrystalController.instance.isActive)
        {
            StartCoroutine(LoadTrees());
        }
    }

    IEnumerator LoadTrees()
    {
        yield return new WaitForSeconds(0.2f);
        InstantLife();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InstantLife()
    {
        currentSprite = sprites.Length - 1;
        revivalSlider.value = revivalSlider.maxValue;
        spriteRenderer.sprite = sprites[currentSprite];

        revivalSlider.gameObject.SetActive(false);
        isAlive = true;
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

        if (currentSprite + 1 < sprites.Length - 1)
        {
            revivalSlider.gameObject.SetActive(true);
            currentSprite++;
            spriteRenderer.sprite = sprites[currentSprite];
            revivalSlider.value = currentSprite;
        }
        else
        {
            isAlive = true;
            revivalSlider.value = currentSprite + 1;
            revivalSlider.gameObject.SetActive(false);
        }

        GetComponentInParent<TreePatchManager>().CheckIfPatchIsActive();

        // Clear bubbles

    }

    public void TakeWater()
    {
        currentSprite -= 1;

        if (currentSprite < 0)
        {
            currentSprite = 0;
        }
        revivalSlider.gameObject.SetActive(true);
        spriteRenderer.sprite = sprites[currentSprite];
        revivalSlider.value = currentSprite;

        isAlive = false;

        GetComponentInParent<TreePatchManager>().CheckIfPatchIsActive();
    }
}
