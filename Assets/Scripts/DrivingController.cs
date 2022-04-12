using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingController : MonoBehaviour
{

    PolygonCollider2D myBodyCollider;
    CapsuleCollider2D myHeadCollider;
    public Rigidbody2D Ftire;
    public Rigidbody2D Btire;
    public float speed;
    private float boostTimer;
    private bool boosting;
    [SerializeField] AudioClip boostPickupSFX;
    [SerializeField] AudioClip crashSoundSFX;
    public float movement;
    public Rigidbody2D Vehicle;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] float levelLoadDelay = 2f;
    

    public bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        myBodyCollider = GetComponentInChildren<PolygonCollider2D>();
        myHeadCollider = GetComponent<CapsuleCollider2D>();

        speed = 75;
        boostTimer = 0;
        boosting = false;
    }



    // Update is called once per frame
    void Update()
    {

        
        if (!isAlive) { return; }
        movement = Input.GetAxis("Horizontal");
        // PlayerControls();
        StartCoroutine(Die());

        if(boosting)
        {
            boostTimer += Time.deltaTime;
            if(boostTimer >= 3)
            {
                speed = 75;
                boostTimer = 0;
                boosting = false;
            }
        }

        

        
        
    }

    public void FixedUpdate()
    {
        Ftire.AddTorque( -movement * speed * Time.fixedDeltaTime);
        Btire.AddTorque( -movement * speed * Time.fixedDeltaTime);
        Vehicle.AddTorque( -movement * speed * Time.fixedDeltaTime);
    }

    

    // void PlayerControls()
    // {
        
    //     float x = Input.GetAxis("Vertical");

    //     if (x > 0)
    //     {
    //         backMotor.motorSpeed = SpeedForward;
    //         frontMotor.motorSpeed = SpeedForward;

    //         backMotor.maxMotorTorque = Torque;
    //         frontMotor.maxMotorTorque = Torque;

    //         wheelFront.motor = frontMotor;
    //         wheelBack.motor = backMotor;
    //     }

    //     else if (x < 0)
    //     {
    //         backMotor.motorSpeed = SpeedBackward;
    //         frontMotor.motorSpeed = SpeedBackward;

    //         backMotor.maxMotorTorque = Torque;
    //         frontMotor.maxMotorTorque = Torque;

    //         wheelFront.motor = frontMotor;
    //         wheelBack.motor = backMotor;
    //     }

    //     else
    //     {
    //         backMotor.motorSpeed = 0;
    //         frontMotor.motorSpeed = 0;

    //         wheelFront.motor = frontMotor;
    //         wheelBack.motor = backMotor;
    //     }

        
    // }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "SpeedBoost")
        {
            boosting = true;
            speed = 150;
            AudioSource.PlayClipAtPoint(boostPickupSFX, Camera.main.transform.position);
            Destroy(other.gameObject);
        }

        if (other.tag == "SuperSpeedBoost")
        {
            boosting = true;
            speed = 200;
            AudioSource.PlayClipAtPoint(boostPickupSFX, Camera.main.transform.position);
            Destroy(other.gameObject);
        }
    }

    

    
    IEnumerator Die()
    {
        

        if (myHeadCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Obstacle")))
        {
            
            AudioSource.PlayClipAtPoint(crashSoundSFX, Camera.main.transform.position);
            crashEffect.Play();
            isAlive = false;
            yield return new WaitForSecondsRealtime(levelLoadDelay);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }

        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Obstacle")))
        {
            
            AudioSource.PlayClipAtPoint(crashSoundSFX, Camera.main.transform.position);
            crashEffect.Play();
            isAlive = false;
            yield return new WaitForSecondsRealtime(levelLoadDelay);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    

    
}
