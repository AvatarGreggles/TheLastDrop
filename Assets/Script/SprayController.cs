using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayController : MonoBehaviour
{
    [SerializeField] float speed = 2f;


    PlayerMovement player;

    [HideInInspector]
    public Transform target;

    public float waterSpent = 0f;

    Vector3 directionToPunch;

    private void Start()
    {
        player = PlayerManager.instance.playerMovement;

        directionToPunch = transform.right;

        if (target == null)
        {
            if (player.transform.localScale.x == 1)
            {
                directionToPunch = transform.right;
            }
            else
            {
                directionToPunch = -transform.right;
            }
        }
    }

    void Update()
    {
        transform.Translate(directionToPunch * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
