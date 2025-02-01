using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Collider playerCollider;
    [SerializeField] private Collider checkerCollider;

    private void Start()
    {
        Physics.IgnoreCollision(checkerCollider, playerCollider);
    }
    private void OnTriggerEnter(Collider col)
    {
            player.isGround = true;
    }

    public void OnTriggerExit(Collider col)
    {
            player.isGround = false;
    }
}
