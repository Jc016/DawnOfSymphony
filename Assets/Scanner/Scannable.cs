using UnityEngine;
using System.Collections;

public class Scannable : MonoBehaviour
{
	public AudioClip[] playableSounds;
    private AudioSource audio;

    private void Start()
    {
       audio = GetComponent<AudioSource>();
        audio.clip = playableSounds[(int)Random.Range(0, playableSounds.Length)];
    }

    public void Ping()
	{
        if (GetComponent<GrowingAnimator>().isGrown && !audio.isPlaying)
        {
            
            audio.Play();
            Debug.Log("Playing: " + audio.clip.name);
        }

    }
}
