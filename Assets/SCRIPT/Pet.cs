using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Pet : MonoBehaviour
{
    [SerializeField] float runSpeed = 3f;
    [SerializeField] float snackDelay = 1f;
    Vector3 targetSnack;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        // Cached component references
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();  

        targetSnack=transform.position;
        //StartCoroutine(eatSnack());
    }

    // Update is called once per frame
    void Update()
    {
        getNearestSnack();
        float distance = (targetSnack - transform.position).sqrMagnitude;
        if(distance < 0.3){
            myRigidBody.velocity = Vector2.zero;
        }
        //Running anim
        bool playerMoving = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("is_running", playerMoving);
        FlipSprite();
    }
    
    void FlipSprite(){
        bool playerMoving = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerMoving)
        {
            float xScale = Mathf.Sign(myRigidBody.velocity.x) * 4f;
            transform.localScale = new Vector2(xScale, 4f);
            
        }
    }
    
    void getNearestSnack(){
        float distanceToClosestSnack = Mathf.Infinity;

        // Debug: SHOW LIST
        //Debug.Log(string.Join(", ", SnackList.snackList));

        foreach(Vector3 currentSnackPos in SnackList.snackList)
        {   
            //Debug.Log(currentSnackPos.ToString());
            float distanceToSnack = (transform.position - currentSnackPos).sqrMagnitude;
            if (distanceToSnack < distanceToClosestSnack)
            {
                distanceToClosestSnack = distanceToSnack;
                targetSnack = currentSnackPos;
            }
        }
        
        targetSnack = new Vector3(targetSnack.x, transform.position.y, transform.position.z);
        var dir = (targetSnack - transform.position).normalized * runSpeed;
        myRigidBody.velocity = dir;
        //Debug.Log(dir);
        
        float distance = (targetSnack - transform.position).sqrMagnitude;
        if(distance < 0.2){
            myRigidBody.velocity = Vector2.zero;
        }
        
        
        //Running anim
        bool playerMoving = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("is_running", playerMoving);
        
    }

    IEnumerator eatSnack(){
        while(true){
            yield return new WaitForSeconds(snackDelay);
            getNearestSnack();
        }
    }

}
