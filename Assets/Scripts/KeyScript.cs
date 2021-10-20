using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyScript : MonoBehaviour
{
    public BoxCollider doorCollider;
    public BoxCollider door2Collider;
    public BoxCollider door3Collider;
    public GameObject fallingFloor;
    public Rigidbody heldRB;
    public Camera cam;

    public Transform camTrans;
    public Transform held;
    public LayerMask targetLayer;

    public bool hasEnteredLongHall;

    private void Start()
    {
        cam = Camera.main;
        camTrans = Camera.main.transform;

    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (held == null)
            {
                RaycastHit hit;

                if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, 50, targetLayer))
                {
                    held = hit.transform;
                    heldRB = held.gameObject.GetComponent<Rigidbody>();
                    StartCoroutine(GrabTarget());
                }
            }
            else
            {
                held.SetParent(null);
                heldRB.isKinematic = false;
                held = null;
                heldRB.AddForce(camTrans.forward * 200);
            }
        }
    }

    IEnumerator GrabTarget()
    {
        float t = 0;
        Vector3 startpos = held.position;
        heldRB.isKinematic = true;
        while (t<1)
        {
            held.position = Vector3.Lerp(startpos, camTrans.position + camTrans.forward * 2, t);
            t += Time.deltaTime * 2;
            yield return null;
        }

        held.SetParent(camTrans);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            PublicVar.keyCollected = true;
        }
        else if (other.CompareTag("Key2"))
        {
            Destroy(other.gameObject);
            PublicVar.key2Collected = true;
        }
        else if (other.CompareTag("Key3"))
        {
            Destroy(other.gameObject);
            fallingFloor.SetActive(false);
        }
        else  if (other.CompareTag("Key4"))
        {
            Destroy(other.gameObject);
            PublicVar.key4Collected = true;
        }
        else if (other.CompareTag("WinDoor"))
        {
            SceneManager.LoadScene("WinScreen");
        }

        if (other.CompareTag("FovShifter")) {
            StartCoroutine(ChangeFOV());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            print("hi");
            if (PublicVar.keyCollected == true)
            {
                doorCollider.enabled = false;

            }
        }
        if (collision.gameObject.CompareTag("Door2"))
        {
            if (PublicVar.key2Collected == true)
            {
                door2Collider.enabled = false;
            }
        }
        if (collision.gameObject.CompareTag("Putin"))
        {
            SceneManager.LoadScene("GameOver");
        }
        else if (collision.gameObject.CompareTag("Door3"))
        {
            door3Collider.enabled = false;
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
