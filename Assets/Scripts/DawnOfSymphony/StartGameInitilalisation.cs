using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SymphonyOfDawn;

public class StartGameInitilalisation : MonoBehaviour {

    ParticleSystem clouds;

	// Use this for initialization
	void Start () {
        RenderSettings.skybox.SetColor("_Tint", new Color(56.0f / 255.0f, 32.0f / 255.0f, 14.0f / 255f, 128.0f / 255.0f));
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        emitParams.startColor = Color.magenta;
        clouds.Emit(emitParams, 10);
        for (int i = 0; i < 4; i++)
        {
            GameDataState.LastSeedSpawnTaken = i;
            EventManager.TriggerEvent("SeedTaken");

        }
	}

    private void OnEnable()
    {
        EventManager.StartListening("MoonCrashed", OnDestroyAllMusicalObjects);
    }

    private void OnDisable()
    {
        EventManager.StopListening("MoonCrashed", OnDestroyAllMusicalObjects);
    }

    public void OnDestroyAllMusicalObjects()
    {
        GameDataState.DeleteAllMusicalObjects();
    }

    public void Reset()
    {
       EventManager.TriggerEvent("Reset");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
