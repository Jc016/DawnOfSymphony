using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SymphonyOfDawn;

public class FieldSeedContact : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision c)
    {
        Debug.Log("hitt");
        GameObject contactSeed = c.gameObject;
        SeedController seedController;
        if (ColliderIsSeed(contactSeed, out seedController))
        {
            GameDataState.AddMusicalObject(seedController.Type, c.transform.position);
        }

        Destroy(contactSeed);
    }

    bool ColliderIsSeed(GameObject gameObject,out SeedController seedController)
    {
        seedController =gameObject.GetComponent<SeedController>();
        return seedController != null;
    }
}
