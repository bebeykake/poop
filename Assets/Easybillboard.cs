using UnityEngine;

public class EasyBillboard : MonoBehaviour
{

    Camera maincamera;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maincamera = Camera.main;


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newRotation = maincamera.transform.eulerAngles;

        newRotation.x = 0;
        newRotation.z = 0;

        transform.eulerAngles = newRotation;

        Debug.Log("newRotation");

        //transform.LookAt(maincamera.transform.position, Vector3.left);
    }
}