﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Cable_end : Interactible
{
    private temporalCable cable;
    private Transform lastParent;

    // Use this for initialization
    void Start()
    {
        cable = this.transform.parent.GetComponent<temporalCable>();
    }

    // Update is called once per frame
    void Update()
    {

        if (cable.energy == true)
        {
            this.transform.tag = "Interactible";
        }
        else
        {
            this.transform.tag = "Untagged";
        }

    }

    public override void Action(BoyMovement _boy)
    {
        if (_boy.grabbedObject == null)
        {
            //Grabs
            _boy.grabbedObject = this;
            lastParent = this.transform.parent;
            this.transform.SetParent(_boy.hand.transform);
            this.transform.localPosition = Vector3.zero;
        }

        else
        {
            Collider[] objectsInArea = null;
            objectsInArea = Physics.OverlapSphere(transform.position, 2f, 1 << 14);

            float minDistance = 0;
            GameObject closest = null;

            //Checks if there are possible parents, like plugs.
            if (objectsInArea.Length > 1)
            {
                for (int i = 0; i < objectsInArea.Length; i++)
                {
                    if (objectsInArea[i].GetComponent<BU_Plug>() != null)
                    {
                        float distance = Vector3.Distance(objectsInArea[i].transform.position, this.gameObject.transform.position);

                        if (minDistance == 0 || minDistance > distance)
                        {
                            minDistance = distance;
                            closest = objectsInArea[i].gameObject;
                        }
                    }
                }

                this.transform.SetParent(closest.transform);
                this.transform.localPosition = Vector3.zero;

                _boy.grabbedObject = null;
            }

            else
            {
                this.transform.SetParent(lastParent);
                _boy.grabbedObject = null;
            }
        }

    }
}
