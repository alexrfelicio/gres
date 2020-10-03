using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicTheme : MonoBehaviour {

    public static MusicTheme Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayAudio();
        } else {
            Destroy(gameObject);
        }
    }

    IEnumerator Play() {
        yield return new WaitForSeconds(0.5f);
        GetComponent<AudioSource>().volume = GamePersist.Instance.volume;
        GetComponent<AudioSource>().Play();
    }

    public void PlayAudio() {
        StartCoroutine(Play());
    }

    public void StopAudio() {
        GetComponent<AudioSource>().Stop();
    }

    public void SetVolume(float volume) {
        Instance.GetComponent<AudioSource>().volume = volume;
    }

    public bool IsPlaying() {
        return Instance.GetComponent<AudioSource>().isPlaying;
    }
}
