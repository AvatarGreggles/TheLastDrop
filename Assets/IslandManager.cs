using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IslandManager : MonoBehaviour
{

    [SerializeField] TilemapCollider2D tilemapToActive;
    [SerializeField] TilemapCollider2D otherTilemapToActive;


    public List<TilemapCollider2D> islands = new List<TilemapCollider2D>();

    public bool reverseTravel = false;

    [SerializeField] bool isStartTilemap = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isStartTilemap)
        {
            GetComponent<TilemapCollider2D>().enabled = true;
        }
        else
        {
            GetComponent<TilemapCollider2D>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToIsland()
    {
        Debug.Log("Should disable tilemap");

        GetComponent<TilemapCollider2D>().enabled = false;

        if (reverseTravel && otherTilemapToActive != null)
        {
            otherTilemapToActive.enabled = true;
            PlayerManager.instance.gameObject.GetComponentInChildren<Canvas>().sortingLayerName = otherTilemapToActive.GetComponentInChildren<TilemapRenderer>().sortingLayerName;
        }
        else
        {
            tilemapToActive.enabled = true;
            PlayerManager.instance.gameObject.GetComponentInChildren<Canvas>().sortingLayerName = tilemapToActive.GetComponentInChildren<TilemapRenderer>().sortingLayerName;
        }

        reverseTravel = !reverseTravel;
    }
}
