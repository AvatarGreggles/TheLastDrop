using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    [SerializeField] public Sprite activeSprite;
    [SerializeField] public Sprite inactiveSprite;

    SpriteRenderer spriteRenderer;

    public bool isActive = false;

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

        ActivateCystal();
    }

    public void SetCrystalSprite()
    {
        if (isActive)
        {
            spriteRenderer.sprite = activeSprite;
        }
        else
        {
            spriteRenderer.sprite = inactiveSprite;
        }
    }

    public void ActivateCystal()
    {
        isActive = true;
        SetCrystalSprite();
    }
}
