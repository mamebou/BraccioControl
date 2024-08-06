using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class targetControllerLatest : MonoBehaviour
{
    public Vector3 positionDif;
    public GameObject manager;
    private ExperimentManager exManager;
    // Start is called before the first frame update
    void Start()
    {
        exManager = manager.GetComponent<ExperimentManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "block"){
            exManager.accuracyResult[exManager.index] = Vector3.Distance(this.transform.position, collision.gameObject.transform.position);
            if(exManager.index == 2){
                exManager.finishExperiment = true;
                exManager.endTime = DateTime.Now;
            }
            exManager.index += 1;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
