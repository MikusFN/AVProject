using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMoving : MonoBehaviour {
    public bool isBeingHeld;
    BoxCollider boxColl;
    public bool rightSide;

    int moveWhere;
    float move;
    public float max, min, vel;
    public GameObject player;

    public Vector3 lol;
    public AudioSource aS;


    // Use this for initialization
    void Start()
    {
        boxColl = GetComponent<BoxCollider>();

        //Posições
        max = 30;
        min = 0;
        vel = 6;
        aS = GetComponent<AudioSource>();

    }

    public void Move(float valor)
    {
        if (valor > 0)
        {
            moveWhere = 1;
        }

        if (valor < 0)
        {
            moveWhere = 2;
        }

        if (valor == 0)
        {
            moveWhere = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (move > max)
            move = max;

        if (move < min)
            move = min;


        lol = transform.position - player.transform.position;
        lol = Vector3.Normalize(lol);
        lol = transform.InverseTransformVector(lol);

        Vector3 frente = transform.TransformDirection(Vector3.up);
        Debug.DrawRay(transform.position, frente, Color.green);

        #region Esta a ser selecionada

        if (moveWhere == 0)
        {
            if (aS.isPlaying)
                aS.Stop();
        }

        if (isBeingHeld == true)
        {

            #region Esquerda

            if (!rightSide)
            {

                #region Não Invertido
                if (lol.y > 0)
                {
                    if ((moveWhere == 2) && (move > min))
                    {
                        boxColl.transform.Translate(Vector3.right * vel * Time.deltaTime, Space.Self);
                        move--;
                    }
                    else
                         if ((moveWhere == 1) && move < max)
                    {
                        boxColl.transform.Translate(Vector3.left * vel * Time.deltaTime, Space.Self);
                        move++;
                    }
                    if (!aS.isPlaying)
                        aS.Play();
                }
                #endregion

                else

                #region Invertido
                if (lol.y < 0)
                {
                    if ((moveWhere == 2) && (move < max))
                    {
                        boxColl.transform.Translate(Vector3.left * vel * Time.deltaTime, Space.Self);
                        move++;
                    }
                    else
                    if ((moveWhere == 1) && move > min)
                    {
                        boxColl.transform.Translate(Vector3.right * vel * Time.deltaTime, Space.Self);
                        move--;
                    }
                    if (!aS.isPlaying)
                        aS.Play();
                }
                #endregion

            }

            #endregion

            else

            #region Direita

            if (rightSide)
            {
                //1 -> Direita 2 -> Esquerda
                #region Não invertido
                if (lol.y > 0)
                {
                    if ((moveWhere == 2) && (move < max))
                    {
                        boxColl.transform.Translate(Vector3.right * vel * Time.deltaTime, Space.Self);
                        move++;
                    }
                    else
                         if ((moveWhere == 1) && move > min)
                    {
                        boxColl.transform.Translate(Vector3.left * vel * Time.deltaTime, Space.Self);
                        move--;
                    }
                    if (!aS.isPlaying)
                        aS.Play();
                }
                #endregion

                else

                #region Invertido
                if (lol.y < 0)
                {
                    if ((moveWhere == 2) && (move > min))
                    {
                        boxColl.transform.Translate(Vector3.left * vel * Time.deltaTime, Space.Self);
                        move--;
                    }
                    else
                    if ((moveWhere == 1) && move < max)
                    {
                        boxColl.transform.Translate(Vector3.right * vel * Time.deltaTime, Space.Self);
                        move++;
                    }
                    if (!aS.isPlaying)
                        aS.Play();
                }
                #endregion

            }
            #endregion

        }

        #endregion




    }
}
