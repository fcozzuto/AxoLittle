using UnityEngine;

public class SpecialCivilian : MonoBehaviour
{
    public string Team;
    public int currentHp;
    public int MaxHp;

    private SpriteRenderer spriteRenderer;
    private SpecialCivilianAI specialcivilianAI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHp = MaxHp;
        specialcivilianAI = GetComponent<SpecialCivilianAI>();
    }

    public void Attack(int damage)
    {
        currentHp -= damage;
    }
}
