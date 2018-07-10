﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BU_WorkerMaker : BU_UniqueBuilding
{
    private BU_Resources_Workers workerMaker;
    private float timeToSpawnWorker = 45, desiredRotation;
    private float[] timeToSpawnWorkerCounter = new float[3];
    List<Image> workerClocks = new List<Image>();
    private bool[] workersReady = new bool[3];
    [SerializeField]
    private GameObject worker, spinningStructure;
    float biggestClockValue;

    public override void Start()
    {
        base.Start();

        workerMaker = this.transform.Find("WorkerMaker").GetComponent<BU_Resources_Workers>();
        spinningStructure = this.transform.Find("BU_UI/SpinningStructure").gameObject;

        foreach (Image clock in this.transform.Find("BU_UI/Production_Clocks").GetComponentsInChildren<Image>())
        {
            if (clock.name == "Clock")
            {
                workerClocks.Add(clock);
            }
        }

        requiredEnergy = 1;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (totalEnergy >= requiredEnergy)
        {
            WorkerMaker();
        }

        SpinningStructure();
    }


    void SpinningStructure()
    {

        desiredRotation = -((biggestClock() * (-180)) + 90);
        spinningStructure.transform.eulerAngles = new Vector3(0, desiredRotation, 0);

    }

    //Makes Workers
    private void WorkerMaker()
    {
        //Checks energy up to 3 to see how much it creates. Sends info to the clocks with @WorkerClocks.
        if (totalEnergy > 0)
        {
            //Used to see how many workers are going to be build.
            int calcTotalEnergy = totalEnergy;

            for (int i = 0; i < workersReady.Length; i++)
            {
                if (calcTotalEnergy > 0 && workersReady[i] == false)
                {
                    if (timeToSpawnWorkerCounter[i] < timeToSpawnWorker)
                    {
                        timeToSpawnWorkerCounter[i] += Time.deltaTime;

                        WorkerClocks(timeToSpawnWorkerCounter[i] / timeToSpawnWorker, i, Color.green);
                    }
                    if (timeToSpawnWorkerCounter[i] > timeToSpawnWorker)
                    {
                        workersReady[i] = true;
                        WorkerClocks(timeToSpawnWorkerCounter[i] / timeToSpawnWorker, i, Color.cyan);
                    }
                    calcTotalEnergy -= 1;
                }
            }
        }
    }

    public void MakeWorker()
    {
        for (int i = 0; i < 3; i++)
        {
            if (workersReady[i])
            {
                //Should be a pool later on
                Instantiate(worker, new Vector3(workerMaker.transform.position.x + Random.Range(-2f, 2f), workerMaker.transform.position.y, workerMaker.transform.position.z - 3f), Quaternion.identity);
                workersReady[i] = false;
                timeToSpawnWorkerCounter[i] = 0;
                //restart
                biggestClockValue = 0;
                WorkerClocks(timeToSpawnWorkerCounter[i] / timeToSpawnWorker, i, Color.green);
            }
        }

    }

    private float biggestClock()
    {
        for (int i = 0; i < 3; i++)
        {
            if (timeToSpawnWorkerCounter[i] / timeToSpawnWorker > biggestClockValue) { biggestClockValue = timeToSpawnWorkerCounter[i] / timeToSpawnWorker; }
        }
        return biggestClockValue;
    }

    private void WorkerClocks(float _fillAmount, int _clock, Color _color)
    {

        workerClocks[_clock].fillAmount = _fillAmount;
        workerClocks[_clock].color = _color;

    }
}
