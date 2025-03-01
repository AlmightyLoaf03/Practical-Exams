using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class OneWayPlatform : MonoBehaviour
{
    private Collider2D platformCollider;
    private PlayerInput playerInput;
    private InputAction dropAction;
    public float disableTime = 0.3f;

    private void Start()
    {
        platformCollider = GetComponent<Collider2D>();

        // Find the Player GameObject
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("⚠️ Player GameObject not found! Make sure your player has the tag 'Player'.");
            return;
        }

        // Get the PlayerInput component
        playerInput = player.GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("⚠️ PlayerInput component not found! Ensure Player has the PlayerInput component.");
            return;
        }

        // Get the Drop action
        dropAction = playerInput.actions["Drop"];
        if (dropAction == null)
        {
            Debug.LogError("⚠️ 'Drop' action not found! Ensure it's correctly set in your Input Actions.");
        }
    }

    void Update()
    {
        if (dropAction != null && dropAction.WasPressedThisFrame())
        {
            StartCoroutine(DisableCollision());
        }
    }

    private IEnumerator DisableCollision()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) yield break; // Prevent errors if player isn't found

        Collider2D playerCollider = player.GetComponent<Collider2D>();
        if (playerCollider == null) yield break; // Ensure player has a Collider2D

        Physics2D.IgnoreCollision(platformCollider, playerCollider, true);
        yield return new WaitForSeconds(disableTime);
        Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
    }
}
