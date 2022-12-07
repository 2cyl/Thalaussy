using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : MonoBehaviour {

    [SerializeField] public InputActionReference controllerActionTrigger;
    private XRDirectInteractor interactor;
    private float prevTrigger = 0f;
    private void Start()
    {
        interactor = GetComponent<XRDirectInteractor>();
    }

    void Update()
    {

    }

void OnTriggerStay(Collider other)
{
    if (other.CompareTag("Gun"))
    {
        if (interactor.hasSelection)
        {
            float trigger = controllerActionTrigger.action.ReadValue<float>();
            //Debug.Log("Grabbing gun.");
            if (trigger != 0 && prevTrigger == 0)
            {
                other.gameObject.GetComponent<Gun>().Fire();
            }
            prevTrigger = trigger;
        }
        
    }
}
}
