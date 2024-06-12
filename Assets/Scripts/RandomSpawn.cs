using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject[] spawnPoints; //Array of spawn points in the scene 5 in total Capsules


    // Start is called before the first frame update
    void Awake()
    {
        //Find all spawn points in the scene Sp1, Sp2, Sp3, Sp4, Sp5
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints");
        Debug.Log("Found " + spawnPoints.Length + " spawn points in the scene." );

        //Get Individual spawnPoints.
        foreach (GameObject spawnPoint in spawnPoints)
        {
            int spawnPointX = Random.Range(-10, 11);
            int spawnPointY = Random.Range(10, 20);
            int spawnPointZ = Random.Range(-10, 11);
            Vector3 spawnPositions = new Vector3(spawnPointX, spawnPointY, spawnPointZ);
            Instantiate(spawnPoint, spawnPositions, Quaternion.identity);

            Debug.Log("Spawn Point: " + spawnPoint.name + "Spawn Location is:" + spawnPointX + " " + spawnPointY + " " + spawnPointZ);
        }

        //Randomize spawn points from within spawnPoints[]:
        //spawnPoints = new GameObject[5];
        //spawnPoints[0] = GameObject.Find("Sp1");
        //spawnPoints[1] = GameObject.Find("Sp2");
        //spawnPoints[2] = GameObject.Find("Sp3");
        //spawnPoints[3] = GameObject.Find("Sp4");
        //spawnPoints[4] = GameObject.Find("Sp5");
        




        //int randomIndex = Random.Range(0, spawnPoints.Length);
        //transform.position = spawnPoints[randomIndex].transform.position;
    }

    void Update()
    {
            
          //Vector3 randomSpawnPositions = new Vector3(Random.Range(-10, 11), 5, Random.Range(-10, 11));
          //Instantiate(spawnPoints[Random.Range(0, spawnPoints.Length)], randomSpawnPositions, Quaternion.identity);
          //Instantiate(spawnPoints, randomSpawnPositions, Quaternion.identity);
        
    }

}

