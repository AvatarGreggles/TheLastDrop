using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerMovement playerMovement;

    private void Awake()
    {
        instance = this;

        if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
