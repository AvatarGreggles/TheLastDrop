using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{

    [SerializeField] Transform checkpoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = checkpoint.position;
            StartCoroutine(HandleDeath(other.gameObject.transform));
        }
    }

    IEnumerator HandleDeath(Transform other)
    {
        yield return new WaitForSeconds(1f);
        other.position = checkpoint.position;
        Destroy(gameObject);
    }
}
