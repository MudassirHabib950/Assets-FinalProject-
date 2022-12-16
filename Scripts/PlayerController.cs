
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
   

    private int desiredLane = 1; // 0:left,1:middle,2:right

    public float laneDistance = 4f; // this is distance between two lanes
    public float forwardSpeed;
    public float jumpForce;
    public float maxSpeed;

    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;
    private bool isSliding = false;

    public float gravity = -20f;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
     controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
      if(!PlayerManager.isGameStarted)
      return;
       // Increasing forward speed as it moves
      if(forwardSpeed < maxSpeed)
            forwardSpeed += 0.1f * Time.deltaTime;
      animator.SetBool("isGameStarted",true);
     direction.z = forwardSpeed; 
     isGrounded = Physics.CheckSphere(groundCheck.position,0.15f,groundLayer);
     animator.SetBool("isGrounded",isGrounded);
    
    
     // getting input on which lane we should be
     if(SwipeManager.swipeRight)
     {
        desiredLane++;
        if(desiredLane == 3) 
          desiredLane = 2;
     }
      if(SwipeManager.swipeLeft)
     {
        desiredLane--;
        if(desiredLane == -1) 
          desiredLane = 0;
     }
      if(controller.isGrounded)
      {
        direction.y = -1;
         if(SwipeManager.swipeUp)
     {
        Jump();
     }
      }
      else
      {
         direction.y += gravity * Time.deltaTime;
      }
      if(SwipeManager.swipeDown && !isSliding)
      {
        StartCoroutine(Slide());
      }
     // Calculate the position where we are in
     Vector3 targetPosition = transform.position.z * transform.forward  + transform.position.y * transform.up;
     if(desiredLane == 0)
     {
        targetPosition += Vector3.left * laneDistance;
     }
     else if(desiredLane == 2)
     {
        targetPosition += Vector3.right * laneDistance;

     }
     transform.position = Vector3.Lerp(transform.position,targetPosition,80 * Time.deltaTime);
      controller.center = controller.center;
   
    }
    private void FixedUpdate()
    {
      if(!PlayerManager.isGameStarted)
      return;
     controller.Move(direction * Time.fixedDeltaTime);

    }
    private void Jump()
    {
    direction.y = jumpForce;
    }
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
      if(hit.transform.tag == "Obstacle")
      {
         PlayerManager.gameOver = true;
      }
    }
     IEnumerator Slide()
{
   isSliding = true;
   animator.SetBool("isSliding",true);
   controller.center = new Vector3(0,-.5f,0);
   controller.height = 1;

   yield return new WaitForSeconds(1.3f);
   controller.center = new Vector3(0,0,0);
   controller.height = 2;
   animator.SetBool("isSliding",false);
   isSliding = false;
}

}

