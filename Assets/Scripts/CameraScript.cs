using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Camera camera;
    private Animator animator;
    void Start()
    {
        camera = GetComponent<Camera>();
        animator = GetComponent<Animator>();
    }

    public void Shake()
    {
        animator.SetTrigger("ScreenShake");
    }

    public void BarrelRoll()
    {
        animator.SetTrigger("BarrelRoll");
    }
}
