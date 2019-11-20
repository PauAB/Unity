using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterMove : MonoBehaviour,IEntity
{
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    public float speed = 12f;
    [SerializeField]
    bool flyMode = false;

    public float jumpHeight = 3.0f;

    Vector3 velocity;
    bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
        
    }
    void IEntity.EAwake()
    {
        
    }

    void IEntity.EUpdate(float delta)
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

      
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
        if (!flyMode)
        {
            velocity += Physics.gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
   

    
}
