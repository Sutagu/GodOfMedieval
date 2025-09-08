using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public int currentHealth = 100;
    public int maxHealth = 100;

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if(currentHealth <=0) gameObject.SetActive(false);
    }
}
