using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriedObject : MonoBehaviour
{
    public GameObject robotController;
    private RobotController controller;
    private bool followHead = false;
    private bool beforeGrab;
    public GameObject robotHead;
    // Start is called before the first frame update
    void Start()
    {
        controller = robotController.GetComponent<RobotController>();
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

        // もし接触している相手オブジェクトの名前が"Plane"ならば
        if (collision.gameObject.tag == "head"){
            Debug.Log("Collision with head");
            if(beforeGrab != null && controller.isGrab != beforeGrab && !followHead && controller.isGrab)
                followHead = true;
            if(beforeGrab != null && controller.isGrab != beforeGrab &&  followHead && !controller.isGrab)
                followHead = false;
            
            beforeGrab = controller.isGrab;
        }
    }
}
