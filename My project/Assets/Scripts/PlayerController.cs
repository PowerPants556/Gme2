using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PhotonView pView;
    [SerializeField] private GameObject cameraHolder;

    private Vector3 smoothMove, moveAmount;

    private float walkSpeed, sprintSpeed, mouseSensetivity,
        jumpForce, smoothTime, verticalLookRotation;
    public bool isGround { get; set; }
    private void Start()
    {
        if (!pView)
        {
            Destroy(playerCamera);
            Destroy(rb);
        }
    }

    private void Update()
    {
        if (!pView.IsMine) return;
        Look();
        Movement();
        Jump();
    }

    private void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensetivity);
        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensetivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -80f, 90f);
        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    private void Movement()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMove, smoothTime);
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
}
