using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using Vuforia;

public class FightManager : MonoBehaviour
{
    [SerializeField] GameObject Cactus1;
    [SerializeField] GameObject Cactus2;
    [SerializeField] ImageTargetBehaviour imageTarget;
    [SerializeField] float distantaX;
    [SerializeField] float viteza = 0.01f;
    [SerializeField] bool detected;

    private float attackTimer1 = 0f;
    private float attackTimer2 = 0f;
    private bool inFight = false;

    private Cactus cactus1;
    private Cactus cactus2;
    private float attackInterval1;
    private float attackInterval2;

    void Start()
    {
        cactus1 = Cactus1.GetComponent<Cactus>();
        cactus2 = Cactus2.GetComponent<Cactus>();

        attackInterval1 = Mathf.Clamp(2f / cactus1.GetSpeed(), 0.4f, 2f);
        attackInterval2 = Mathf.Clamp(2f / cactus2.GetSpeed(), 0.4f, 2f);

        distantaX = Mathf.Abs(Cactus1.transform.position.x - Cactus2.transform.position.x);
        detected = false;
    }

    void Update()
    {
        if (imageTarget.TargetStatus.Status == Status.TRACKED)
        {
            detected = true;

            if (cactus1 != null && cactus2 != null)
            {
                cactus1.SetWalking(true);
                cactus2.SetWalking(true);
            }
        }
        else
        {
            detected = false;

            if (cactus1 != null) cactus1.SetWalking(false);
            if (cactus2 != null) cactus2.SetWalking(false);
            inFight = false;
        }

        if (cactus1 != null && cactus2 != null)
            distantaX = Mathf.Abs(Cactus1.transform.position.x - Cactus2.transform.position.x);

        if (detected && cactus1 != null && cactus2 != null)
        {
            if (distantaX > 0.05f)
            {
                Cactus1.transform.position = Vector3.MoveTowards(
                   Cactus1.transform.position,
                   Cactus2.transform.position,
                   viteza * Time.deltaTime
               );

                Cactus2.transform.position = Vector3.MoveTowards(
                    Cactus2.transform.position,
                    Cactus1.transform.position,
                    viteza * Time.deltaTime
                );
            }
            else
            {
                cactus1.SetWalking(false);
                cactus2.SetWalking(false);

                if (!inFight)
                {
                    inFight = true;
                    attackTimer1 = 0f;
                    attackTimer2 = 0f;
                    Debug.Log("Lupta a început!");
                }

                if (inFight)
                {
                    attackTimer1 += Time.deltaTime;
                    attackTimer2 += Time.deltaTime;

                    if (attackTimer1 >= attackInterval1 && cactus1 != null && cactus2 != null)
                    {
                        cactus1.PlayAttack();
                        cactus2.TakeDamageAnim();
                        cactus2.TakeDamage(cactus1.GetDamage());
                        attackTimer1 = 0f;
                        Debug.Log("Cactus1 atacă!");

                        if (cactus2.GetHealth() <= 0)
                        {
                            cactus2.DieAnim();
                            cactus2.Die();
                            cactus1.SetWalking(false);
                            cactus1.GetComponent<Animator>().ResetTrigger("Attack");
                            cactus1.GetComponent<Animator>().SetBool("Present", false);
                            cactus2 = null;
                            inFight = false;
                            Debug.Log("Cactus2 a murit!");
                        }
                    }
                    if (attackTimer2 >= attackInterval2 && cactus1 != null && cactus2 != null)
                    {
                        cactus2.PlayAttack();
                        cactus1.TakeDamageAnim();
                        cactus1.TakeDamage(cactus2.GetDamage());
                        attackTimer2 = 0f;
                        Debug.Log("Cactus2 atacă!");

                        if (cactus1.GetHealth() <= 0)
                        {
                            cactus1.DieAnim();
                            cactus1.Die();
                            cactus2.SetWalking(false);
                            cactus2.GetComponent<Animator>().ResetTrigger("Attack");
                            cactus2.GetComponent<Animator>().SetBool("Present", false);
                            cactus1 = null;
                            inFight = false;
                            Debug.Log("Cactus1 a murit!");
                        }
                    }
                }
            }
        }

    }


}
