using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLookAt : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 800f;
    public bool isEnemy;

    private bool playerInRange;
    private Transform player;
    private PlayerHealth playerHealth;
    
    private float health = 20f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            playerInRange = true;
            player = other.transform;
            playerHealth = player.GetComponent<PlayerHealth>();
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

        if (isEnemy)
            playerHealth.TakeDamage(2f * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E) && isEnemy) {
            TakeDamage();
        }
    }

    private void TakeDamage() {
        health -= playerHealth.damage;
        Debug.Log($"Enemy took damage. Remaining life: {health}");
        if (health <= 0) {
            Debug.Log("Enemy is dead");
            Destroy(gameObject);
        }
    }
}
