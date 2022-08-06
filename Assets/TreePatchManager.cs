using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePatchManager : MonoBehaviour
{
    [SerializeField] GameObject wetTilemap;

    public TreeController[] trees;

    [SerializeField] PlantController plantController;
    [SerializeField] GameObject activeVines;
    [SerializeField] GameObject inactiveVines;
    [SerializeField] GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



    }

    public void CheckIfPatchIsActive()
    {
        // check that all trees are alive

        foreach (TreeController tree in trees)
        {
            if (!tree.isAlive)
            {
                wetTilemap.SetActive(false);
                if (plantController != null)
                {
                    Debug.Log("PlantController is not null");
                    plantController.ResetPlantSprite();
                }

                if (activeVines != null)
                {
                    activeVines.SetActive(false);
                }

                if (inactiveVines != null)
                {
                    inactiveVines.SetActive(true);
                }

                return;
            }
        }


        wetTilemap.SetActive(true);

        if (plantController != null)
        {
            Debug.Log("PlantController is not null");
            plantController.SetPlantSprite();
        }

        if (activeVines != null)
        {
            activeVines.SetActive(true);
        }

        if (inactiveVines != null)
        {
            inactiveVines.SetActive(false);
        }
    }
}
