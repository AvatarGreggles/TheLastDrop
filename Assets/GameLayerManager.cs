using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayerManager : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisableIslandOne()
    {
        Physics.IgnoreLayerCollision(8, 9, true);
    }

    public void DisableIslandTwo()
    {
        Physics.IgnoreLayerCollision(8, 10, true);
    }

    public void DisableIslandThree()
    {
        Physics.IgnoreLayerCollision(8, 11, true);
    }

    public void EnableIslandOne()
    {
        Physics.IgnoreLayerCollision(8, 9, false);
    }

    public void EnableIslandTwo()
    {
        Physics.IgnoreLayerCollision(8, 10, false);
    }

    public void EnableIslandThree()
    {
        Physics.IgnoreLayerCollision(8, 11, false);
    }
}
