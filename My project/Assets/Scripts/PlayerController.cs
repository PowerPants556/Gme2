using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviourPunCallbacks, IDamageable
{
    private const float MAX_HEALTH = 100;
    private PlayerManager playerManager;

    [SerializeField] private Item[] items;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PhotonView pView;
    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject ui;
    [SerializeField] private Animator visualDamageAnimator;
    //[SerializeField] private GameObject destroyFX;

    private Vector3 smoothMove, moveAmount;

    private float walkSpeed, sprintSpeed, mouseSensetivity,
        jumpForce, smoothTime, verticalLookRotation, currentHealth;

    private bool isReload = false;
    private int currentAmmo;

    private int itemIndex = -1;

    public bool isGround { get; set; }

    private void Awake()
    {
        currentHealth = MAX_HEALTH;
    }
    private void Start()
    {
        playerManager = PhotonView.Find((int)pView.InstantiationData[0]).GetComponent<PlayerManager>();
        if (!pView)
        {
            Destroy(ui);
            Destroy(playerCamera);
            Destroy(rb);
        }
        else
        {
            EquipItem(0);
        }
    }

    private void Update()
    {
        if (!pView.IsMine) return;
        Look();
        Movement();
        Jump();
        SelectWeapon();
        UseItem();
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

    private void SelectWeapon()
    {
        float mouseAxis = Input.GetAxisRaw("Mouse ScrollWheel");
        if (mouseAxis == 0) return;
        int modifier = 0;
        if(mouseAxis > 0)
        {
            modifier = 1;
        }
        else if(mouseAxis < 0)
        {
            modifier = -1;
        }
        int newIndex = itemIndex + modifier;
        if(newIndex < 0)
        {
            newIndex = items.Length - 1;
        }
        else if (newIndex > items.Length -1)
        {
            newIndex = 0;
        }
        EquipItem(newIndex);
        //EquipItem(Mathf.Clamp((itemIndex + modifier), 0, items.Length -1));
    }

    private void EquipItem(int newIndex)
    {
        if (newIndex == itemIndex) return;
        itemIndex = newIndex;
        if (itemIndex != -1)
            items[itemIndex].ItemObject.SetActive(false);
        items[itemIndex].ItemObject.SetActive(true);
        itemIndex = newIndex;

        if (pView.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("index", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

    private void UseItem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            items[itemIndex].Use();
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changeProps)
    {
        if (!pView.IsMine && targetPlayer == pView.Owner)
        {
            EquipItem((int)changeProps["index"]);
        }
    }

    public void TakeDamage(float damage)
    {
        pView.RPC("RPC_Damage", RpcTarget.All, damage);
    }

    [PunRPC]
    private void RPC_Damage(float damage)
    {
        if (!pView.IsMine) return;
        currentHealth -= Mathf.Clamp(currentHealth - damage, 0, MAX_HEALTH);
        healthBar.fillAmount = currentHealth / MAX_HEALTH;
        visualDamageAnimator.Play("takeDamage");
        if (currentHealth <= 0) playerManager.Die(transform.position); 
    }

}
