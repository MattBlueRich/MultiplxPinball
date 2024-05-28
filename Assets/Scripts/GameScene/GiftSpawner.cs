using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftSpawner : MonoBehaviour
{
    public GameObject giftPrefab;
    public float maxSpawnTime, minSpawnTime;
    private float randomTime;
    bool isLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        randomTime = Random.Range(minSpawnTime, maxSpawnTime);
        InvokeRepeating("SpawnGift", randomTime, randomTime);
    }

    void SpawnGift()
    {
        if(GameObject.Find("Mystery Gift(Clone)"))
        {
            return;
        }

        randomTime = Random.Range(minSpawnTime, maxSpawnTime); 
      
        Instantiate(giftPrefab);
    }
  
}
