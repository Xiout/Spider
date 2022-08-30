using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeBonus { SpeedBonus }

public class Bonus : MonoBehaviour
{
    //Visual Effect members
    public float deltaY;
    public float deltaScale;
    public float speed;
    private bool isGoingUp;
    private bool isGrowing;

    //Bonus mechanic members
    public TypeBonus typeBonus;
    public float value;
    public float duration;

    private Vector3 initialPos;
    private Vector3 initialScale;
    void Start()
    {
        isGoingUp = true;
        initialPos = transform.position;
        initialScale = transform.localScale;
    }

    void Update()
    {
        transform.Translate(0, (isGoingUp?1:-1) * Time.deltaTime * speed, 0);
        if (Mathf.Abs(transform.position.y - initialPos.y) >= deltaY)
        {
            isGoingUp = !isGoingUp;
        }

        float offsetScale = (isGrowing?1:-1) * Time.deltaTime * speed;
        transform.localScale = new Vector3(transform.localScale.x + offsetScale, transform.localScale.y + offsetScale, transform.localScale.z + offsetScale);
        if (Mathf.Abs(transform.localScale.x - initialScale.x) >= deltaScale)
        {
            isGrowing = !isGrowing;
        }
    }
}
