using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SymphonyOfDawn;


public class CrashingMoonController : MonoBehaviour {

    public GameObject explosion;
    private Vector3 originPosition;


    private void Start()
    {
        originPosition = transform.position;
    }


    private void OnEnable()
    {
        EventManager.StartListening("StartCrash", CrashMoon);
        EventManager.StartListening("Reset", OnReset);
    }

    private void OnDisable()
    {
        EventManager.StopListening("StartCrash", CrashMoon);
        EventManager.StopListening("Reset", OnReset);
    }

    private void OnTriggerEnter(Collider other)
    {
        EventManager.TriggerEvent("MoonCrashed");
        explosion.SetActive(true);
    }

    private void OnReset()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.Find("CrashingFire").gameObject.SetActive(false);
        transform.position = new Vector3(originPosition.x, 17, originPosition.z);
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
            CrashMoon();
    }



    public void CrashMoon()
    {
        EventManager.TriggerEvent("MoonCrashing");
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        transform.Find("CrashingFire").gameObject.SetActive(true);
      

    }
}
