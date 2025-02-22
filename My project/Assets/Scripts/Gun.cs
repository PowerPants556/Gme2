using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gun : Weapon
{
    [SerializeField] private Camera charCamera;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shootFX;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Animator gunAnimator;

    private PhotonView pView;

    protected override void Awake()
    {
        pView = GetComponent<PhotonView>();
        gunAnimator = GetComponent<Animator>;
    }
    protected override void Start()
    {

    }
    protected override void Update()
    {

    }
    public override void Use()
    {
        Shoot();
    }

    private void Shoot()
    {
        Ray ray = charCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        ray.origin = charCamera.transform.position;
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((WeaponData)data).Damage);
        }
    }

    [PunRPC]
    private void RPC_Shoot(Vector3 hitPoint, Vector3 hitNormal)
    {

    }
}
