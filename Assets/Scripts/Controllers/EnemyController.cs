using QFramework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour, IController
{
    [Header("Attribute")]
    [SerializeField] bool isBoss;
    [SerializeField] float speed;
    [SerializeField] int hp;
    private int currentHealth;

    [Header("Preference")]
    [SerializeField] Animator animator;
    [SerializeField] TMP_Text hpText;

    [SerializeField] GameObject rightPos;
    [SerializeField] GameObject leftPos;
    private Rigidbody2D rb;
    private Transform currentPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = rightPos.transform;
        animator.SetBool("isBoss", isBoss);
        currentHealth = hp;
        if(hpText != null) hpText.text = hp.ToString();

        this.RegisterEvent<RestartEvent>(Restart).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    void Restart(RestartEvent e)
    {
        currentHealth = hp;
        if (hpText != null) hpText.text = currentHealth.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ChangePath();
        }
    }

    private void Update()
    {
        if(hpText != null)
        {
            switch (hp)
            {
                case 5:
                    {
                        hpText.color = Color.red; break;
                    }
                case 3:
                    {
                        hpText.color = Color.yellow; break;
                    }
                case 1:
                    {
                        hpText.color = Color.green; break;
                    }
            }
        }
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == rightPos.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            ChangePath();
        }
    }

    void ChangePath()
    {
        if (currentPoint == leftPos.transform)
        {
            currentPoint = rightPos.transform;
        }
        else
        {
            currentPoint = leftPos.transform;
        }
    }

    public void GetKilled()
    {
        currentHealth--;
        if(hpText != null) hpText.text = currentHealth.ToString();
        if (currentHealth <= 0)
        Destroy(gameObject);
    }

    IArchitecture IBelongToArchitecture.GetArchitecture()
    {
        return SadnessKnightAdventure.Interface;
    }
}
