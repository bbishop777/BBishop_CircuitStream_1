using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class testscript : MonoBehaviour
{
	
    AudioSource audioSource;
    AudioClip myClip;
    void Start()
    {
		audioSource = GetComponent<AudioSource>();
        StartCoroutine(GetAudioClip());
		Debug.Log("Starting to download the audio...");
    }

    IEnumerator GetAudioClip()
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("https://storage.googleapis.com/rcuh-test-bf133.appspot.com/IntroSlowRoll.wav", AudioType.WAV))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log("Getting network errror of: " + www.error);
            }
            else
            {
                myClip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = myClip;
                audioSource.Play();
				Debug.Log("Audio is playing.");
            }
        }
    }
	
	
	public void pauseAudio(){
		audioSource.Pause();
	}
	
	public void playAudio(){
		audioSource.Play();
	}
	
	public void stopAudio(){
		audioSource.Stop();
	
	}
}