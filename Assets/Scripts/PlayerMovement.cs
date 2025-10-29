using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip eatSound;
    [SerializeField] private AudioClip ratSound;
    
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    private Vector3 respawnPoint;
    public GameObject fallDetector;
    public ScoreCounter scoreCounter;
    public HealthBar healthBar;
    public GameManager gameManager;
    
    void Start() {
        //store the players starting position for respawining (ended up not getting used)
        respawnPoint = transform.position; 
        //Find a GameObject named ScoreCounter in the Heirarchy, is slow so try to do it only once per script (in start)
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        //searches for a scorecounter script component on the scoreGO gameObject and assigns it to scoreCounter
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
    }

    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //flip player when moving left-right
        if(horizontalInput > 0.01f){
            transform.localScale = new Vector3(9, 8, 1);
        }
        else if(horizontalInput < -0.01f){
            transform.localScale = new Vector3(-9, 8, 1);
        }

        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //Wall jump logic
        if (wallJumpCooldown > 0.2f)
        {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 5;
            }
            //jump when using up arrow 
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Jump();

                //only play jump sound once when youre holding down the jump key 
                if(Input.GetKeyDown(KeyCode.UpArrow) && isGrounded())
                {
                    SoundManager.instance.PlaySound(jumpSound);
                }
            } 
        }
        else 
        {
            wallJumpCooldown += Time.deltaTime;
        }

        //move fall detector with the player
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);

    }

    //jumping logic 
    private void Jump()
    {
        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }

            wallJumpCooldown = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if you collide with the falldetector, die and load game over scene 
        if(collision.tag == "FallDetector") 
        {
            healthBar.isDead = true;
            gameObject.SetActive(false);
            Health.totalHealth = 0f;
            gameManager.gameOver();
            //transform.position = respawnPoint;
        }

        //when collecting a sardine tin, increase score by 50 
        else if(collision.tag == "Sardine") {
            SoundManager.instance.PlaySound(eatSound);
            scoreCounter.score +=50;
            collision.gameObject.SetActive(false); //deactivate it once points have already been collected 
        }

        if(collision.gameObject.CompareTag("Rat")) 
        {
            //SoundManager.instance.PlaySound(ratSound);
            scoreCounter.score -= 30;
            healthBar.Damage(0.1f);
        }
        else if(collision.gameObject.CompareTag("RatHead")) 
        {
            SoundManager.instance.PlaySound(ratSound);
            scoreCounter.score +=100;
            Destroy(collision.gameObject);  //deactivate it once points have already been collected 

        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}