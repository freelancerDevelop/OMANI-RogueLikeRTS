﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Plug : MonoBehaviour
{

    MeshRenderer mesh;
    bool givenEnergy;
    public int energy;
    BU_WeaponsBay parentBuilding;

    // Use this for initialization
    void Start()
    {
        mesh = this.gameObject.GetComponent<MeshRenderer>();
        parentBuilding = this.gameObject.transform.parent.parent.parent.GetComponent<BU_WeaponsBay>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (this.transform.childCount > 0)
        {
            //if (energy == false)
            //{
              //  if (this.transform.GetChild(0).tag == "Interactible")
               // {
                    energy = 1;
                    mesh.material.color = Color.yellow;
                //}
            }
            /*else
            {
                energy = false;
                mesh.material.color = Color.white;
            }*/

        //}
    }

}
