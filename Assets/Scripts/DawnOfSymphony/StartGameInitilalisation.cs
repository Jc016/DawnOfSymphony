using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SymphonyOfDawn;

public class StartGameInitilalisation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 4; i++)
        {
            GameDataState.LastSeedSpawnTaken = i;
            EventManager.TriggerEvent("SeedTaken");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
