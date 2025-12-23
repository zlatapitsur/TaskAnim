using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor10c : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetBool("IsOpen", true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("IsOpen", false);    
    }
}
