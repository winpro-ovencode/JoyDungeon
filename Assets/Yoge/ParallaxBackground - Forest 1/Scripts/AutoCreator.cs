using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCreator : MonoBehaviour
{
    public Canvas MainCanvas;
    private float startDelay = 1;
    private float spawnInterval;
    private float spawnLimitXLeft = 20;
    private float spawnLimitXRight = 25;
    private float spawnPosY = 30;

    public Tree Tree;
    public GameObject Target;
    void Start()
    {
        Invoke("SpawnRandomBall", startDelay);
    }

    void SpawnRandomBall()
    {
        Vector3 spawnPos = Target.transform.position + new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), 0, 0);
        spawnPos.y = Tree.transform.position.y;
        spawnPos.z = Tree.transform.position.z;
        
        var go = Instantiate(Tree, spawnPos, Tree.transform.rotation, transform);
        go.ShowHP(true, MainCanvas);
        go.gameObject.SetActive(true);
        spawnInterval = Random.Range(8, 10);

        Invoke("SpawnRandomBall", spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
