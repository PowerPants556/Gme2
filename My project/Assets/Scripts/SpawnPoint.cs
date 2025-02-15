using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    [SerializeField] private GameObject render;

    private void Start()
    {
        render.SetActive(false);
    }
}
