using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Animator anim;
    public Rigidbody rb;
    float X, Z;
    public float jumpforce;
    bool jump=false;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        anim.SetFloat("Speed",(Mathf.Abs(X)+Mathf.Abs(Z)));

        if (Input.GetKeyDown(KeyCode.Space) && jump==true)
        {
            rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            jump=false;
        }

    }
    private void FixedUpdate()
    {
        X = Input.GetAxis("Horizontal");
        Z = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(X, 0, Z).normalized; //normalized 1 ve -1 arasýnda döndürür sadece

        if (movement == Vector3.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(movement);

        targetRotation = Quaternion.RotateTowards(transform.rotation,targetRotation,360 * Time.fixedDeltaTime);

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        rb.MoveRotation(targetRotation);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boat"))
        {
            jump=true;
        }
    }

}
