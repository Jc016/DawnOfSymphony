using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SymphonyOfDawn;

public class SpawnController : MonoBehaviour {

    public int ID;
    public GameObject Seed;
    public int RespawnTime;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        EventManager.StartListening("SeedTaken", StartRespawn);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SeedTaken", StartRespawn);
    }

    void TakeSeed()
    {  

    }

    void StartRespawn()
    {
        StartCoroutine(RespawnSeed());
    }

    IEnumerator RespawnSeed()
    {
        if(GameDataState.LastSeedSpawnTaken == ID)
        {
            yield return new WaitForSeconds(RespawnTime);
            Seed.GetComponent<SeedController>().SourceSpawnID = ID;
            Instantiate(Seed,transform.position,transform.rotation);
        }
     
    }
}
