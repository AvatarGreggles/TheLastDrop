using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IslandManager : MonoBehaviour
{

    public TilemapCollider2D tilemapToActive;
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

    public void ResetTilemaps()
    {
        foreach (TilemapCollider2D tilemap in islands)
        {
            tilemap.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
