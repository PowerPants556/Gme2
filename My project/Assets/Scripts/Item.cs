using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IUseable
{
    [SerializeField] protected ItemData data;
    [SerializeField] protected GameObject itemObject;

    public GameObject ItemObject
    {
        get { return itemObject; }
    }
   
    protected abstract void Awake();
    protected abstract void Start();
    protected abstract void Update();
    public abstract void Use();
}
