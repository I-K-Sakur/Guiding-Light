using UnityEngine;
using TMPro;

public class LightPowerMeter : MonoBehaviour
{
    private LightZone _lightZone;
    [SerializeField] private TextMeshProUGUI text; 

    void Start()
    {
        // Find LightZone in the scene
        _lightZone = FindObjectOfType<LightZone>();

        if (_lightZone == null)
        {
            Debug.LogError("LightZone not found in the scene!");
            enabled = false; 
            return;
        }

        // Check if _text is assigned in the Inspector
        if (text == null)
        {
            Debug.LogError("TextMeshProUGUI component is not assigned!");
            enabled = false;
        }
    }

    void Update()
    {
        UpdatePowerMeter(); 
    }

    private void UpdatePowerMeter()
    {
        if (_lightZone != null && text != null)
        {
          
            float area = _lightZone.CircleAreaCalculation();

          
            text.text = $"Light Power: {area:F2}"; 
        }
    }
}