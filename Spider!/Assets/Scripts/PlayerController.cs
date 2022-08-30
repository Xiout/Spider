using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject cameraParent;

    private Rigidbody playerRb;
    private Vector3 previousPosition;
    private List<float> speedBoosts;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        previousPosition = gameObject.transform.position;
        speedBoosts = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {
        playerRb.AddForce(new Vector3(Input.GetAxis("Vertical") * Time.deltaTime * speed, 0, -Input.GetAxis("Horizontal") * Time.deltaTime * speed));

        Vector3 deltaPosition = gameObject.transform.position - previousPosition;
        cameraParent.transform.Translate(deltaPosition.x, 0, deltaPosition.z);

        previousPosition = gameObject.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bonus"))
        {
            Bonus bonus = other.gameObject.GetComponent<Bonus>();
            if(bonus.typeBonus == TypeBonus.SpeedBonus)
            {
                speedBoosts.Add(bonus.value);
            }

            GameObject.Destroy(other.gameObject);
        }
    }
}
