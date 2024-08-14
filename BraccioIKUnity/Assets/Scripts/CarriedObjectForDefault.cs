using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriedObjectForDefault : MonoBehaviour
{
    public GameObject robotController;
    private MQTTTest controller;
    private bool followHead = false;
    private bool beforeGrab;
    public GameObject robotHead;
    // Start is called before the first frame update
    void Start()
    {
        controller = robotController.GetComponent<MQTTTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if(followHead){
            this.gameObject.transform.position = robotHead.transform.position;
        }
    }

    void OnCollisionStay(Collision collision)
    {


        if (collision.gameObject.tag == "head"){
            Debug.Log("Collision with head");
            if(beforeGrab != null && controller.isGrip != beforeGrab && !followHead && controller.isGrip)
                followHead = true;
            if(beforeGrab != null && controller.isGrip != beforeGrab &&  followHead && !controller.isGrip)
                followHead = false;
            
            beforeGrab = controller.isGrip;
        }
    }
}
