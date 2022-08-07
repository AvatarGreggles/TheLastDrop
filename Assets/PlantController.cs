using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Layers
{
    Island1, Island2, Island3
}

public class PlantController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    public int currentSprite = 0;

    public int id = 0;

    public bool reverseTravel = false;

    [SerializeField] Transform target;
    [SerializeField] Transform otherTarget;

    [SerializeField] Layers islandLayerToActivate;



    public bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        EnableIslandOne();

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
        // Compare islandLayerToActivate enum to string
        string layerName = System.Enum.GetName(typeof(Layers), islandLayerToActivate);

        if (layerName == "Island1")
        {
            EnableIslandOne();
        }
        else if (layerName == "Island2")
        {
            EnableIslandTwo();
        }
        else if (layerName == "Island3")
        {
            EnableIslandThree();
        }


        PlayerManager.instance.playerMovement.DoHangTime();

        Transform trueTarget = reverseTravel && otherTarget != null ? otherTarget : target;

        // Move towards target over time
        while (Vector3.Distance(PlayerManager.instance.playerMovement.transform.position, trueTarget.position) > 0.1f)
        {
            PlayerManager.instance.playerMovement.transform.position = Vector3.MoveTowards(PlayerManager.instance.playerMovement.transform.position, trueTarget.position, Time.deltaTime * 15f);
            yield return null;
        }


        GetComponentInParent<IslandManager>().GoToIsland();

        currentSprite = 1;
        // reverseTravel = !reverseTravel;

        PlayerManager.instance.playerMovement.StopHangTime();

    }

    public void EnableIslandOne()
    {
        Physics.IgnoreLayerCollision(8, 9, false);
        Physics.IgnoreLayerCollision(8, 10, true);
        Physics.IgnoreLayerCollision(8, 11, true);
    }

    public void EnableIslandTwo()
    {
        Physics.IgnoreLayerCollision(8, 10, false);
        Physics.IgnoreLayerCollision(8, 11, true);
        Physics.IgnoreLayerCollision(8, 9, true);
    }

    public void EnableIslandThree()
    {
        Physics.IgnoreLayerCollision(8, 11, false);
        Physics.IgnoreLayerCollision(8, 10, true);
        Physics.IgnoreLayerCollision(8, 9, true);
    }
}
