using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float speed;
    public float timer;
    public bool keepMoving;
    public bool movementInitiated;
    public int n;
    public bool onTheRoll;
    public bool InResetState;
    public string direction;
    public Rigidbody2D rb;
    public GameController gameController;
    public string other_tag;
    public string self_tag;
    public GameObject self;
    public Vector2 destination;
    public int turn;

    private void Start()
    {
        destination = transform.position;
        timer = 0f;
        direction = "down";
        keepMoving = false;
        InResetState = false;
        rb = GetComponent<Rigidbody2D>();
        //speed = 0.6337f;
        speed = 0.80f;
        destination = Vector2.zero;
        turn = 0;
        //self_tag = self.tag;
        DeterminePlayerType();


    }
    void DeterminePlayerType(){
        if ((self.tag == "BlackBall1") || (self.tag == "BlackBall2")){
            self_tag = "blackball";
        }else{
            self_tag = "whiteball";
        }
    }
    private void FixedUpdate()
    {
        
        if (keepMoving == true)
        {
            switch (direction)
            {
                case "upleft":
                    rb.velocity = Vector2.zero;
                    //rb.AddForce (Vector2.up + Vector2.left * speed);
                    rb.AddForce((Vector2.left + Vector2.up) * speed);
                    break;
                case "downleft":
                    rb.velocity = Vector2.zero;
                    rb.AddForce((Vector2.down + Vector2.left) * speed);
                    break;
                case "upright":
                    rb.velocity = Vector2.zero;
                    rb.AddForce((Vector2.right + Vector2.up) * speed);
                    break;
                case "downright":
                    rb.velocity = Vector2.zero;
                    rb.AddForce((Vector2.down + Vector2.right) * speed);
                    break;
                case "right":
                    rb.velocity = Vector2.zero;
                    rb.AddForce(Vector2.right * speed);
                    break;
                case "down":
                    rb.velocity = Vector2.zero;
                    rb.AddForce(Vector2.down * speed);
                    break;
                case "left":
                    rb.velocity = Vector2.zero;
                    rb.AddForce(Vector2.left * speed);
                    break;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    public float maxCollisionDistance;

    //if not properly alighned may eventually make circle collider (the ball's collider ONLY)
    void OnTriggerEnter2D(Collider2D other)
    {
        other_tag = other.tag;
        //      if (Vector2.Distance(other.gameObject.transform.position, transform.position) > maxCollisionDistance){
        //          
        //          return;
        //          
        //      }
        if (turn >= 30)
        {
            //gameController.DisplayNotification_CompletedExercise ();
            gameController.DisplayNotification_TooManySteps();
            keepMoving = false;
        }

        //print("White Ball 1 Colliding with: " + other.tag);
        turn += 1;
        //print(turn);

        bool contains = false;
        foreach (string tag in gameController.blackList)
        {
            //print ("===========");
            //print (other.tag);
            //print (tag);
            //print (tag.Equals (other.tag));
            if (tag.Equals(other.tag))
            {
                contains = true;
            }
        }
        if (!contains)
        {
            transform.position = other.gameObject.transform.position;
            //print("snapping to center : " + other.tag);
        }

        SwitchDirection(other_tag);

        print("White ball 1: " + other_tag);


    }

    void SwitchDirection(string other_tag)
    {
        if (other_tag == "BlackCircleTool"){
            if (self_tag == "blackball"){
                direction = "down";
            }
        }
        else if (other_tag == "WhiteCircleTool"){
            if (self_tag == "whiteball"){
                direction = "down";
            }
        }
        else if (other_tag == "WhiteTriangleTool"){
            if (self_tag == "blackball"){
                direction = "upright";
            }
            else{
                direction = "downleft";
            }
        }
        else if (other_tag == "BlackTriangleTool"){
            if (self_tag == "blackball"){
                direction = "downright";
            }
            else{
                direction = "upleft";
            }
        }
        else if (other_tag == "WhiteSquareTool"){
            if (self_tag == "blackball"){
                direction = "right";
            }
            else{
                direction = "left";
            }
        }
        else if (other_tag == "BlackSquareTool")
        {
            if (self_tag == "blackball")
            {
                direction = "left";
            }
            else
            {
                direction = "right";
            }
        }
        else if (other_tag == "XTile"){
            keepMoving = false;
            gameController.DisplayNotification_FailedAttempt();
        }
        else if (other_tag == "CheckTile"){
            keepMoving = false;
            gameController.CheckWinCondition();
        }

    }

}
