using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    [SerializeField] public Sprite activeSprite;
    [SerializeField] public Sprite inactiveSprite;

    SpriteRenderer spriteRenderer;

    [SerializeField] GameObject waterfall;

    public bool isActive = false;

    [SerializeField] GameObject spawner;

    [SerializeField] List<GameObject> vines = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetCrystalSprite();
    }

    // Update is called once per frame
    void Update()
    {

        foreach (GameObject vine in vines)
        {
            if (!vine.activeSelf)
            {
                return;
            }
        }

        if (!isActive)
        {
            ActivateCystal();
        }
    }

    public void SetCrystalSprite()
    {
        if (isActive)
        {
            spriteRenderer.sprite = activeSprite;
            waterfall.SetActive(true);
        }
        else
        {
            spriteRenderer.sprite = inactiveSprite;
            waterfall.SetActive(false);
        }
    }

    public void ActivateCystal()
    {
        isActive = true;
        SetCrystalSprite();

        StartCoroutine(PurifyWorld());
    }

    public void DeativateCystal()
    {
        isActive = false;
        SetCrystalSprite();
    }

    IEnumerator PurifyWorld()
    {
        EnemyManager[] enemies = FindObjectsOfType<EnemyManager>();

        foreach (EnemyManager enemy in enemies)
        {
            enemy.KillEnemy();
        }
        spawner.SetActive(false);
        yield return null;
    }
}
