using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    public Material newMaterial; // Drag your new material here through the Inspector

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the other object has a specific tag (Optional)
        //if(collision.gameObject.tag == "YourTargetTag")
        //{
        ChangeMaterial();
        //}
    }

    private void ChangeMaterial()
    {
        Renderer rend = GetComponent<Renderer>(); // Get the renderer of the object
        if (rend != null && newMaterial != null) // Check if renderer and new material exist
        {
            rend.material = newMaterial; // Assign the new material
        }
        else if (rend == null)
        {
            Debug.LogError("No Renderer component found on the GameObject.");
        }
        else if (newMaterial == null)
        {
            Debug.LogError("No new material assigned.");
        }
    }
}
