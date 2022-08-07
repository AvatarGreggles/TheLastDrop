using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject punchPrefab;
    [SerializeField] GameObject sprayPrefab;
    [SerializeField] Transform punchSpawn;
    [SerializeField] Transform spraySpawn;
    [SerializeField] float attackTime = 1f;
    [SerializeField] float sprayTime = 1f;

    public bool isPunching = false;
    public bool isSpraying = false;

    private float sprayDelay = 0.05f;
    float sprayCounter = 0f;
    public int sprayAmount = 3;

    // Start is called before the first frame update
    void Start()
    {
        sprayCounter = sprayDelay;

    }

    // Update is called once per frame
    void Update()
    {
        sprayCounter -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.P) && !isPunching)
        {
            StartCoroutine(HandlePunch());
        }

        if (Input.GetKey(KeyCode.S) && PlayerManager.instance.currentWaterLevel > 0f && sprayCounter <= 0f)
        {
            sprayCounter = sprayDelay;
            StartCoroutine(HandleSpray());
        }
    }

    IEnumerator HandleSpray()
    {
        isSpraying = true;
        PlayerManager.instance.LoseWater(0.005f);
        // PlayerManager.instance.playerMovement.DoHangTime();

        GameObject nearestEnemy = FindClosestEnemy(1, 10);

        float angle = 0f;

        if (nearestEnemy != null)
        {
            Vector2 directionToTarget = nearestEnemy.transform.position - transform.position;
            angle = Vector3.Angle(Vector3.right, directionToTarget);
            if (nearestEnemy.transform.position.y < transform.position.y) angle *= -1;
        }
        else
        {
            spraySpawn.rotation = Quaternion.identity;
        }

        Quaternion sprayRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        for (int i = 0; i < sprayAmount; i++)
        {
            GameObject spray = Instantiate(sprayPrefab, spraySpawn.position, sprayRotation);

            if (nearestEnemy != null)
            {
                spray.GetComponent<SprayController>().target = nearestEnemy.transform;
            }

        }

        yield return new WaitForSeconds(sprayTime);

        // PlayerManager.instance.playerMovement.StopHangTime();
        isSpraying = false;
    }

    IEnumerator HandlePunch()
    {
        isPunching = true;
        PlayerManager.instance.playerMovement.DoHangTime();
        //Get direction vector pointing at target
        GameObject nearestEnemy = FindClosestEnemy(1, 5);

        float angle = 0f;

        if (nearestEnemy != null)
        {
            Vector2 directionToTarget = nearestEnemy.transform.position - transform.position;
            angle = Vector3.Angle(Vector3.right, directionToTarget);
            angle -= 15f;
            if (nearestEnemy.transform.position.y < transform.position.y)
            {
                angle *= -1;
            }
        }
        else
        {
            punchSpawn.rotation = Quaternion.identity;
        }

        Quaternion punchRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        GameObject punch = Instantiate(punchPrefab, punchSpawn.position, punchRotation);

        if (nearestEnemy != null)
        {
            punch.GetComponent<PunchController>().target = nearestEnemy.transform;
        }

        yield return new WaitForSeconds(attackTime);


        punch.GetComponent<PunchController>().extending = false;
        punch.GetComponent<PunchController>().retracting = true;

        yield return new WaitForSeconds(attackTime);


        PlayerManager.instance.GainWater(punch.GetComponent<PunchController>().waterGathered);
        Destroy(punch);

        PlayerManager.instance.playerMovement.StopHangTime();
        isPunching = false;
    }

    public GameObject FindClosestEnemy(float min, float max)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
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
            if (curDistance < distance && curDistance >= min && curDistance <= max)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
