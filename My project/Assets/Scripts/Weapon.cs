using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    protected abstract override void Awake();
    protected abstract override void Start();
    protected abstract override void Update();
    protected abstract override void Use();
}
