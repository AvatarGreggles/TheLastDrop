using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IslandManager : MonoBehaviour
{
    [SerializeField] GameObject wetTilemap;

    public TreeController[] trees;
    // Start is called before the first frame update
    void Start()
    {

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
            }
        }
    }
}
