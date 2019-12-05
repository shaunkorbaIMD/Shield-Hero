using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public PlayerMovement instance;

    //Move speed, speeds for shielding vs not shielding
    public float moveSpeed;
    private float normMoveSpeed;
    public float shieldMoveSpeed;

    public GameObject cam;

    public Rigidbody theRB;

    //Slow Time Variables
    public float slowDownFactor = 0.9f;
    public float slowDownLength = 3f;


    //Health Variables
    static public float maxHealth = 15f;
    static public float health = maxHealth;
    
    //Shield and cooldown/"mana" variable
    public GameObject shield;
    
    public float shieldDuration; // This depletes while shield is out.
    public float shieldDurationLength = 20f;

    private float shieldWaitTime; // This counts down to zero after shield is put away, after at zero, shieldDuration starts recharging
    public float shieldWaitTimeLength = 0.02f;

    public float coolDown;

    Freezer _freezer;

    PauseMenu pauseMenu;
    
    //Sounds
    public AudioClip playerHit;
    public AudioClip playerDeath;
    public AudioClip footsteps;

    AudioSource audioSource;

    public int frame;
    public int frameWaitLength;

    public Animator animator;

    //Time it takes to play the player death animation
    private float dyingTimer;

    // Animation state variables
    private bool isDead = false;
    private bool shieldActive = false;
    private bool isMoving = false;
    private bool isCasting = false;
    private bool isHit = false;


    


    IEnumerator FrameWait()
    {  
        yield return new WaitUntil(() => frame >= 10);   
    }

    void slowTime()
    {
  
            //Time.timeScale = slowDownFactor;
            Time.timeScale -= (3f / slowDownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, slowDownFactor, 1f);

            Time.fixedDeltaTime = Time.timeScale * 0.02f; 
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //Get freeze manager to check if the frame is frozen
        GameObject mgr = GameObject.FindWithTag("FreezeManager");

        StartCoroutine(FrameWait());
        
        if (mgr)
        {
            _freezer = mgr.GetComponent<Freezer>();
        }    

        instance = this;
        
        theRB = GetComponent<Rigidbody>();

        normMoveSpeed = moveSpeed;

        
        audioSource = GetComponent<AudioSource>();

        //The time you need to wait for the player death animation to play
        dyingTimer = 0.4f; 

        //Deactivate the shield, and set the duration bar to be filled, and the wait time between shield uses to zero
        shield.active = false;
        shieldDurationLength = 10f;
        shieldDuration = shieldDurationLength;
        shieldWaitTimeLength = 3f;
        shieldWaitTime = 0;


        frameWaitLength = 6;
    }

    public float playerHealthToText()
    {
        float hp = health;
        return hp;
    }

    public void PlayOneShot(AudioClip clip, float volumeScale = 1.0F)
    {

    }
    // What to do when hit
    public void Ouch()
    {
        health -= 1;
        cam.GetComponent<CameraFollow>().ShakeTheCamera(1.0f);    
    }
    public void hitSound()
    {
        audioSource.PlayOneShot(playerHit, 0.7f);
        FrameWait();

        //set animation state
        isHit = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Set the Moving state
        
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            isMoving = true;
        } else {
            isMoving = false;
        }

        if(frame == 0)
        {
            isHit = false;
        }


        // Set the animator states

        animator.SetBool("shieldActive", shieldActive);
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isDead", isDead);
        animator.SetBool("isHit", isHit);
        animator.SetBool("isCasting", isCasting);


        Debug.Log("isMoving: " + isMoving + ". shieldActivate: " + shieldActive);


        //If out of health, play death animation, and play deathSound
        if (health <= 0)
        {
            isDead = true;
            audioSource.PlayOneShot(playerDeath, 0.7f);
            
        }
        if (isDead)
        {
            dyingTimer += Time.unscaledDeltaTime;

            theRB.velocity = new Vector3(0, 0, 0);

            health = 0;

            if (dyingTimer > 4.2f)
            {
                SceneManager.LoadScene("MainMenu");
                health = maxHealth;
            }

            return;
        }



        //If the frame is frozen, don't exicute this frame's code.
        if (_freezer._isFrozen == true || PauseMenu.GameIsPaused == true)
        {
            return;
        }


        

        if (frame <= frameWaitLength)
        {
            //Debug.Log("Frame: " + frame);
            frame++;
        }
        else if (frame > frameWaitLength)
        {
            frame = 0;
        }

        theRB.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, theRB.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
        

        



        var offset = cam.GetComponent<CameraFollow>().offset;
        var startOffset = cam.GetComponent<CameraFollow>().startOffset;


        // SLOWING TIME ////////////////////////////////////////////////////////

            

            Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            


            if (Input.GetMouseButton(1)) //right click
            {
                slowTime();

                //set animation state
                isCasting = true;


                cam.GetComponent<CameraFollow>().offset = Vector3.Lerp(offset, startOffset + new Vector3(0, -1, 1), 0.01f);
            }
            else
            {
                //set animation state
                isCasting = false;

                cam.GetComponent<CameraFollow>().offset = Vector3.Lerp(offset, startOffset, 0.04f);
            }
        
        // TAKING OUT SHIELD ////////////////////////////////////////////////////////

        if (Input.GetMouseButton(0) && shieldDuration > 0) // if you are pressing LMB and you have shield "mana" to spend
        {
            shieldDuration -= Time.deltaTime;

            if (shield.active == false) 
            {
                shield.SetActive(true);
                

            }
            //Set anim state
            shieldActive = true;

            shieldDuration -= Time.unscaledDeltaTime;

            if(shieldDuration < 0)
            {
                shieldDuration = 0;
            }
            
            moveSpeed = Mathf.Lerp(moveSpeed, shieldMoveSpeed, 0.07f);

        } else  { // Otherwise


            if (shield.active == true)
            {
                shieldWaitTime = shieldWaitTimeLength;
                shield.SetActive(false);
            }

            //Set anim state
            shieldActive = false;


            if (shieldWaitTime > 0)
            {
                shieldWaitTime -= Time.unscaledDeltaTime * 5f; 
            }



            moveSpeed = Mathf.Lerp(moveSpeed, normMoveSpeed, 0.07f);
        }

        //If the wait is over since last shield activate, start recharging
        if(shieldWaitTime == 0 && shield.active == false)
        {
            shieldDuration += Time.unscaledDeltaTime*3f;

            if(shieldDuration > shieldDurationLength)
            {
                shieldDuration = shieldDurationLength;
            }
        }

        //Floor shieldWaitTime at 0
        if (shieldWaitTime < 0)
        {
            shieldWaitTime = 0;
        }






        


        // Aim at the mouse by casting a ray at the ground, and getting the collision coordinate to point to

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float midPoint = (transform.position - Camera.main.transform.position).magnitude; //dist between this and camera

        Vector3 lookPoint = mouseRay.origin + mouseRay.direction * midPoint;

        lookPoint.y = transform.position.y;

        transform.LookAt(lookPoint);


       


    }

 
}
