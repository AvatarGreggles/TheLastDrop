using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePatchManager : MonoBehaviour
{
    [SerializeField] GameObject wetTilemap;

    public TreeController[] trees;

    [SerializeField] PlantController plantController;

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
            }

            wetTilemap.SetActive(true);

            if (plantController != null)
            {
                plantController.SetPlantSprite();
            }
        }

    }
}
