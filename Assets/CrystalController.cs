using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{

    public static CrystalController instance;
    [SerializeField] public Sprite activeSprite;
    [SerializeField] public Sprite inactiveSprite;

    SpriteRenderer spriteRenderer;

    [SerializeField] GameObject waterfall;

    public bool isActive = false;

    [SerializeField] GameObject spawner;
    [SerializeField] PlantController plantController;

    [SerializeField] List<GameObject> vines = new List<GameObject>();

    [SerializeField] GameObject poisonState;
    [SerializeField] GameObject freshState;

    [SerializeField] GameObject poisonDeadZone;
    [SerializeField] GameObject freshDeadZone;

    [SerializeField] TreeController[] trees;

    [SerializeField] GameObject[] objectsToActive;
    [SerializeField] GameObject[] objectsToInactive;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (isActive)
        {
            StartCoroutine(EnableWorld());

        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetCrystalSprite();
    }

    IEnumerator EnableWorld()
    {
        yield return new WaitForSeconds(0.2f);

        if (waterfall != null)
        {
            waterfall.gameObject.SetActive(false);
        }

        if (spawner != null)
        {
            spawner.gameObject.SetActive(false);
        }

        if (plantController != null)
        {
            plantController.SetPlantSprite();
        }

        if (poisonState != null)
        {
            poisonState.gameObject.SetActive(false);
        }

        if (freshState != null)
        {
            freshState.gameObject.SetActive(true);
        }

        if (poisonDeadZone != null)
        {
            poisonDeadZone.gameObject.SetActive(false);
        }

        if (freshDeadZone != null)
        {
            freshDeadZone.gameObject.SetActive(true);
        }

        foreach (GameObject objectToActive in objectsToActive)
        {
            objectToActive.SetActive(true);
        }

        foreach (GameObject objectToInactive in objectsToInactive)
        {
            objectToInactive.SetActive(false);
        }

        ProgressController.instance.CompleteProgress();


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
            waterfall.SetActive(false);
        }
        else
        {
            spriteRenderer.sprite = inactiveSprite;
            // waterfall.SetActive(false);
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

        yield return EnableWorld();


        spawner.SetActive(false);
        plantController.SetPlantSprite();
        yield return null;
    }
}
