using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item<TData> : MonoBehaviour, IUseable where TData : ItemData
{
    [SerializeField] protected TData data;
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
