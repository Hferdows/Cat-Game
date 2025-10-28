using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    //[SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private EnemyPatrol enemyPatrol;

    private void Awake() 
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;

        //attack only when player is in sight 
        if(PlayerInSight()) 
        {
            if(cooldownTimer >= attackCooldown)
            {
                //attack
                cooldownTimer = 0;
                anim.SetTrigger("ratAttack");
            }
        }
        
    }

    private bool PlayerInSight() 
    {
        //creates the boxcast or "sight" in front of the enemy -- making sure to flip with the character 
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0, Vector2.left, 0, playerLayer);
            
        //player is in enemy sight when the hit collider is not null
        return hit.collider != null;
    }

    //function to flip enemy along the x axis 
    private void flip() 
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    //draws out and shows us where point A and B lie, and the distance line between them
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
              new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
