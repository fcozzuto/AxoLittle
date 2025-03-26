using UnityEngine;

public class SpecialCivilian : MonoBehaviour
{
    public string Team;
    public float currentHp;
    public float MaxHp;

    private SpriteRenderer spriteRenderer;
    private SpecialCivilianAI specialcivilianAI;

    public AudioSource audioSource; // Assign this in the Inspector
    public AudioClip hitSound; // The initial background music (before win)
    public AudioClip deathSound1;
    public AudioClip deathSound2;

    public float deathSoundChance = 0.5f;

    public float chanceToHitSound = 0.01f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHp = MaxHp;
        specialcivilianAI = GetComponent<SpecialCivilianAI>();
    }

    private void Update()
    {
        if (currentHp < 0)
        {
            float randomValue = Random.Range(0f, 1f);
            if (randomValue < deathSoundChance)
                AudioSource.PlayClipAtPoint(deathSound1, transform.position);
            else
                AudioSource.PlayClipAtPoint(deathSound2, transform.position);
            this.gameObject.SetActive(false);
        }

    }

    public void Attack(float damage)
    {
        currentHp -= damage;

        float randomValue = Random.Range(0f, 1f);
        if(randomValue < chanceToHitSound)
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
    }
}
