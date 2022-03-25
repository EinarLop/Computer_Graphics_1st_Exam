using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    Walker_Torso walker_Torso = new Walker_Torso();
    Walker_Arms walker_Arms = new Walker_Arms();
    
   

    
    // Start is called before the first frame update
    void Start()
    {
        walker_Torso.bodySetup();
        walker_Arms.bodySetup(walker_Torso.sizes[0],walker_Torso.sizes[1],walker_Torso.sizes[1]);
    }

    // Update is called once per frame
    void Update()
    {
        walker_Torso.bodyMovement(); 
        walker_Arms.bodyMovement();
    }
}
