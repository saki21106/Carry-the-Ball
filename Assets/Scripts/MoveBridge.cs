using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBridge : MonoBehaviour
{
    public float speed = 0.03f;
    float right_limit = 6.5f;
    float left_limit = -9.0f;
    bool move_R = true; //‰E‚É“®‚¢‚Ä‚¢‚é‚©
    // Start is called before the first frame update
    void Start()
    {
        if(left_limit > right_limit)
        {
            float temp = left_limit;
            left_limit = right_limit;
            right_limit = temp;
        }   
        if(speed < 0)
        {
            move_R = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if(pos.x <= left_limit && !move_R)
        {
            speed *= -1;
            move_R = true;
        }
        if(pos.x >= right_limit && move_R)
        {
            speed *= -1;
            move_R = false;
        }
        transform.Translate(new Vector3(speed, 0, 0));
    }
}
