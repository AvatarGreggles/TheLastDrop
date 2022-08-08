using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchController : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    public bool extending = true;
    public bool retracting = false;

    LineRenderer line;

    PlayerMovement player;

    [HideInInspector]
    public Transform target;

    public float waterGathered = 0f;

    private void Start()
    {
        player = PlayerManager.instance.playerMovement;

        line = GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);
    }

    void Update()
    {
        Vector3 directionToPunch = transform.right;

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

        if (extending)
        {
            transform.Translate(directionToPunch * speed * Time.deltaTime, Space.World);
        }
        else if (retracting)
        {
            transform.Translate(directionToPunch * -speed * Time.deltaTime, Space.World);
        }

        line.SetPosition(1, transform.position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            waterGathered += other.gameObject.GetComponent<EnemyManager>().waterProvided;
            Timer.instance.AddTime(other.gameObject.GetComponent<EnemyManager>().secondsProvided);
            Destroy(other.gameObject);
        }
    }
}
