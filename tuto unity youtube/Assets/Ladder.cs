
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private bool isInRange;

    private PlayerMovement playerMovment;

    public BoxCollider2D collider;
    // Start is called before the first frame update
    void Awake()
    {
        playerMovment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerMovment.isClimbing = true;
            collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            playerMovment.isClimbing = false;
            collider.isTrigger = false;
        }
    }
}
