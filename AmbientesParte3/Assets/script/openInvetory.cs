using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openInvetory : MonoBehaviour {
    public GameObject canvas;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (canvas.GetComponent<Canvas>().enabled == false)
        {
            //Debug.Log("entrei");
            if (Input.GetKeyDown(KeyCode.I))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                canvas.GetComponent<Canvas>().enabled = true;
               
            }
        }

        else if (canvas.GetComponent<Canvas>().enabled == true)
        {

            if (Input.GetKeyDown(KeyCode.I))
            {
                Cursor.visible = false;
                canvas.GetComponent<Canvas>().enabled = false;

            }
        }

    }

}
