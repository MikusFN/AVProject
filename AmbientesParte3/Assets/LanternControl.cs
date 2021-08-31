using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternControl : MonoBehaviour {
    public bool beingMoved;
    public GameObject player;
    private Light luz;
    // Use this for initialization
    void Start()
    {
        beingMoved = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (beingMoved == true)
        {
            luz = this.gameObject.GetComponentInChildren<Light>();

            if (luz.intensity > 0 & Input.GetKeyDown(KeyCode.L))
            {
                luz.intensity = 0;
            }
            else if (luz.intensity == 0 & Input.GetKeyDown(KeyCode.L))
            {
                luz.intensity = 5;
            }
        }
    }
}
