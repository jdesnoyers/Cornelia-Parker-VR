using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayCreate : MonoBehaviour {

    [SerializeField] private Rigidbody arrayOriginal;
    [SerializeField] private Transform arrayParent;

    public int arraySizeX; //number of instances in X
    public int arraySizeY; //number of instances in Y
    public int arraySizeZ; //number of instances in Z
    public float arraySpacing; //spacing between instances in all directions
    public float arrayRandomize; //randomize positions
    public float arrayAngle; //add slope to array
    public static bool arrayGravAct = false; //boolean to activate/deactivate local gravity
    public float gravityConstantSet = 6.674E-11f;
    public static float gravityConstant = 6.674E-11f;

    [HideInInspector] public static float totalMass; //stores total mass of array
    [HideInInspector] public static int arraySizeN; //stores total size of array
    [HideInInspector] public static GameObject[] arrayObjects = null; //array of objets with tag "arrayObject"
    [HideInInspector] public static Vector3 arrayCOM; //array center of mass

    // Use this for initialization
    void Start () {      

        for (int i = 0; i < arraySizeX; i++)
        {
            for (int j = 0; j < arraySizeY; j++)
            {
                for(int k = 0; k < arraySizeZ; k++)
                {
                    Vector3 arrayTranslate = new Vector3(i+(Random.Range(-1.0f,1.0f)*arrayRandomize), j + (Random.Range(-1.0f, 1.0f) * arrayRandomize)+ (i*arrayAngle), k + (Random.Range(-1.0f, 1.0f) * arrayRandomize) + (i * arrayAngle));
                    Instantiate(arrayOriginal,arrayParent.position + (arrayTranslate * arraySpacing),arrayOriginal.transform.rotation, arrayParent);
                }
                
            }
        }

        //define array of objects
        arrayObjects = GameObject.FindGameObjectsWithTag("arrayObject");

        //calculate total mass
        foreach (GameObject arrayObject in arrayObjects)
            totalMass += arrayObject.GetComponent<Rigidbody>().mass;

        arraySizeN = arrayObjects.Length;
        gravityConstant = gravityConstantSet;
            
    }
	
	// Update is called once per frame
	void Update () {

        
        Vector3 sumCOM = Vector3.zero; //create/reset vector3

        // calculate center of mass
        foreach (GameObject arrayObject in arrayObjects)
        {
            
            sumCOM += arrayObject.GetComponent<Rigidbody>().worldCenterOfMass;
        }

        arrayCOM = sumCOM / arraySizeN; //calculate average

    }
}
