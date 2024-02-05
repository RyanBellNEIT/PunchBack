using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFadeOut : MonoBehaviour
{

    private AudioSource music;

    private void Start()
    {
        music = Camera.main.gameObject.GetComponent<AudioSource>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(StartFade(music, 1f, 0f));
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
