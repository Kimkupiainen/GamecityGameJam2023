using UnityEngine;
using System.Collections;
 
public class CheerScript : MonoBehaviour {
 
    public AudioClip soundFile;
 
    public void Cheer() {
        // Play the sound file
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = soundFile;
        audio.Play();
 
        // Jiggle the capsules for 2 seconds
        float startTime = Time.time;
        while (Time.time - startTime < 2) {
            foreach (Transform child in transform) {
                foreach (Transform capsule in child) {
                    capsule.position += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
                }
            }
        }
    }
        void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Cheer();
        }
    }
}