using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SymphonyOfDawn;

public class StartGameInitilalisation : MonoBehaviour {

    public ParticleSystem clouds;
    public GameObject explosion;
    public Color[] CloudsColors;
    public Material[] skyboxes;
    public GameObject[] FieldElements;
    public Material[] FieldMaterials;
    public ParticleSystem DustEffect;
    public GameObject[] DustSystems;
    public Color[] ScannerColors;
    public Color[] ScannerEdgeColors;
    private bool isEarthQuakeRunning = false;

    [SerializeField]
    public OculusHapticsController leftControllerHaptics;

    [SerializeField]
    public OculusHapticsController rightControllerHaptics;

    public int seasonIndex = -1;

    // Use this for initialization
    void Start() {

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
        ControllerStopBigVibration();
        seasonIndex = (seasonIndex + 1) % 3;
        GameDataState.currentScannerColor = ScannerColors[seasonIndex];
        GameDataState.currentScannerEdgeColor = ScannerEdgeColors[seasonIndex];
        UpdateClouds();
        UpdateDust();
        UpdateFieldMaterials();
        RenderSettings.skybox = skyboxes[seasonIndex];



        explosion.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.StartListening("Reset", OnReset);
        EventManager.StartListening("StartCrash", ControllerStartBigVibration);
        EventManager.StartListening("SeedPulse", PulseSeed);
    }
    

    private void UpdateClouds()
    {
        var main = clouds.main;
        main.startColor = CloudsColors[seasonIndex];
    }

    private void UpdateDust()
    {
        for (int i = 0; i < DustSystems.Length; i++)
        {
            DustSystems[i].SetActive(false);
        }

        DustSystems[seasonIndex].SetActive(true);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Reset", OnReset);
        EventManager.StopListening("StartCrash", ControllerStartBigVibration);
        EventManager.StopListening("SeedPulse", PulseSeed);
    }

    private void ControllerStartBigVibration()
    {
        StartCoroutine("VibrateEarthquake");
        isEarthQuakeRunning = true;
    }

    private void ControllerStopBigVibration()
    {
        if (isEarthQuakeRunning)
        {
            StopCoroutine("VibrateEarthquake");
            isEarthQuakeRunning = false;
            leftControllerHaptics.Clear();
            rightControllerHaptics.Clear();
        }

    }

    public void OnReset()
    {
        OnDestroyAllMusicalObjects();
        Init();

    }

    public Material[] GenerateFieldMaterialArray()
    {
        Material[] materials = new Material[8];

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = FieldMaterials[seasonIndex];
        }

        return materials;

    }

    public void UpdateFieldMaterials()
    {
        for (int i = 0; i < FieldElements.Length; i++)
        {
            FieldElements[i].GetComponent<Renderer>().materials = GenerateFieldMaterialArray();
        }
    }



    public void OnDestroyAllMusicalObjects()
    {
        GameDataState.DeleteAllMusicalObjects();
    }



    IEnumerator VibrateEarthquake()
    {

        while (true)
        {
            leftControllerHaptics.Vibrate(VibrationForce.Hard);
            rightControllerHaptics.Vibrate(VibrationForce.Hard);
            yield return new WaitForSeconds(leftControllerHaptics.earthQuake.length);
        }
        yield return null;
    }

    void PulseSeed()
    {
        if(GameDataState.takenSeed != null)
        {
            AudioSource audioSource = GameDataState.takenSeed.GetComponent<AudioSource>();
            leftControllerHaptics.Vibrate(audioSource.clip);
            rightControllerHaptics.Vibrate(audioSource.clip);
        }
    }
}
