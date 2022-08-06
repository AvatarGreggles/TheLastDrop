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

    [SerializeField] PolygonCollider2D polygonCollider;

    // Start is called before the first frame update
    void Start()
    {
        TilemapCollider2D tilemap = GetComponent<TilemapCollider2D>();
        if (tilemap != null)
        {
            if (isStartTilemap)
            {
                tilemap.enabled = true;
            }
            else
            {
                tilemap.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToIsland()
    {
        Debug.Log("Should disable tilemap");



        if (polygonCollider != null)
        {
            foreach (TilemapCollider2D island in islands)
            {
                island.enabled = false;
                island.gameObject.SetActive(false);
            }

            polygonCollider.enabled = true;
            PlayerManager.instance.gameObject.GetComponentInChildren<Canvas>().sortingLayerName = polygonCollider.GetComponentInChildren<SpriteRenderer>().sortingLayerName;
            PlayerManager.instance.gameObject.GetComponentInChildren<Canvas>().sortingOrder = polygonCollider.GetComponentInChildren<SpriteRenderer>().sortingOrder;
        }
        else if (reverseTravel && otherTilemapToActive != null)
        {
            GetComponent<TilemapCollider2D>().enabled = false;
            otherTilemapToActive.enabled = true;
            PlayerManager.instance.gameObject.GetComponentInChildren<Canvas>().sortingLayerName = otherTilemapToActive.GetComponentInChildren<TilemapRenderer>().sortingLayerName;
            PlayerManager.instance.gameObject.GetComponentInChildren<Canvas>().sortingOrder = otherTilemapToActive.GetComponentInChildren<TilemapRenderer>().sortingOrder;
        }
        else
        {
            GetComponent<TilemapCollider2D>().enabled = false;
            tilemapToActive.enabled = true;
            PlayerManager.instance.gameObject.GetComponentInChildren<Canvas>().sortingLayerName = tilemapToActive.GetComponentInChildren<TilemapRenderer>().sortingLayerName;
            PlayerManager.instance.gameObject.GetComponentInChildren<Canvas>().sortingOrder = tilemapToActive.GetComponentInChildren<TilemapRenderer>().sortingOrder;
        }

        reverseTravel = !reverseTravel;
    }
}
