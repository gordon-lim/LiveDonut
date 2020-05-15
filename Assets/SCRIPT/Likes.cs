using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Likes : MonoBehaviour
{
    Text text;
    private int likeCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>(); 
    }

    void updateLikeCount(){
        text.text="" + likeCount;
    }
}
