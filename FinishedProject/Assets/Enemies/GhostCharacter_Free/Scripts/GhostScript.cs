using UnityEngine;

public class GhostScript : MonoBehaviour
{
    private Animator Anim;
    private CharacterController Ctrl;
    private Transform Player;
    private Vector3 MoveDirection;

    public float Speed = 3f;
    public string MoveState = "Move";
    public string AttackState = "Attack";
    public float DamageDistance = 2f;  // The distance at which the ghost starts attacking
    public int HP = 100;
    public int damageAmount = 1;  // Amount of damage dealt by the ghost
    private bool isAttacking = false;  // Flag to prevent continuous damage

    void Start()
    {
        Anim = GetComponent<Animator>();
        Ctrl = GetComponent<CharacterController>();

        // Initially find the player by tag (don't need to manually assign it)
        Player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (Player == null)
        {
            Debug.LogWarning("No player object found with 'Player' tag!");
        }
    }

    void Update()
    {
        if (Player == null) return;  // Exit if player isn't found

        MoveTowardPlayer();
    }

    private void MoveTowardPlayer()
    {
        // If Player is still null after Start, don't do anything
        if (Player == null) return;

        // Calculate direction towards the player
        Vector3 direction = (Player.position - transform.position).normalized;
        direction.y = 0;  // Prevent vertical movement (if you don't want ghosts to fly)

        // If the ghost is far enough from the player, move toward them
        if (Vector3.Distance(transform.position, Player.position) > DamageDistance)
        {
            Anim.CrossFade(MoveState, 0.1f);
            MoveDirection = direction * Speed;
            Ctrl.Move(MoveDirection * Time.deltaTime);
            transform.LookAt(new Vector3(Player.position.x, transform.position.y, Player.position.z));
        }
        else
        {
            Anim.CrossFade(AttackState, 0.1f);  // If close, attack the player
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        // Only attack if the ghost isn't already attacking
        if (isAttacking) return;

        // Check the distance to the player and deal damage if close
        if (Vector3.Distance(transform.position, Player.position) <= DamageDistance)
        {
            isAttacking = true;  // Flag as attacking to prevent repeated damage
            PlayerHealth playerHealth = Player.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);  // Deal damage to player
                Debug.Log("Player damaged! Current Health: " + playerHealth.MaxHealth);
            }
        }

        // Reset the attack flag after a short delay
        Invoke(nameof(ResetAttackFlag), 1f);  // Adjust the cooldown as necessary
    }

    private void ResetAttackFlag()
    {
        isAttacking = false;  // Reset the attack flag so the ghost can attack again
    }
}
