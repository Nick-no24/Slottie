using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera activeCamera;
    public Camera inactiveCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            activeCamera.enabled = true;
            inactiveCamera.enabled = false;
        }
    }
}
