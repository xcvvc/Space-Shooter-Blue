﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
            Destroy(this.gameObject, 3.0f);  // removes prefab 3 seconds after instantiation
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
