using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetController1 : MonoBehaviour
{
    public Vector3 positionDif;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "block"){
            positionDif = Vector3.Distance(this.transform.position, collision.gameObject.transform.position);
        }
    }
}
