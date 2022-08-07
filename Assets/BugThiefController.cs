using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugThiefController : MonoBehaviour
{

    [SerializeField] float speed = 0.001f;

    bool isDraining = false;

    GameObject nearestTree;

    [SerializeField] float yOffset = 0.5f;

    TreeController tree;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        nearestTree = FindClosestTree(0, 100);

        if (nearestTree != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(nearestTree.transform.position.x, nearestTree.transform.position.y + yOffset, nearestTree.transform.position.z), speed);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        tree = other.GetComponent<TreeController>();
        if (other.gameObject.tag == "Dead_Tree" && !isDraining)
        {
            StartCoroutine(DrainWater(tree));
        }
    }

    IEnumerator DrainWater(TreeController targetTree)
    {
        isDraining = true;
        yield return new WaitForSeconds(0.5f);
        targetTree.TakeWater();
        yield return new WaitForSeconds(0.5f);

        if (targetTree.currentSprite == 0)
        {
            // targetTree.gameObject.SetActive(false);
            nearestTree = FindClosestTree(0, 10);
        }

        isDraining = false;
        // GetComponent<AudioSource>().Play();
    }

    public GameObject FindClosestTree(float min, float max)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Dead_Tree");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        // calculate squared distances
        min = min * min;
        max = max * max;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && curDistance >= min && curDistance <= max && go.GetComponent<TreeController>().currentSprite != 0)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
