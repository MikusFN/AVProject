using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showme : MonoBehaviour {
    public Button myButton;
    public GameObject text1;
    public Button myButton2;
    public GameObject text2;
    public Button myButton3;
    public GameObject text3;
    public Button myButton4;
    public GameObject text4;

    public bool page1, page2;
    // Use this for initialization
    void OnEnable()
    {

        myButton.onClick.AddListener(MyFunction);//adds a listener for when you click the button
        myButton2.onClick.AddListener(MyFunction2);//adds a listener for when you click the button
        myButton3.onClick.AddListener(MyFunction3);//adds a listener for when you click the button
        myButton4.onClick.AddListener(MyFunction4);//adds a listener for when you click the button

    }
    public void MyFunction()// your listener calls this function
    {
        //your code here 
        Debug.Log("cliked");
        if (page1 == true)
        {
            if (text1.active == false)
            {
                text1.active = true;
                text2.active = false;
                text3.active = false;
                text4.active = false;
            }
        }
    }
    public void MyFunction2()// your listener calls this function
    {
        //your code here 
        Debug.Log("cliked2");
        if (page2 == true)
        {
            if (text2.active == false)
            {
                text2.active = true;
                text1.active = false;
                text3.active = false;
                text4.active = false;
            }
        }
    }
    public void MyFunction3()// your listener calls this function
    {
        //your code here 
        Debug.Log("cliked3");
        if (text3.active == false)
        {
            text3.active = true;
            text2.active = false;
            text1.active = false;
            text4.active = false;
        }
    }
    public void MyFunction4()// your listener calls this function
    {
        //your code here 
        Debug.Log("cliked4");
        if (text4.active == false)
        {
            text4.active = true;
            text2.active = false;
            text3.active = false;
            text1.active = false;
        }
    }
}
