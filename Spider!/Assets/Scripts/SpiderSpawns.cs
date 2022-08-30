using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawns : MonoBehaviour
{
    public int numberSpidersMax;
    public float spawnCooldown_sec;
    public GameObject spiderPrefab;

    private int numberSpiders;
    private GameObject player;
    private bool isCooldownElapsed;

    // Start is called before the first frame update
    void Start()
    {
        numberSpiders = 0;
        player = GameObject.Find("Player");
        SpawnSpider();
    }

    // Update is called once per frame
    void Update()
    {
        if(isCooldownElapsed && numberSpiders < numberSpidersMax)
        {
            SpawnSpider();
        }
    }

    public void SpawnSpider()
    {
        Transform activeSpawnTrans = null;

        while (activeSpawnTrans == null)
        {
            int spawnIndex = Random.Range(0, transform.childCount);
            Transform transformSpawn = transform.GetChild(spawnIndex);
            if((transformSpawn.position-player.transform.position).magnitude > 5)
            {
                activeSpawnTrans = transformSpawn;
            }
        }

        Vector3 positionSpider = new Vector3(activeSpawnTrans.position.x, 0.5f, activeSpawnTrans.position.z);
        GameObject newSpider = Instantiate(spiderPrefab, positionSpider, Quaternion.FromToRotation(Vector3.right, player.transform.position - positionSpider));
        newSpider.AddComponent(new Controller)

        numberSpiders++;
        isCooldownElapsed = false;
        StartCoroutine(SpawnCooldownRoutine());
    }

    IEnumerator SpawnCooldownRoutine()
    {
        yield return new WaitForSeconds(spawnCooldown_sec);
        isCooldownElapsed = true;
    }
}
