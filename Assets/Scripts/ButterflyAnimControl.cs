using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyAnimControl : MonoBehaviour
{
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator.speed = Random.Range(0.25f, 3f);
    }
}
