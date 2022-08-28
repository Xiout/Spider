using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> legGroup1;
    public List<GameObject> legGroup2;
    public GameObject head;

    public float maxAngle;
    public float speedLeg; //recommended : 100

    private bool isGroup1Up;
    private Spider spider;

    void Start()
    {
        isGroup1Up = false;
        spider = gameObject.GetComponent<Spider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spider.isWalking)
        {
            WalkAnimation();

            //Todo  : get the position at the bottom of the foot and make sure this point is at y=0 by tranlate up/down spider while walking
            //transform.Translate(Vector3.up * -FindOffsetWithFloor(0));
        }
    }

    public float FindOffsetWithFloor(float groundPosition)
    {
        float offset = 0;
        float currentOffset = 0;
        List<GameObject> allLegs = new List<GameObject>();
        allLegs.AddRange(legGroup1);
        allLegs.AddRange(legGroup2);

        for(int i=0; i<allLegs.Count; ++i)
        {
            Transform tarsusTransf = allLegs[i].transform.Find("Tarsus");
            Vector3 tarsusWorldPos = tarsusTransf.localToWorldMatrix * tarsusTransf.position;
            currentOffset = tarsusWorldPos.y - groundPosition;

            Debug.Log(allLegs[i].transform.parent.name+"/"+allLegs[i].name+" world pos :"+tarsusWorldPos+" current offset : "+ (-currentOffset));

            if(i==0 || currentOffset< offset)
            {
                offset = currentOffset;
            }
        }

        return offset;
    }

    public void WalkAnimation()
    {
        List<GameObject> upGroup, downGroup;
        if (isGroup1Up)
        {
            upGroup = legGroup1;
            downGroup = legGroup2;
        }
        else
        {
            upGroup = legGroup2;
            downGroup = legGroup1;
        }

        bool hadBenUpdated = false;
        for(int i=0;  i<upGroup.Count; ++i)
        {
            bool isLeft = false;
            if (upGroup[i].name.Contains("Left"))
                isLeft = true;

            if(isLeft)
                upGroup[i].transform.RotateAround(head.transform.position, new Vector3(1, 0, 0), Time.deltaTime* speedLeg);
            else
                upGroup[i].transform.RotateAround(head.transform.position, new Vector3(1, 0, 0), Time.deltaTime *-speedLeg);

            float xEulerAngle180 = Mathf.Repeat(upGroup[i].transform.localEulerAngles.x + 180, 360) - 180;
            if (!hadBenUpdated && (xEulerAngle180<=-maxAngle))
            {
                //Debug.Log("CHANGE " + upGroup[i].transform.parent.name + "/" + upGroup[i].name + " " + upGroup[i].transform.localEulerAngles.x+" "+ xEulerAngle180);
                isGroup1Up = !isGroup1Up;
                hadBenUpdated = true;
            }
        }
        for (int i = 0; i < downGroup.Count; ++i)
        {
            bool isLeft = false;
            if (downGroup[i].name.Contains("Left"))
                isLeft = true;

            if (isLeft)
                downGroup[i].transform.RotateAround(head.transform.position, new Vector3(1, 0, 0), Time.deltaTime * -speedLeg);
            else
                downGroup[i].transform.RotateAround(head.transform.position, new Vector3(1, 0, 0), Time.deltaTime * speedLeg);
        }
    }
}
