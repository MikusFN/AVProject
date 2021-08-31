using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageControl : MonoBehaviour {
    public bool beingLookAt;
    public bool pickedUp;
    private Renderer rend;
    Color originalColor;

    // Use this for initialization
    void Start()
    {
        beingLookAt = false;
        pickedUp = false;
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (beingLookAt == true)
        {
            //rend.material.shader = Shader.Find("Specular");
            rend.material.color = Color.blue;
        }
        else
        {
            rend.material.color = originalColor;
        }


        if (pickedUp == true)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            Destroy(gameObject);
        }
    }
}
