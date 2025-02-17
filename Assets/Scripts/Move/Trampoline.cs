using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;

    public bool isOpen;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("pressed", true);
            isOpen = true;
            SoundManager.instance.PlaySound(jumpSound);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
                Invoke("SetAnimatorBool", 6f);
            
        }   
    }

    private void SetAnimatorBool()
    {
        animator.SetBool("pressed", false);
        isOpen = false;
    }
}