using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMovement : MonoBehaviour
{   //Sets int for level movement speed
    public static float LevelSpeed = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {   
        //Drops level down through view
        transform.position += new Vector3(0, -LevelSpeed * Time.deltaTime, 0);
        
       
    }
}
