using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour {
    public List<Transform> Rockspawns;
    public GameObject Rock;

	// Use this for initialization
	void Start () {
        SpawnRock();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnRock()
    {
        int random = Random.Range(0, Rockspawns.Count - 1);
        if (Rockspawns[random] != null) { 
            Transform targetSpawn = Rockspawns[random];
            GameObject temp = Instantiate(Rock, targetSpawn.position, Quaternion.identity);
            temp.GetComponent<RockFall>().rockSpawner = this;
            Rockspawns.RemoveAt(random);
        }
    }
}
