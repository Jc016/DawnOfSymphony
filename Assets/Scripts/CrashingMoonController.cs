using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SymphonyOfDawn;


public class CrashingMoonController : MonoBehaviour {

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        EventManager.TriggerEvent("MoonCrashed");
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
            CrashMoon();
    }

    public void CrashMoon()
    {
        GetComponent<Rigidbody>().useGravity = true;
        transform.Find("CrashingLight").gameObject.SetActive(true);
        transform.Find("CrashingFire").gameObject.SetActive(true);
    }
}
