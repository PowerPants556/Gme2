using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NickNameBoard : MonoBehaviour
{
    [SerializeField] private Camera playerCam;

    private void Update()
    {
        if(playerCam != null)
        {
            transform.LookAt(playerCam.transform);
            transform.Rotate(Vector3.up * 180);
        }
    }
}
