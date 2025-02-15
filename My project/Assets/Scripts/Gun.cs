using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private Camera charCamera;

    protected override void Awake()
    {

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
}
