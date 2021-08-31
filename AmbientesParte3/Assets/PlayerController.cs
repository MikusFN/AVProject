using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jSpeed;
    public float gravity;
    public Vector3 moveDirection;

    private Quaternion q_player;
    private Quaternion q_camera;
    private Quaternion q_weapon;
    private Transform t;

    public float mouseSpeed;

    Camera cam;
    public GameObject weapon;
    public GameObject lantern;
    CharacterController charCtrl;
    //ParticleSystem syst;
    Rigidbody rb;

    public float stamina;
    public float runningSpeed;
    public float xRot;
    public float yRot, yRot_last;

    //Maquina de Estados
    public bool running;
    public bool andando;
    public bool ofegante;
    public bool death = false;
    public Collider lastSelected = null;

    //Pages
    public bool page1;

    //Sounds
    public AudioSource soundPlayer_moving;
    public AudioSource soundPlayer_mouth;
    public AudioClip footsteps;
    public AudioClip runningSound;
    public AudioClip offbreath;

    // Use this for initialization
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        //syst = GetComponentInChildren<ParticleSystem>();
        //syst.Stop();
        //Lock Mouse
        Cursor.lockState = CursorLockMode.Locked;
        q_player = charCtrl.transform.localRotation;
        q_camera = cam.transform.localRotation;
        q_weapon = weapon.transform.localRotation;
        stamina = 100f;
        runningSpeed = 20;
        speed = 10;
        yRot = 0;
        page1 = false;

        //InvokeRepeating("walking_on_a_floor", 0.0f, 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!death)
        {
            PlaySounds();
            if (t != null)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Transform p = t;
                    p.parent = null;
                    p.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;


                    p.GetComponent<Rigidbody>().useGravity = true;
                    p.position += Vector3.forward * 2;
                    p.GetComponent<SphereCollider>().isTrigger = true;
                    p.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
                    p.GetComponent<LanternControl>().beingMoved = false;
                    Instantiate(p);
                    Destroy(t.gameObject);
                }
            }

            yRot_last = yRot;

            //Movement
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed + (runningSpeed * Convert.ToInt32(running));




            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                running = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                running = false;
            }

            if (running)
                stamina -= 50 * Time.deltaTime;
            else
                stamina += 1f;

            if (stamina > 100)
                stamina = 100;
            if (stamina < 0)
            {
                running = false;
                stamina = 0;
                ofegante = true;
            }

            if (stamina > 65 && ofegante == true)
            {
                ofegante = false;
            }

            moveDirection.y -= gravity * Time.deltaTime;

            if (charCtrl.isGrounded == false)
            {
                gravity += 10 + (runningSpeed * Convert.ToInt32(running));
            }

            if (moveDirection.x != 0 && moveDirection.z != 0)
                andando = true;
            else
                andando = false;

            charCtrl.Move(moveDirection * Time.deltaTime);


            ////Shooting
            //if (Input.GetButton("Fire1"))
            //{
            //    syst.Stop();
            //    syst.Play();
            //}

            //Move Camera/View
            xRot = Input.GetAxis("Mouse Y") * mouseSpeed;
            yRot = Input.GetAxis("Mouse X") * mouseSpeed;

            q_player *= Quaternion.Euler(0.0f, yRot, 0.0f);
            q_camera *= Quaternion.Euler(-xRot, 0.0f, 0.0f);
            //q_weapon *= Quaternion.Euler(-xRot, yRot, 0.0f);

            transform.localRotation = q_player;
            cam.transform.localRotation = q_camera;
            weapon.transform.localRotation = q_weapon;
        }
    }

    void PlaySounds()
    {
        if (!running)
        {
            if (andando && !soundPlayer_moving.isPlaying)
            {
                soundPlayer_moving.Play();
            }
            else if (!andando && soundPlayer_moving.isPlaying)
            {
                soundPlayer_moving.Stop();
            }

        }
        else
        {
            if (andando && !soundPlayer_moving.isPlaying)
            {
                soundPlayer_moving.Play();
            }
            else if (!andando && soundPlayer_moving.isPlaying)
            {
                soundPlayer_moving.Stop();
            }
        }

        if (ofegante && !soundPlayer_mouth.isPlaying)
        {
            soundPlayer_mouth.Play();
        }
        else if (!ofegante && soundPlayer_mouth.isPlaying)
        {
            soundPlayer_mouth.Stop();
        }

        if (running)
           soundPlayer_moving.clip = runningSound;
        else
           soundPlayer_moving.clip = footsteps;
        if (ofegante)
            soundPlayer_mouth.clip = offbreath;

    }

    void OnTriggerStay(Collider other)
    {
        #region Portas
        float interactionDistance = 25f;
        //if (other.tag == ("porta"))
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(transform.position, cam.transform.forward, out hit, interactionDistance + 10))
                {
                    if (hit.collider.tag == "porta")
                    {
                        Debug.Log("hit");
                        lastSelected = hit.collider;
                        //if (lastSelected != null)

                        lastSelected.GetComponent<DoorMoving>().isBeingHeld = true;
                        float val = 0;
                        if (yRot < 0)
                            val = yRot;
                        if (yRot > 0)
                            val = yRot;
                        if (yRot == 0)
                            val = 0;
                        lastSelected.GetComponent<DoorMoving>().Move(val);

                    }
                    else if (other.gameObject.tag == "porta")
                    {
                        other.GetComponent<DoorMoving>().isBeingHeld = false;
                        lastSelected = null;
                    }
                }
                else if (other.gameObject.tag == "porta")
                {
                    other.GetComponent<DoorMoving>().isBeingHeld = false;
                    lastSelected = null;
                }
            }
            else if (!(Input.GetMouseButtonDown(0)) && other.gameObject.tag == "porta")
            {
                other.GetComponent<DoorMoving>().isBeingHeld = false;
                lastSelected = null;
            }
        }
        #endregion

        #region Lanternas e Papeis
        Debug.DrawRay(transform.localPosition, cam.transform.forward * interactionDistance, Color.green);
        RaycastHit hits = new RaycastHit();

        if (Physics.Raycast(transform.position, cam.transform.forward, out hits, interactionDistance))
        {
            if (hits.transform.gameObject.tag == "paper")
            {
                hits.transform.GetComponent<PageControl>().beingLookAt = true;
            }
        }
        else
        {
            if (other.transform.gameObject.tag == "paper")
            {
                other.GetComponent<PageControl>().beingLookAt = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && t == null)
        {
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(transform.position, cam.transform.forward, out hit, interactionDistance))
            {
                if (hit.transform.gameObject.tag == "lantern")
                {
                    t = hit.transform;
                    //t.GetComponent<Rigidbody>().useGravity = false;
                    t.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    t.position = weapon.transform.position;
                    t.position += new Vector3(0, -1.5f, 0);
                    t.GetComponent<SphereCollider>().isTrigger = false;
                    t.GetComponent<LanternControl>().beingMoved = true;
                    t.parent = weapon.transform;
                    //t.transform.position += new Vector3(0.25f,-1.0f,1f);
                    //t.localScale = new Vector3(0.95f, 0.95f, 0.95);

                    //weapon = hit.collider.gameObject;
                    //weapon.GetComponent<Renderer>().
                }
                else { }

                if (hit.transform.gameObject.tag == "paper")
                {
                    hits.transform.GetComponent<PageControl>().beingLookAt = true;
                    hits.transform.GetComponent<PageControl>().pickedUp = true;
                    page1 = true;
                    
                    //t.transform.position += new Vector3(0.25f,-1.0f,1f);
                    //t.localScale = new Vector3(0.95f, 0.95f, 0.95);

                    //weapon = hit.collider.gameObject;
                    //weapon.GetComponent<Renderer>().
                }
                else
                {
                    if (other.gameObject.tag == "paper")
                        other.GetComponent<PageControl>().beingLookAt = false;
                }
            }

        }

        #endregion

        if (other.transform.gameObject.tag == "death")
        {
            death = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == ("porta"))
        {
            other.GetComponent<DoorMoving>().isBeingHeld = false;
            other.GetComponent<DoorMoving>().Move(0);
            //lastSelected = null;
        }

        if (other.tag == ("paper"))
        {
            other.GetComponent<PageControl>().beingLookAt = false;

        }

    }

}