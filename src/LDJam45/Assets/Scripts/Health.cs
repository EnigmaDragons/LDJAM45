using UnityEngine;

public class Health : MonoBehaviour
{
    public Role Role;
    public int MaxHealth;
    public int CurrentHealth { get; set; }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }
}
