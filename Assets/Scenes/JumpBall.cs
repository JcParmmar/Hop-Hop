using System;
using UnityEngine;

public class JumpBall : MonoBehaviour
{
    public float movingSpeed = 2f;
    public float jumpHeight = 2f;
    public float gravity = 9.8f;
    public Vector3 groundOffset = new Vector3(0f, 0.2f, 0f);

    [SerializeField] private float checkSphereRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private bool _isGrounded;
    private float _verticalVelocity;
    
    void Update()
    {
        //we will check that ball is on a ground or not??
        //for that we're going to use Physics.CheckSphere which will create physic sphere under the ball and by using that we can check if ground is touching that sphere or not 
        //other than that we are using Conditional Operator for making the code sort
        _isGrounded = Physics.CheckSphere(transform.position + groundOffset, checkSphereRadius, groundLayer);

        if (_isGrounded)
        {
            //if Ball is on ground then We need to Jump Again for that we are using _verticalVelocity parameter
            _verticalVelocity = 0;//we make it 0 so any old movement will be nullified 
            _verticalVelocity = Mathf.Sqrt(2 * jumpHeight * gravity);// for adding Jump We are using Mathf.Sqrt for getting a perfect Jumping Height
        }
        else
        {
            //if ball is not on Ground then we need to add gravity in velocity so ball will fall
            _verticalVelocity -= gravity * Time.deltaTime;
        }

        //now for implement velocity in ball we will create Vector3 Using _verticalVelocity variable with Time.deltaTime so it can be smooth on all Devices
        Vector3 move = new Vector3(0, _verticalVelocity * Time.deltaTime, 0);
        transform.position += move;//now we will add new vector3 in Ball Position
        transform.Translate(Vector3.forward * (movingSpeed * Time.deltaTime)); // this code is responsible for moving forward Ball
    }

    /// <summary>
    /// This Method Is for Debugging purpose it going to draw sphere under ball so we can decide what radius and groundOffset we need for checkSphere Method
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + groundOffset, checkSphereRadius);
    }
}
