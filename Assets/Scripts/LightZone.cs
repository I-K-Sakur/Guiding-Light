
using UnityEngine;
using Random = UnityEngine.Random;

public class LightZone : MonoBehaviour
{
    public float shrinkRate = 0.5f; // Rate of shrinking per second
 
    private Vector3 _currentScale; // Store initial scale
    private Vector3 _spawnPoint;
    private bool _isActive = true;


    public Vector3 SpawnPoint
    {
        get { return _spawnPoint; }  
        set { _spawnPoint = value; } 
    }

    public bool IsActive
    {
        get { return _isActive; }
        set { _isActive = value; }
    } 
    void Start()
    {
   
        _currentScale = transform.localScale; // Set initial scale
    
    }

    void Update()
    {
        // Shrink the light zone's visual size
        _currentScale.x -= shrinkRate * Time.deltaTime;
        _currentScale.y -= shrinkRate * Time.deltaTime;
        _currentScale.x = Mathf.Max(_currentScale.x, 0); 
        _currentScale.y = Mathf.Max(_currentScale.y, 0);
        transform.localScale = _currentScale; // Apply the new scale
        float spawnX = transform.position.x + Random.Range(-_currentScale.x / 2, _currentScale.x / 2);
        float spawnY = transform.position.y + Random.Range(-_currentScale.y / 2, _currentScale.y / 2);
        _spawnPoint = new Vector3(spawnX, spawnY, 0);
        CircleAreaCalculation();
    }

    public float CircleAreaCalculation()
    {
       
        return _currentScale.x * _currentScale.y;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            Debug.Log("Game Over");
            Application.Quit();
        }



    }


    // Function to expand the light zone (used for checkpoints)
    public void Expand(float amount)
    {
        
        _currentScale.x += amount ; 
        _currentScale.y += amount;
        transform.localScale = _currentScale; 
    }
}