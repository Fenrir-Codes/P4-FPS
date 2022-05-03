using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fillAmmo : MonoBehaviour
{
    gun gun;
    CharacterController ctrl;
    private void Start()
    {
        gun = GetComponentInChildren<gun>();
    }
    private void OnTriggerEnter(Collider other)
    {
        gun = other.gameObject.GetComponent<gun>();
        if (ctrl)
        {
            gun.addAmmo(gun.maxAmmunition);
        }
        //Destroy(gameObject);
    }
}
