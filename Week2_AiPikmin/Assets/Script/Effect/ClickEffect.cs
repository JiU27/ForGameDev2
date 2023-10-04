using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    public Camera mainCamera; 
    public GameObject prefabToSpawn; 

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            
            if (Physics.Raycast(ray, out hit))
            {
                
                Instantiate(prefabToSpawn, hit.point, Quaternion.identity);
            }
        }
    }
}
