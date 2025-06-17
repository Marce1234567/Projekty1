using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //public Transform Baltransform;
    private GameObject BallObject;
    public Vector3 dystans;
    public float lookUp;
    public float LerpAmaunt;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        BallObject = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.Lerp(transform.position, BallObject.transform.position + dystans, LerpAmaunt * Time.deltaTime);
        transform.LookAt(BallObject.transform.position);
        transform.Rotate(-lookUp, 0, 0);

    }
    private void FixedUpdate()
    {
        //transform.position = Vector3.Lerp(transform.position, BallObject.transform.position + dystans, LerpAmaunt * Time.deltaTime);
        //transform.LookAt(BallObject.transform.position);
        //transform.Rotate(-lookUp, 0, 0);
    }
}
