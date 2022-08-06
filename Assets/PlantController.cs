using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    public int currentSprite = 0;

    public int id = 0;

    public bool reverseTravel = false;

    [SerializeField] Transform target;
    [SerializeField] Transform otherTarget;

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

        if (Input.GetKeyDown(KeyCode.Z) && inRange && currentSprite == 1)
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

        Transform trueTarget = reverseTravel && otherTarget != null ? otherTarget : target;

        // Move towards target over time
        while (Vector3.Distance(PlayerManager.instance.playerMovement.transform.position, trueTarget.position) > 0.1f)
        {
            PlayerManager.instance.playerMovement.transform.position = Vector3.MoveTowards(PlayerManager.instance.playerMovement.transform.position, trueTarget.position, Time.deltaTime * 15f);
            yield return null;
        }

        // GetComponentInParent<IslandManager>().GoToIsland();

        currentSprite = 1;
        reverseTravel = !reverseTravel;

        PlayerManager.instance.playerMovement.StopHangTime();

    }
}
