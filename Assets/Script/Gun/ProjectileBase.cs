using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 2f;
    public int damageAmount = 1;
    public float speed = 50f;

    public List<string> tagsToHit;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Colidiu com: {collision.transform.name}");
        Debug.Log($"Tag do objeto: {collision.transform.tag}");
        foreach (var t in tagsToHit)
        {
            if(collision.transform.tag == t)
            {
                Debug.Log($"Tag encontrada: {t}");
                var damageable = collision.transform.GetComponent<IDamageable>();
                Debug.Log($"Damageable: {damageable}");
                if (damageable != null)
                {
                    Vector3 dir = collision.transform.position - transform.position;
                    dir = -dir.normalized;
                    dir.y = 0;
                    damageable.Damage(damageAmount, dir);
                    Destroy(gameObject);
                }
                break;
            }
        }
        
    }
}