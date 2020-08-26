using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flashlight : MonoBehaviour
{ 
    //cache and declarations
    Light2D flashlightLightComponent;

    private void Start()
    {
        flashlightLightComponent = GetComponent<Light2D>();
    }


    private void Update()
    {
        ToggleGameObject();
    }

    private void ToggleGameObject()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlightLightComponent.enabled = !flashlightLightComponent.enabled;
        }
    }
}
