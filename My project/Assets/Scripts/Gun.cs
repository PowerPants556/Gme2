using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Gun : Weapon<GunData>
{
    [SerializeField] private Camera charCamera;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shootFX;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private TMP_Text ammoText;


    private PhotonView pView;
    private bool isReload = false;
    private int currentAmmo;

    protected override void Awake()
    {
        pView = GetComponent<PhotonView>();
        gunAnimator = GetComponent<Animator>();
        if (!pView.IsMine)
        {
            Destroy(ammoText);
        }
    }
    protected override void Start()
    {
        currentAmmo = data.maxAmmo;
        UpdateAmmoUI();
    }

    private void OnEnable()
    {
        UpdateAmmoUI();
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
        if (currentAmmo < 1 && !isReload) return;
        currentAmmo--;
        UpdateAmmoUI();
        //gunAnimator.Play("ShootAnim");
        Ray ray = charCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        ray.origin = charCamera.transform.position;
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((WeaponData)data).Damage);
            pView.RPC("RPC_SHOOT", RpcTarget.All, hit.point, hit.normal);
        }
        if (currentAmmo < 1 && !isReload)
        {
            isReload = true;
            Invoke("Reload", data.reloadTime);
        }
    }

    [PunRPC]
    private void RPC_Shoot(Vector3 hitPoint, Vector3 hitNormal)
    {
        Collider[] colls = Physics.OverlapSphere(hitPoint, 0.1f);
        if(colls.Length != 0)
        {
            GameObject bulletImp = Instantiate(bulletPrefab, hitPoint,
                Quaternion.LookRotation(hitNormal, Vector3.up)* bulletPrefab.transform.rotation);
            bulletImp.transform.SetParent(colls[0].transform);
        }
    }

    private void UpdateAmmoUI()
    {
        ammoText.text = $"AMMO: {currentAmmo}";
    }

    private void Reload()
    {
        currentAmmo = data.maxAmmo;
        isReload = false;
        UpdateAmmoUI();
    }
}
