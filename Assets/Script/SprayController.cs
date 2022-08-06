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
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + Random.Range(0, 1));
            float moveTimer = 1;
            while (moveTimer > 0)
            {
                gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);
                print("IsMoving");
                moveTimer -= Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
