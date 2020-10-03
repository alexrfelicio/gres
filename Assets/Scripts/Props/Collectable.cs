using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    [SerializeField] Sprite sprite;
    [SerializeField] string artifactTitle;
    [SerializeField] string artifact;
    [SerializeField] AudioClip SFX;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            var SFXVolume = GamePersist.Instance.sfx;
            AudioSource.PlayClipAtPoint(SFX, gameObject.transform.position, SFXVolume);
            FindObjectOfType<UIManager>().ShowItemDetail(sprite, artifactTitle, artifact);
            FindObjectOfType<UIManager>().MarkAsFounded(artifact);
            Destroy(gameObject);
        }
    }

}
