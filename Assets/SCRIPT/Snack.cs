using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snack : MonoBehaviour
{
    public Vector3 snackPos;
   void RemoveDonut(){
       Destroy(gameObject);
       SnackList.snackList.Remove(snackPos);
       GameObject.Find("Dog").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
       GameObject.Find("Dog").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    
   }
}
