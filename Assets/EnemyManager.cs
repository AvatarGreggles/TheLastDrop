using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public float waterProvided = 0.25f;
    public float secondsProvided = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void KillEnemy()
    {
        Destroy(gameObject);
    }
}
