using System.Collections;
using UnityEngine;

public class CheckPointSpawner : MonoBehaviour
{
    [SerializeField] private GameObject checkPointPrefab; 
    private LightZone _lightZone;
    private Vector3 _spawnPosition;
    private GameObject _currentCheckpoint; 
    private bool _isSpawning ;
    private Coroutine _autoDisableCoroutine; 

    void Start()
    {
        // Ensure the LightZone is found at the start
        _lightZone = FindObjectOfType<LightZone>();

        if (_lightZone == null)
        {
            Debug.LogError("LightZone not found in the scene!");
            enabled = false; 
            return;
        }

        SpawnCheckpoint(); 
    }

    void Update()
    {
   
        if ((_currentCheckpoint == null || !_currentCheckpoint.activeInHierarchy) && !_isSpawning)
        {
            StartCoroutine(WaitAndSpawn());
        }
    }

    private void SpawnCheckpoint()
    {
        // Prevent multiple checkpoints from being spawned at once
        if (_currentCheckpoint != null) return;

        // Get spawn position from LightZone
        _spawnPosition = _lightZone.SpawnPoint;
        Debug.Log($"Spawning checkpoint at: {_spawnPosition}");

        // Instantiate a new checkpoint using the prefab
        _currentCheckpoint = Instantiate(checkPointPrefab, _spawnPosition, Quaternion.identity);
        _currentCheckpoint.SetActive(true);

        // Start auto-disable timer (5 seconds)
        _autoDisableCoroutine = StartCoroutine(AutoDisableCheckpoint(0.5f));
    }

    public void OnCheckpointCleared()
    {
        if (_currentCheckpoint != null)
        {
            if (_autoDisableCoroutine != null)
            {
                StopCoroutine(_autoDisableCoroutine);
            }
            
            _currentCheckpoint.SetActive(false); 
            _currentCheckpoint = null; // Remove reference
        }

        // Start the spawn process after clearing the checkpoint
        if (!_isSpawning)
        {
            StartCoroutine(WaitAndSpawn());
        }
    }

    private IEnumerator AutoDisableCheckpoint(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (_currentCheckpoint != null)
        {
            _currentCheckpoint.SetActive(false);
            _currentCheckpoint = null; // Remove reference

            // Start the respawn process
            if (!_isSpawning)
            {
                StartCoroutine(WaitAndSpawn());
            }
        }
    }

    private IEnumerator WaitAndSpawn()
    {
        _isSpawning = true; // Set the flag to indicate spawning is in progress

        // Wait for 1 second before spawning a new checkpoint
        yield return new WaitForSeconds(1f);
        SpawnCheckpoint();

        _isSpawning = false; // Reset the flag after spawning is complete
    }
}
