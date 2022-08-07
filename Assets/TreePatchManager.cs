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
    [SerializeField] CrystalController crystal;
    [SerializeField] GameObject deadState;
    [SerializeField] GameObject aliveState;

    // Start is called before the first frame update
    void Start()
    {
        if (CrystalController.instance.isActive)
        {
            StartCoroutine(EnableWorld());
        }

    }

    IEnumerator EnableWorld()
    {
        yield return new WaitForSeconds(0.2f);
        HandleActiveLogic();

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

                if (crystal != null)
                {
                    crystal.DeativateCystal();
                }

                // if (deadState != null)
                // {
                //     deadState.gameObject.SetActive(true);
                // }

                // if (aliveState != null)
                // {
                //     aliveState.gameObject.SetActive(false);
                // }

                return;
            }
        }

        HandleActiveLogic();

    }

    public void HandleActiveLogic()
    {
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

        if (deadState != null)
        {
            deadState.gameObject.SetActive(false);
        }

        if (aliveState != null)
        {
            aliveState.gameObject.SetActive(true);
        }

        foreach (TreeController tree in trees)
        {
            tree.InstantLife();
        }
    }

    public void ReviveHuman()
    {
        deadState.SetActive(false);
        aliveState.SetActive(true);
    }

    public void KillHuman()
    {
        deadState.SetActive(true);
        aliveState.SetActive(false);
    }
}
