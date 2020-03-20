using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlataform : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    public float fallingDistance = 5f;
    public float destroyTime = 1f;
    public ParticleSystem fallingFX;

    private float fallingDelay = 1f;
    private bool drop;

    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (drop)
        {
            Drop();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Tags.IsPlayer(collision))
        {
            anim.SetTrigger("willFall");
            StartCoroutine(WaitToDropPlataform());
        }
    }

    private IEnumerator WaitToDropPlataform()
    {
        yield return new WaitForSeconds(fallingDelay);
        drop = true;
    }

    private void Drop()
    {
        fallingFX.Play();
        Vector2 fall = new Vector2(transform.position.x, transform.position.y - fallingDistance);
        transform.position = Vector2.MoveTowards(transform.position, fall, speed * Time.deltaTime);
        Destroy(gameObject, destroyTime);
    }
}
