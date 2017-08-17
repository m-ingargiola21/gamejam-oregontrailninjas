using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SelectOnInput : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] GameObject selcetedGameObject;

    private bool buttonSelected;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selcetedGameObject);
            buttonSelected = true;
        }
	   
	}

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
