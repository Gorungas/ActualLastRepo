using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform PlayerPos;
    public float speed;
    public float followThresh;

    void Update()
    {
       if (Vector3.Distance(this.transform.position, PlayerPos.position) <= followThresh)
       {
            transform.position = Vector3.MoveTowards(this.transform.position, PlayerPos.position, speed * Time.deltaTime);
            transform.LookAt(PlayerPos.position);
        }
    }
}
