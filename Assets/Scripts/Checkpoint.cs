
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private float expandAmount = 2f; 
   



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the checkpoint");
            LightZone lightZone = FindObjectOfType<LightZone>();

            if (lightZone != null)
            {
                lightZone.Expand(expandAmount);
            }

            // Find the checkpoint spawner and notify it
            CheckPointSpawner spawner = FindObjectOfType<CheckPointSpawner>();
            if (spawner != null)
            {
                spawner.OnCheckpointCleared();
            }

            gameObject.SetActive(false); 
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("LightZone"))
        {
            CheckPointSpawner spawner = FindObjectOfType<CheckPointSpawner>();
            if (spawner != null)
            {
                spawner.OnCheckpointCleared();
            }

            gameObject.SetActive(false);
        }
    }
}