using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public BoxCollider doorCollider;
    public Camera cam;
    public bool hasEnteredLongHall;

    private void Start()
    {
        cam = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            PublicVar.keyCollected = true;
        }

        if (other.CompareTag("FovShifter")) {
            StartCoroutine(ChangeFOV());
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
    IEnumerator ChangeFOV()
    {
        float t = 0;
        while (t < 1)
        {
            cam.fieldOfView = Mathf.Lerp(60, 110, t);
            t += Time.deltaTime * .1f;
             yield return null;

        }
        hasEnteredLongHall = true;
    }
}
