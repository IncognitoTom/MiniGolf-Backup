using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject handle;
    [SerializeField] private GameObject key;
    
    [Header("Audio")]
    [SerializeField] private AudioClip unlockSound;
    [SerializeField] [Range(0, 1)] private float unlockVolume = 0.8f;
    
    private bool locked;
    private AudioSource audioSource;

    void Start()
    {
        locked = true;
        door.GetComponent<Rigidbody>().isKinematic = true;
        handle.GetComponent<BoxCollider>().enabled = false;
        
        
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f; 
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key") && locked)
        {
            UnlockDoor();
        }
    }

    private void UnlockDoor()
    {
        
        if (unlockSound != null)
        {
            audioSource.PlayOneShot(unlockSound, unlockVolume);
        }

      
        door.GetComponent<Rigidbody>().isKinematic = false;
        handle.GetComponent<BoxCollider>().enabled = true;
        this.GetComponent<Rigidbody>().isKinematic = false;
        locked = false;

        GetComponent<Renderer>().material.color = Color.green;
    }
}
