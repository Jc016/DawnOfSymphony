using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SymphonyOfDawn;

[ExecuteInEditMode]
public class ScannerEffectDemo : MonoBehaviour
{
	public Transform ScannerOrigin;
	public Material EffectMaterial;
	public float ScanDistance;

    public float speedWave = 15f;
    public float minSpeedWave = 15f;
    public float maxSpeedWave = 40f;

	private Camera _camera;

	// Demo Code
	public bool _scanning;
    List<Scannable> _scannables = new List<Scannable>();


    void Start()
	{

        StartScanner();
    }

    public void AddScannable(Scannable s)
    {
        _scannables.Add(s);

    }

    public void ClearScannables()
    {
        _scannables.Clear();
    }

	void Update()
	{
        if (_scanning)
		{
			ScanDistance += Time.deltaTime * speedWave;
            foreach (Scannable s in _scannables)
            {
                if ((int)Vector3.Distance(Vector3.zero, s.transform.position) == (int)ScanDistance)
					s.Ping();
			}
		}
        
		if (Input.GetKeyDown(KeyCode.C))
		{
			_scanning = true;
			ScanDistance = 0;
		}

		//if (Input.GetMouseButtonDown(0))
		//{
		//	Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
		//	RaycastHit hit;

		//	if (Physics.Raycast(ray, out hit))
		//	{
		//		_scanning = true;
		//		ScanDistance = 0;
		//		ScannerOrigin.position = hit.point;
		//	}
		//}
	}
    void pulseScanner()
    {
        Ray ray = _camera.ScreenPointToRay(Vector3.zero);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _scanning = true;
            ScanDistance = 0;
            speedWave = Random.Range(minSpeedWave, maxSpeedWave);
            EventManager.TriggerEvent("SeedPulse");
        }
    }
	// End Demo Code

	void OnEnable()
	{
		_camera = GetComponent<Camera>();
		_camera.depthTextureMode = DepthTextureMode.Depth;
        EventManager.StartListening("MoonCrashing", StopScanner);
        EventManager.StartListening("Reset", StartScanner);

    }

    void StartScanner()
    {
        EffectMaterial.SetColor("_MidColor", GameDataState.currentScannerColor);
        EffectMaterial.SetColor("_TrailColor", GameDataState.currentScannerColor);
        EffectMaterial.SetColor("_LeadColor", GameDataState.currentScannerColor);
        EffectMaterial.SetColor("_HBarColor", GameDataState.currentScannerColor);
        EffectMaterial.SetColor("_LeadingEdgeSharpness", GameDataState.currentScannerEdgeColor);
        InvokeRepeating("pulseScanner", 6f, 6f);
    }

    void StopScanner()
    {
        CancelInvoke("pulseScanner");
    }

    private void OnDisable()
    {
        EventManager.StopListening("MoonCrashing", StopScanner);
        EventManager.StopListening("Reset", StartScanner);
    }

    [ImageEffectOpaque]
	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		EffectMaterial.SetVector("_WorldSpaceScannerPos", Vector3.zero);
		EffectMaterial.SetFloat("_ScanDistance", ScanDistance);
		RaycastCornerBlit(src, dst, EffectMaterial);
	}

	void RaycastCornerBlit(RenderTexture source, RenderTexture dest, Material mat)
	{
		// Compute Frustum Corners
		float camFar = _camera.farClipPlane;
		float camFov = _camera.fieldOfView;
		float camAspect = _camera.aspect;

		float fovWHalf = camFov * 0.5f;

		Vector3 toRight = _camera.transform.right * Mathf.Tan(fovWHalf * Mathf.Deg2Rad) * camAspect;
		Vector3 toTop = _camera.transform.up * Mathf.Tan(fovWHalf * Mathf.Deg2Rad);

		Vector3 topLeft = (_camera.transform.forward - toRight + toTop);
		float camScale = topLeft.magnitude * camFar;

		topLeft.Normalize();
		topLeft *= camScale;

		Vector3 topRight = (_camera.transform.forward + toRight + toTop);
		topRight.Normalize();
		topRight *= camScale;

		Vector3 bottomRight = (_camera.transform.forward + toRight - toTop);
		bottomRight.Normalize();
		bottomRight *= camScale;

		Vector3 bottomLeft = (_camera.transform.forward - toRight - toTop);
		bottomLeft.Normalize();
		bottomLeft *= camScale;

		// Custom Blit, encoding Frustum Corners as additional Texture Coordinates
		RenderTexture.active = dest;

		mat.SetTexture("_MainTex", source);

		GL.PushMatrix();
		GL.LoadOrtho();

		mat.SetPass(0);

		GL.Begin(GL.QUADS);

		GL.MultiTexCoord2(0, 0.0f, 0.0f);
		GL.MultiTexCoord(1, bottomLeft);
		GL.Vertex3(0.0f, 0.0f, 0.0f);

		GL.MultiTexCoord2(0, 1.0f, 0.0f);
		GL.MultiTexCoord(1, bottomRight);
		GL.Vertex3(1.0f, 0.0f, 0.0f);

		GL.MultiTexCoord2(0, 1.0f, 1.0f);
		GL.MultiTexCoord(1, topRight);
		GL.Vertex3(1.0f, 1.0f, 0.0f);

		GL.MultiTexCoord2(0, 0.0f, 1.0f);
		GL.MultiTexCoord(1, topLeft);
		GL.Vertex3(0.0f, 1.0f, 0.0f);

		GL.End();
		GL.PopMatrix();
	}
}
