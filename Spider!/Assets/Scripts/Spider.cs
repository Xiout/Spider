using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    //public bool isWalking { get; private set; }
    public bool isWalking;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        isWalking = true;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 spiderParentPosition = transform.position;
            Vector3 spiderParentRotation = transform.eulerAngles;
            Vector3 spiderHeadPosition = transform.Find("Head").position;
            Vector3 spiderBodyPosition = transform.Find("Body").position;

            bool hasMove = false;

            Vector3 vectorToPlayer = new Vector3(playerPosition.x - spiderParentPosition.x, 0 , playerPosition.z - spiderParentPosition.z);
            Vector3 vectorBodyToHead = new Vector3(spiderHeadPosition.x - spiderBodyPosition.x, 0, spiderHeadPosition.z - spiderBodyPosition.z);

            float angleToPlayer = Vector3.SignedAngle(vectorBodyToHead, vectorToPlayer, Vector3.up);
            Vector3 eulerRotationSpider = new Vector3(0, angleToPlayer * rotationSpeed * Time.deltaTime, 0);

            //Quaternion quaternionRotationSpider = Quaternion.Euler(eulerRotationSpider);
            //Quaternion quaternionFromTo = Quaternion.FromToRotation(vectorBodyToHead, vectorToPlayer);


            if (Mathf.Abs(angleToPlayer) < 90)
            {
                Vector3 movementDirection;
                if (vectorToPlayer.x < 0)
                {
                    movementDirection = -vectorBodyToHead.normalized;
                }
                else
                {
                    movementDirection = vectorBodyToHead.normalized;
                }
                
                transform.Translate(movementDirection * Time.deltaTime * speed);
                hasMove = true;
            }

            transform.Rotate(eulerRotationSpider, Space.Self);
            //transform.SetPositionAndRotation(transform.position, Quaternion.LookRotation(vectorToPlayer));
            //transform.SetPositionAndRotation(transform.position, quaternionFromTo);
            //transform.SetPositionAndRotation(transform.position, Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(vectorToPlayer), Time.deltaTime * rotationSpeed));

            Debug.Log(
                $"Start playerPosition : {playerPosition} \n" +
                $"Start SpiderParentPosition : {spiderParentPosition} \n" +
                $"Start spiderHeadPosition : {spiderHeadPosition} \n" +
                $"Start spiderBodyPosition : {spiderBodyPosition} \n" +
                $"Start spiderParentRotation : {spiderParentRotation} \n" +
                $"vectorToPlayer : {vectorToPlayer} \n" +
                $"vectorBodyToHead : {vectorBodyToHead} \n" +
                $"angleToPlayer : {angleToPlayer}° \n" +
                $"eulerRotation : {eulerRotationSpider} \n" +
                //$"lookRotation :{Quaternion.LookRotation(vectorToPlayer).eulerAngles} \n" +
                $"hasMove : {hasMove} \n" +
                $"End playerPosition : {player.transform.position} \n" +
                $"End SpiderParentPosition : {transform.position} \n" +
                $"End spiderHeadPosition : {transform.Find("Head").position} \n" +
                $"End spiderBodyPosition : {transform.Find("Body").position} \n" +
                $"End spiderParentRotation : {transform.rotation} \n" +
                "");
            
        }
    }
}
