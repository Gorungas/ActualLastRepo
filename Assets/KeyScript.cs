using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public BoxCollider doorCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            PublicVar.keyCollected = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Door"))
        {
            print("hi");
            if (PublicVar.keyCollected == true)
            {
                doorCollider.enabled = false;

            }
        }
    }
}
