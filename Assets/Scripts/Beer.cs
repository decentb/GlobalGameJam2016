﻿using UnityEngine;

public class Beer : MonoBehaviour
{
    public int stressReductionAmount = 1;
    
    // Use this for initialization
    void Start()
    {
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            
            gameManager.ReduceStress(stressReductionAmount);
            
            // TODO: Play sound like "Ahhh"
            
            Destroy(transform);
        }
    }
	
    // Update is called once per frame
    void Update()
    {
    }
}