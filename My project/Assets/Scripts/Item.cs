using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected ItemData data;
    protected GameObject itemObject;

    protected abstract void Awake();
    protected abstract void Start();
    protected abstract void Update();
    protected abstract void Use();
}
