using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IslandManager : MonoBehaviour
{

    [SerializeField] GameObject wetTilemap;
    [SerializeField] TilemapCollider2D wetTilemapCollider;

    [SerializeField] TilemapCollider2D tilemapToActive;
    [SerializeField] TilemapCollider2D otherTilemapToActive;

    [SerializeField] PlantController plantController;


    public List<TilemapCollider2D> islands = new List<TilemapCollider2D>();

    public TreeController[] trees;

    public bool reverseTravel = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (TilemapCollider2D island in islands)
        {
            island.enabled = false;
        }
        islands[0].enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        // check that all trees are alive
        if (!wetTilemap.activeSelf)
        {
            foreach (TreeController tree in trees)
            {
                if (!tree.isAlive)
                {
                    return;
                }
                wetTilemap.SetActive(true);
                plantController.SetPlantSprite();
            }
        }
    }

    public void GoToIsland()
    {
        foreach (TilemapCollider2D island in islands)
        {
            island.enabled = false;
        }

        if (reverseTravel && otherTilemapToActive != null)
        {
            otherTilemapToActive.enabled = true;
        }
        else
        {
            tilemapToActive.enabled = true;
        }
        reverseTravel = !reverseTravel;
    }
}
