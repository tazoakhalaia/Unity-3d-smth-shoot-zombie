using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    private Animator anim;
    public bool isDead;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {

            if (anim != null)
            {
                anim.SetBool("isDeath", true);
            }
            isDead = true;
            Destroy(gameObject, 2f);
        }
    }
}
