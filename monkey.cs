using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monkey : MonoBehaviour
{
    public float ForwardForce = 10f;
    public float TurnForce = 40f;
    public float JumpForce = 5f;
    public float CameraFollowSpeed = 1f;
    public Camera cam = null;

    private Animator anim = null;
    private Rigidbody rb = null;
    private Transform cameraposition = null;
    private Transform cameratarget = null;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        cameraposition = GetChildTransform("cameraposition", this.transform);
        cameratarget = GetChildTransform("cameratarget", this.transform);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            rb.AddRelativeForce(Vector3.forward * ForwardForce * Time.deltaTime, ForceMode.VelocityChange);
            anim.SetBool("isrunning", true);
            anim.speed = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            rb.AddRelativeForce(Vector3.back * ForwardForce * Time.deltaTime, ForceMode.VelocityChange);            
        }
        else
        {
            anim.SetBool("isrunning", false);
        }

        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            rb.AddRelativeTorque(Vector3.up * -TurnForce * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            rb.AddRelativeTorque(Vector3.up * TurnForce * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            anim.SetTrigger("jump");
            rb.AddRelativeForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
        }

        CameraPosition();
    }
    private void CameraPosition()
    {
        cam.transform.LookAt(cameratarget.position);

        float camMoveAmt = Mathf.Abs(Vector3.Distance(cameraposition.position, cam.transform.position) * CameraFollowSpeed);
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, cameraposition.position, camMoveAmt * Time.deltaTime);
    }
    private Transform GetChildTransform(string name, Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.name.ToLower() == name.ToLower())
                return child;
        }
        return null;
    }
}
