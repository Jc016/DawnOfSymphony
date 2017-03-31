using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingAnimator : MonoBehaviour {

    public float GrowingSpeed = 0.2f;
    public float pourcentileUnderGround = 0.2f;
    public float rangeAngle = -45f;

	// Use this for initialization
	void Start () {

        Vector3 pivot = transform.FindChild("Pivot").transform.position;
        transform.RotateAround(pivot, Vector3.forward, Random.Range(-rangeAngle, rangeAngle));
        transform.RotateAround(pivot, Vector3.left, Random.Range(-rangeAngle, rangeAngle));
        StartCoroutine(Growing());
        Debug.Log(transform.rotation.eulerAngles);



    }
	
	// Update is called once per frame
	void Update () {
		
	}


    IEnumerator Growing()
    {
        while (transform.position.y < -pourcentileUnderGround)
        {
            transform.position += transform.up * GrowingSpeed * Time.deltaTime ;
            yield return null;
        }

        
        yield return null;
    }


 
}
