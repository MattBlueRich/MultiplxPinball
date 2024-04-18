using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMovement : MonoBehaviour
{   //Sets int for level y position
    int levelY = 0;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {   
        //Drops level down through view
        transform.position += new Vector3(0, -1.0f * Time.deltaTime, 0);
        levelY = levelY - 1;
       
    }
}
