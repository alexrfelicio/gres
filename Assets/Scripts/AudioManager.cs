using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private void Awake() {
        this.GetComponent<AudioSource>().volume = GamePersist.Instance.volume;
    }

    public void Stop() {
        GetComponent<AudioSource>().Stop();
    }

}
