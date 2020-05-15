using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackTrigger : MonoBehaviour
{
    public Vector3 position;
    public Animator snack_animator;
    public Animator pet_animator;
    public GameObject pet;

    void Start(){
        snack_animator = transform.parent.GetComponent<Animator>();
        pet = GameObject.Find("Dog");
        pet_animator = pet.GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision){
        
        if(collision.gameObject.name == "Dog"){
            //Fix Donut
            transform.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            pet.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            snack_animator = transform.parent.GetComponent<Animator>();
            
            
            transform.parent.GetComponent<Snack>().snackPos = position;
            //SnackList.snackList.Remove(position);
            snack_animator.Play("BITE_DONUT");
            pet_animator.Play("EAT_DOG");
        }else if(collision.gameObject.name == "Groundpiece"){
            position = transform.position;
            SnackList.snackList.Add(position);
        }
    }
}
