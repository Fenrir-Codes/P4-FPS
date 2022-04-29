using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnHealtBarUI : MonoBehaviour
{
    public Transform cam;
    void Update()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
