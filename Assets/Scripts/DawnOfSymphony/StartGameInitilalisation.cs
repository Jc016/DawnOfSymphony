using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SymphonyOfDawn;

public class StartGameInitilalisation : MonoBehaviour {

    public ParticleSystem clouds;
    public GameObject explosion;
    public Color[] CloudsColors;
    public Material[] skyboxes;

    private int seasonIndex = -1;

	// Use this for initialization
	void Start () {

        Init();
        for (int i = 0; i < 4; i++)
        {
            GameDataState.LastSeedSpawnTaken = i;
            EventManager.TriggerEvent("SeedTaken");

        }
	}

    private void Init()
    {
        GameDataState.Init();
        seasonIndex = (seasonIndex + 1 ) % 4;
        var main = clouds.main;
        Debug.Log(seasonIndex);
        main.startColor = CloudsColors[seasonIndex];
        explosion.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.StartListening("Reset", OnReset);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Reset", OnReset);
    }

    public void OnReset()
    {
        OnDestroyAllMusicalObjects();
        Init();
        
    }



    public void OnDestroyAllMusicalObjects()
    {
        GameDataState.DeleteAllMusicalObjects();
    }


    // Update is called once per frame
    void Update () {
		
	}
}
