using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VR = UnityEngine.VR;

public class ControllerInput : MonoBehaviour {

    public float viscosityHigh = 1.0f;
    public float frictionHigh = 0.6f;
    public float frictionLow = 0.1f;
    public bool viscosityOn = false;
    private OVRPlayerController playerController;
    public GameObject sunObject;
    [SerializeField] private Toggle gravityToggle;
    [SerializeField] private Toggle swarmToggle;
    [SerializeField] private Toggle viscosityToggle;
    // Use this for initialization
    void Start () {
		
	}

    void Awake()
    {
        //get controller
        OVRPlayerController[] playerControllers;
        playerControllers = gameObject.GetComponentsInChildren<OVRPlayerController>();

        if (playerControllers.Length == 0)
        {
            Debug.LogWarning("OVRMainMenu: No OVRPlayerController attached.");
        }
        else if (playerControllers.Length > 1)
        {
            Debug.LogWarning("OVRMainMenu: More then 1 OVRPlayerController attached.");
        }
        else
        {
            playerController = playerControllers[0];
        }

    }
	// Update is called once per frame
	void Update ()
    {

        //Jump
        if (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("Jump")) playerController.Jump();

        //Turn on/off "true" gravity
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Gravity"))
        {
            foreach (GameObject arrayObject in ArrayCreate.arrayObjects)
            {
                arrayObject.GetComponent<Rigidbody>().useGravity = !arrayObject.GetComponent<Rigidbody>().useGravity;
            }

            gravityToggle.isOn = !gravityToggle.isOn;

        }

        //Turn on/off "swarm" gravity
        if (Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("Swarm"))
        {
            ArrayCreate.arrayGravAct = !ArrayCreate.arrayGravAct;
            swarmToggle.isOn = !swarmToggle.isOn;
        }

        if (Input.GetKey(KeyCode.T) || Input.GetButton("Time"))
        {
            sunObject.GetComponent<SunMovement>().MoveSun();
        }

        //Turn on/off object viscosity
        if (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("Viscosity"))
        {
            viscosityOn = !viscosityOn;

            viscosityToggle.isOn = !viscosityToggle.isOn;

            if (viscosityOn == true)
            {
                foreach (GameObject arrayObject in ArrayCreate.arrayObjects)
                {
                    arrayObject.GetComponent<Rigidbody>().drag = viscosityHigh;
                    arrayObject.GetComponent<Rigidbody>().angularDrag = viscosityHigh;
                    arrayObject.GetComponent<BoxCollider>().material.staticFriction = frictionHigh;
                    arrayObject.GetComponent<BoxCollider>().material.dynamicFriction = frictionHigh;
                }
            }
            else
            {
                foreach (GameObject arrayObject in ArrayCreate.arrayObjects)
                {
                    arrayObject.GetComponent<Rigidbody>().drag = 0;
                    arrayObject.GetComponent<Rigidbody>().angularDrag = 0;
                    arrayObject.GetComponent<BoxCollider>().material.staticFriction = frictionLow;
                    arrayObject.GetComponent<BoxCollider>().material.dynamicFriction = frictionLow;
                }
            }
        }
    }
}
