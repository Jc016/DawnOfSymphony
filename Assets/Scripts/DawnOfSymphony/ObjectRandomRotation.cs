using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRandomRotation : MonoBehaviour {

    private void Awake()
    {
        transform.Rotate(Vector3.up, Random.Range(0, 360));
    }

}
