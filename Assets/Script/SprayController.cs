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

    Vector3 directionToSpray;

    float timer = 1.5f;

    [SerializeField] float reviveAmount = 0.1f;

    private void Start()
    {
        player = PlayerManager.instance.playerMovement;

        if (player.transform.localScale.x == 1)
        {
            directionToSpray = transform.right;
        }
        else
        {
            directionToSpray = -transform.right;
        }

        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        float yDir = Random.Range(-0.5f, 0.5f);
        float zDir = Random.Range(-1f, 1f);


        while (true)
        {
            while (timer > 0)
            {
                gameObject.transform.Translate(new Vector3(directionToSpray.x, yDir, zDir) * Time.deltaTime * speed);
                timer -= Time.deltaTime;
                yield return null;
            }

            timer = 1.5f;
            // gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            // gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + Random.Range(-0.25f, 3f));
            yield return new WaitForSeconds(0.25f);

            while (timer > 0)
            {
                gameObject.transform.Translate(new Vector3(directionToSpray.x, yDir, 0) * Time.deltaTime * speed);
                timer -= Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.gameObject.tag == "Enemy")
        // {
        //     Destroy(other.gameObject);
        // }

        if (other.gameObject.tag == "Dead_Tree")
        {
            other.gameObject.GetComponent<TreeController>().CheckForRevive(reviveAmount);
        }
    }
}
