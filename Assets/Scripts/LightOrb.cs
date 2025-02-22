using UnityEngine;

public class LightOrb : MonoBehaviour
{
    
    void Update()
    {
   
            //to follow the mouse pointer
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            transform.position = mousePosition;
        
    }
}