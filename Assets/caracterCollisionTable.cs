using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class caracterCollisionTable : MonoBehaviour
{
    public float speed = 5f;
    public float checkDistance = 0.6f; // How far ahead to check for obstacles
    public LayerMask obstacleLayers;   // Assign "Table" layer or leave as Default

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(moveX, 0, moveZ).normalized;

        if (moveDir.magnitude > 0.1f)
        {
            // Check with Raycast
            if (!Physics.Raycast(transform.position, moveDir, checkDistance, obstacleLayers))
            {
                Vector3 newPos = rb.position + moveDir * speed * Time.fixedDeltaTime;
                rb.MovePosition(newPos);
            }
            else
            {
                // Optional: Debug line to see ray
                Debug.DrawRay(transform.position, moveDir * checkDistance, Color.red);
            }
        }
    }
}