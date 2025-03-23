using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpecialCivilianAI : MonoBehaviour
{
    public SpecialCivilian RefToCivilian;
    public GameObject player;

    public SpecialCivilian[] civilians;

    private float distance;
    public float speed = 3f;

    public int damage;
    public int DamageRadius;

    void Start()
    {
        civilians = FindObjectsOfType<SpecialCivilian>();
        RefToCivilian = GetComponent<SpecialCivilian>();
    }

    // Update is called once per frame
    void Update()
    {
        if(RefToCivilian.currentHp < 0)
            this.gameObject.SetActive(false);
        civilians = FindObjectsOfType<SpecialCivilian>();
        // in like the update, when the chars are moving
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(50 * Time.time));
        if (civilians != null)
        {
            Boolean EnemyFound = false;
            foreach (var civilian in civilians)
            {
                float distanceToCivilian = Vector2.Distance(transform.position, civilian.gameObject.transform.position);
                if (civilian.Team != RefToCivilian.Team)
                {
                    EnemyFound = true;
                    FollowEnemy(civilian);
                    Debug.Log("Targetting enemy");
                    break;
                }
            }
        }
    }

    private void FollowEnemy(SpecialCivilian specialcivilian)
    {
        GameObject civilianPos = specialcivilian.gameObject;
        distance = Vector2.Distance(transform.position, civilianPos.transform.position);
        Vector2 direction = civilianPos.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, civilianPos.transform.position, speed * Time.deltaTime);

        if (distance < DamageRadius)
        {
            specialcivilian.Attack(damage);
        }
    }

}

