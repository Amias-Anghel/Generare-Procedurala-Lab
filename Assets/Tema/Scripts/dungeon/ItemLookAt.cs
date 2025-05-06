using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLookAt : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 800f;

    private bool playerInRange;
    private Transform player;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            playerInRange = true;
            player = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            playerInRange = false;
        }
    }

    void Update()
    {
        if (!playerInRange || player == null) 
            return;

        Vector3 dir = -(player.position - transform.position).normalized;
        Quaternion targetRot = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime);
    }
}
