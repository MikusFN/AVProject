using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {
    public bool playerDeath;
    public GameObject player;
    public GameObject Inventory;

    //YOU DIED
    public bool ativado;
    public RawImage rawImg;
    public Color col;

    // Use this for initialization
    void Start()
    {
        ativado = false;
        col = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().death == true)
        {
            Wait(2);
            ativado = true;
        }

        if (ativado)
        {
            col.a += 0.01f;
        }
        rawImg.color = new Color(rawImg.color.r, rawImg.color.g, rawImg.color.b, col.a);

        if (col.a >= 1)
        {
            Wait(3);
            SceneManager.LoadScene(0);
        }

        if (player.GetComponent<PlayerController>().page1 == true)
        {
            Inventory.GetComponent<showme>().page1 = true;
        }

    }

    IEnumerator Wait(float sec)
    {
        yield return new WaitForSeconds(sec);
    }
}
