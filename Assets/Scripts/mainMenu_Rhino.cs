using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenu_Rhino : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        toIdle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void toIdle()
    {
        Debug.Log("IDLE: Player");
        animator.SetBool("isIdle", true);
    }
}
