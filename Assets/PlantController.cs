using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



public class PlantController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    public int currentSprite = 0;

    public int id = 0;

    public bool reverseTravel = false;

    [SerializeField] Transform target;
    [SerializeField] Transform otherTarget;

    [SerializeField] TilemapCollider2D tilemapToActive;

    [SerializeField] TilemapCollider2D tilemapToDeactive;

    [SerializeField] bool winPlant = false;




    public bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[currentSprite];

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && inRange && currentSprite > 0)
        {
            SetPlantSprite();
            StartCoroutine(HandleTeleport());

        }

    }

    public void SetPlantSprite()
    {
        Debug.Log("SetPlantSprite");
        if (currentSprite < sprites.Length - 1)
        {
            currentSprite++;
            spriteRenderer.sprite = sprites[currentSprite];
        }
    }

    public void ResetPlantSprite()
    {
        currentSprite = 0;
        spriteRenderer.sprite = sprites[currentSprite];
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
        }

    }

    IEnumerator HandleTeleport()
    {


        PlayerManager.instance.playerMovement.DoHangTime();

        // Move towards target over time
        while (Vector3.Distance(PlayerManager.instance.playerMovement.transform.position, target.position) > 0.1f)
        {
            PlayerManager.instance.playerMovement.transform.position = Vector3.MoveTowards(PlayerManager.instance.playerMovement.transform.position, target.position, Time.deltaTime * 15f);
            yield return null;
        }


        if (tilemapToActive != null)
        {
            tilemapToDeactive.enabled = false;
            tilemapToActive.enabled = true;

            PlayerManager.instance.gameObject.GetComponentInChildren<Canvas>().sortingLayerName = tilemapToActive.GetComponentInChildren<TilemapRenderer>().sortingLayerName;
            PlayerManager.instance.gameObject.GetComponentInChildren<Canvas>().sortingOrder = tilemapToActive.GetComponentInChildren<TilemapRenderer>().sortingOrder;
        }

        currentSprite = 1;
        // reverseTravel = !reverseTravel;

        if (winPlant)
        {
            PlayerManager.instance.WinGame();
        }

        PlayerManager.instance.playerMovement.StopHangTime();

    }



}
