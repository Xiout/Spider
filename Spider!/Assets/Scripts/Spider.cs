using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 spiderParentPosition = transform.position;
        Vector3 spiderHeadPosition = transform.Find("Spider_Mesh").Find("Head").position;
        Vector3 spiderBodyPosition = transform.Find("Spider_Mesh").Find("Body").position;

        //For vectorBodyToHead and vectorToPlayer, Y is ignored because we are looking for the projection of the angle on the plan XZ
        Vector3 vectorToPlayer = new Vector3(playerPosition.x - spiderParentPosition.x, 0, playerPosition.z - spiderParentPosition.z);
        //Used as the direction to ensure that the spider's head is always first
        Vector3 vectorBodyToHead = new Vector3(spiderHeadPosition.x - spiderBodyPosition.x, 0, spiderHeadPosition.z - spiderBodyPosition.z);
            
            
        //Angle to rotation along Y axis in order to face the player
        float angleToPlayer = Vector3.SignedAngle(vectorBodyToHead, vectorToPlayer, Vector3.up);
        //Partial rotation using time so appears smoother
        Vector3 eulerRotationSpider = new Vector3(0, angleToPlayer * rotationSpeed * Time.deltaTime, 0);

        if (Mathf.Abs(angleToPlayer) < 90)
        {
            //If the player is currently out or the field of vision of the spider, do not move to prevent walking in the wrong direction
            Vector3 movementDirection;
            if (vectorToPlayer.x < 0)
            {
                //The vector are 
                movementDirection = -vectorBodyToHead.normalized;
            }
            else
            {
                movementDirection = vectorBodyToHead.normalized;
            }

            //TODO 22-08-30 : investiguate if possible to use AddForce instead without breaking the game
            transform.Translate(movementDirection * Time.deltaTime * speed);
        }

        transform.Rotate(eulerRotationSpider, Space.Self);
    }
}
