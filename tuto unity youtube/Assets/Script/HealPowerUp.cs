using UnityEngine;

public class HealPowerUp : MonoBehaviour
{

    public int heal;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && (PlayerHealth.instance.currentHealth < PlayerHealth.instance.maxHealth))
        {
            PlayerHealth.instance.HealPlayer(heal);
            Destroy(gameObject);

        }
    }
}
