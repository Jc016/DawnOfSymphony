using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRandomScaler : MonoBehaviour {

    // Use this for initialization

    public float minRange = 0f, maxRange = 1.0f;


     void Awake(){
        SetRandomScale();
    }
    void Start () {
		
	}

    void SetRandomScale(){
        float scalingFactor = Random.Range(minRange, maxRange);
        transform.localScale = new Vector3(scalingFactor, scalingFactor, scalingFactor);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
