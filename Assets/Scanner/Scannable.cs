using UnityEngine;
using System.Collections;
using SymphonyOfDawn;

public class Scannable : MonoBehaviour
{
	public AudioClip[] playableSounds;
    private AudioSource audio;
    private NumberUrn urn;


    private void Start()
    {
        urn = new NumberUrn(playableSounds.Length);
       audio = GetComponent<AudioSource>();
    }

    public void Ping()
	{
        if (!audio.isPlaying)
        {
            audio.clip = playableSounds[urn.DraftValue()];
            audio.Play();
        }

    }
}
