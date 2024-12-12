using UnityEngine;

public class JumpBall : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 2f; // Total Jump Height
    [SerializeField] private float gravity = 9.8f; // Gravity Fource
    [SerializeField] private float movingSpeed = 2f; // moving Speed For Left Right
    [SerializeField] private Vector3 groundOffset = new Vector3(0f, 0.2f, 0f);// offset for Physics sphere
    [SerializeField] private float checkSphereRadius = 0.2f;// radius for phusics sphere
    [SerializeField] private LayerMask groundLayer; // check Ground Layer
    [SerializeField] private LayerMask wallLayer;// wall Layer
    [SerializeField] private float checkTheJumpAfterSec = 0.5f;// check how long ball is connected to cube

    private bool _isGrounded;// check is grounded
    private float _verticalVelocity; // jump velocity
    private GameManager _gameManager;// game manager ref
    private float _delayTime = 0;// total time pass to connected with cube

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    void Update()
    {
        if(!GameManager.Instance.isGameRunning) return;// if game is not start then do not run anything

        //check if ball is touching wall layer
        if (Physics.CheckSphere(transform.position + groundOffset, checkSphereRadius, wallLayer))
        {
            // if it is then game over
            _gameManager.GameOver();
            return;
        }
        
        //we will check that ball is on a ground or not??
        //for that we're going to use Physics.CheckSphere which will create physic sphere under the ball and by using that we can check if ground is touching that sphere or not 
        //other than that we are using Conditional Operator for making the code sort
        _isGrounded = Physics.CheckSphere(transform.position + groundOffset, checkSphereRadius, groundLayer);

        if (_isGrounded)
        {
            //if Ball is on ground then We need to Jump Again for that we are using _verticalVelocity parameter
            _verticalVelocity = 0;//we make it 0 so any old movement will be nullified 
            _verticalVelocity = Mathf.Sqrt(2 * jumpHeight * gravity);// for adding Jump We are using Mathf.Sqrt for getting a perfect Jumping Height
            
            //here delay is needed because ball is touching cube for multiple Frames so it have issue to calculate jump  
            if (_delayTime > checkTheJumpAfterSec)
            {
                _gameManager.TotalJumps++;
                _delayTime = 0;
            }
            else
            {
                _delayTime += Time.deltaTime;
            }
            
        }
        else
        {
            //if ball is not on Ground then we need to add gravity in velocity so ball will fall
            _verticalVelocity -= gravity * Time.deltaTime;
        }
        
        //add vertical Velocity in movement 
        float velocity = _gameManager.dragManager.dragDir.x * (Time.deltaTime * movingSpeed);

        //now for implement velocity in ball we will create Vector3 Using _verticalVelocity variable with Time.deltaTime so it can be smooth on all Devices
        Vector3 move = new Vector3(velocity, _verticalVelocity * Time.deltaTime, 0);
        transform.position += move;//now we will add new vector3 in Ball Position
        //transform.Translate(Vector3.forward * (movingSpeed * Time.deltaTime)); // this code is responsible for moving forward Ball
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
