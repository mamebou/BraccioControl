﻿using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
using System;

public class HandTrackingTest : MonoBehaviour
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
    public GameObject homePosition;
    public GameObject startButton;
    public GameObject resetButton;
    public float countDown = 3f;
    public bool resetRobot = false;
    public GameObject IKsolver;
    private SolveIK IK;
    public GameObject directionText;

    //実験用
    public GameObject exController;
    private ExperimentManager exManager;


    void Start()
    {
        indexTip = GameObject.CreatePrimitive(PrimitiveType.Cube);  
        indexTip.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);  
        IK = IKsolver.GetComponent<SolveIK>();

        //実験用
        exManager = exController.GetComponent<ExperimentManager>();

    }

    void Update()
    {
        
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Right, out MixedRealityPose pose))
        {
            indexTip.transform.position = pose.Position; 
            indexTip.transform.rotation = pose.Rotation; 
            if(isStart){
                if(isFinishCount){
                    if(isFirstDitect == true){
                        dPosition[0] = 0f;
                        dPosition[1] = 0f;
                        dPosition[2] = 0f;
                        drotation[0] = 0f;
                        drotation[1] = 0f;
                        drotation[2] = 0f;
                        isFirstDitect = false;
                    }else{
                        //amount of change
                        dPosition[0] = indexTip.transform.position.x - pPos[0];
                        dPosition[1] = indexTip.transform.position.y - pPos[1];
                        dPosition[2] = indexTip.transform.position.z - pPos[2];
                        drotation[0] = indexTip.transform.rotation.x - pRot[0];
                        drotation[1] = indexTip.transform.rotation.y - pRot[1];
                        drotation[2] = indexTip.transform.rotation.z - pRot[2];
                    }

                    Vector3 pos = transform.position;
                    pos.x = target.transform.position.x + dPosition[0] * adjust;
                    pos.y = target.transform.position.y + dPosition[1] * adjust;
                    pos.z = target.transform.position.z + dPosition[2] * adjust;

                    Quaternion rot = transform.rotation;
                    rot.x = target.transform.rotation.x + drotation[0];
                    rot.y = target.transform.rotation.y + drotation[1];
                    rot.z = target.transform.rotation.z + drotation[2];

                    //update position
                    target.transform.position = pos;

                    //update rotation
                    target.transform.rotation = rot;

                    pPos[0] = indexTip.transform.position.x;
                    pPos[1] = indexTip.transform.position.y;
                    pPos[2] = indexTip.transform.position.z;
                    pRot[0] = indexTip.transform.rotation.x;
                    pRot[1] = indexTip.transform.rotation.y;
                    pRot[2] = indexTip.transform.rotation.z;
                }
                else{
                    if(judgeDistance(pose)){
                        countDown -= Time.deltaTime;
                        if(countDown < 0f){
                            isFinishCount = true;
                            homePosition.SetActive(false);
                            directionText.SetActive(false);
                        }
                    }
                    else{
                        countDown = 3f;
                    }
                }
            }
            
        }
    }

    public static bool IsPinching(Handedness trackedHand)
    {
        return HandPoseUtils.CalculateIndexPinch(trackedHand) > PinchThreshold;
    }

    public static bool IsGrabbing(Handedness trackedHand)
    {
        return !IsPinching(trackedHand) &&
                HandPoseUtils.MiddleFingerCurl(trackedHand) > GrabThreshold &&
                HandPoseUtils.RingFingerCurl(trackedHand) > GrabThreshold &&
                HandPoseUtils.PinkyFingerCurl(trackedHand) > GrabThreshold &&
                HandPoseUtils.ThumbFingerCurl(trackedHand) > GrabThreshold;
    }

    public void start(){
        startButton.SetActive(false);
        resetButton.SetActive(false);
        if(!isFinishCount){
            homePosition.SetActive(true);
            directionText.SetActive(true);
        }

        
        isStart = true;

        //実験用
        if(!exManager.startExperiment){
            exManager.startExperiment = true;
            exManager.startTime = DateTime.Now;

        }
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
        if(startButton.activeSelf){
            isStart = true;
            startButton.SetActive(false);
            resetButton.SetActive(false);
        }
        else{
            isStart = false;
            startButton.SetActive(true);
            resetButton.SetActive(true);
        }
    }

    public bool judgeDistance(MixedRealityPose pose){
        double x = (double)pose.Position.x - (double)homePosition.transform.position.x;
        double y = (double)pose.Position.y - (double)homePosition.transform.position.y;
        double z = (double)pose.Position.z - (double)homePosition.transform.position.z;
        double distance = Math.Sqrt(x * x) + Math.Sqrt(y * y) + Math.Sqrt(z * z);
        if(distance < 0.1){
            return true;
        }
        else{
            return false;
        }
    }


}