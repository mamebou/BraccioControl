using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
using System;
using System.Threading;

public class HandTrackingButton : MonoBehaviour
{
    //表示するオブジェクトを定義
    GameObject indexTip;
    public GameObject target;
    private float[] dPosition = new float[3] {0f, 0f, 0f};
    private float[] drotation = new float[3] {0f, 0f, 0f};
    //hard coded 
    private int jointNum = 6;
    private float[] pPos = new float[3];
    private float[] pRot = new float[3];
    public float adjust = 5f;
    private bool isFirstDitect = true;

    private const float PinchThreshold = 0.7f;
    private const float GrabThreshold = 0.4f;
    public bool isGrab = false;
    public bool isStart = false;
    public bool isFinishCount = false;
    public GameObject startButton;
    public GameObject resetButton;
    public float countDown = 3f;
    public bool resetRobot = false;
    public GameObject IKsolver;
    private SolveIK IK;


    void Start()
    {

        IK = IKsolver.GetComponent<SolveIK>();

    }

    void Update()
    {
        
        
    }

    public void start(){
        isStart = true;

        startButton.SetActive(false);
        resetButton.SetActive(false);
    }

    public void reset(){
        isStart = false;
        isFirstDitect = true;
        isFinishCount = false;
        resetRobot = true;
        IK.targetPosition = IK.initialTargetPosition;
        IKsolver.transform.position = IK.initialTargetPosition;
    }

    public void menue(){

            isStart = false;
            startButton.SetActive(true);
            resetButton.SetActive(true);

    }

}