using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SymphonyOfDawn;


public class CrashingMoonController : MonoBehaviour {

    public GameObject explosion;
    private Vector3 originPosition;
    private bool HasCrashed = false;
    public AudioSource[] CrashSounds;



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
            Debug.Log("Moon has crashed");
            explosion.SetActive(true); 
    }

    private void OnReset()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().WakeUp();
        transform.Find("CrashingFire").gameObject.SetActive(false);
        transform.position = new Vector3(originPosition.x, 1400, originPosition.z);
        StartCoroutine("fadeOutSounds");

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
        GetComponent<Rigidbody>().WakeUp();
        transform.Find("CrashingFire").gameObject.SetActive(true);
        StartCoroutine("fadeInSounds");
      

    }
        
    public IEnumerator fadeInSounds()
    {
        while(CrashSounds[0].volume < 1)
        {
            for (int i = 0; i < CrashSounds.Length; i++)
            {
                CrashSounds[i].volume += 1 * Time.deltaTime;
            }

            yield return null;
        }
        yield return null;

    }

    public IEnumerator fadeOutSounds()
    {
        while (CrashSounds[0].volume > 0)
        {
            for (int i = 0; i < CrashSounds.Length; i++)
            {
                CrashSounds[i].volume -= 1 * Time.deltaTime;
                yield return null;
            }
        }
        yield return null;
    }
}

