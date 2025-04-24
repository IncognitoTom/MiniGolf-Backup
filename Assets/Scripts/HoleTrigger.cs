using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class HoleTrigger : MonoBehaviour
{
    [SerializeField] private string[] sceneFlow = { "MiniGolf hole 1", "MiniGolf hole 2", "MiniGolf hole 3" };
    [SerializeField] private float delayBeforeLoad = 1.5f;
    
    [Header("Audio Settings")]
    [SerializeField] private AudioClip successSound; 
    [Range(0.1f, 1f)] [SerializeField] private float volume = 0.7f;
    [SerializeField] private bool randomizePitch = true;
    [Range(0.9f, 1.1f)] [SerializeField] private float pitchRange = 0.05f;

    private AudioSource _audioSource;
    private bool _ballEntered;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        ConfigureAudioSource();
    }

    private void ConfigureAudioSource()
    {
        _audioSource.spatialBlend = 1f; 
        _audioSource.playOnAwake = false;
        _audioSource.minDistance = 0.5f;
        _audioSource.maxDistance = 5f;
        _audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GolfBall") && !_ballEntered)
        {
            _ballEntered = true;
            HandleSuccess(other);
            Invoke(nameof(LoadNextScene), delayBeforeLoad);
        }

         Debug.Log($"Trigger entered by: {other.name}");
    
    if (other.CompareTag("GolfBall"))
    {
        Debug.Log("Golf ball detected!");
    }
    else
    {
        Debug.LogWarning($"Object {other.name} has wrong tag or no tag");
    }
    }

    private void HandleSuccess(Collider ball)
    {
        // Audio
        PlaySuccessSound();

        // Physics
        ball.GetComponent<Rigidbody>().isKinematic = true;
        ball.transform.position = transform.position; // Snap to hole center
    }

    private void PlaySuccessSound()
    {
        if (successSound == null)
        {
            Debug.LogWarning("No success sound assigned!");
            return;
        }

        if (randomizePitch)
        {
            _audioSource.pitch = 1f + Random.Range(-pitchRange, pitchRange);
        }

        _audioSource.PlayOneShot(successSound, volume);
    }

    private void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        if (nextSceneIndex < sceneFlow.Length)
        {
            SceneManager.LoadScene(sceneFlow[nextSceneIndex]);
        }
        else
        {
            SceneManager.LoadScene(0); 
        }
    }

    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
        ConfigureAudioSource();
    }
    #endif

    
}