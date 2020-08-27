using UnityEngine;

public class Bullet : MonoBehaviour
{
    //serialize fields
    [SerializeField] float speed;


    //cache and declarations
    Vector2 prevPos;
    Vector2 currentPos;
    Damage damage;


    //LayerMask wallLayerMask;

    private void Start()    
    {
        //TODO wat is this?
        //wallLayerMask = LayerMask.GetMask("Walls");
        damage = GetComponent<Damage>();
    }
    
    private void Update()
    {
        MoveBulletAndDoRaycastThings();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
        {
            Debug.Log("!");
            var otherHealth =  other.GetComponent<Health>();
            otherHealth.ReduceHealthBy(damage.GetDamage());
            Destroy(gameObject);
        }

    }
    private void MoveBulletAndDoRaycastThings()
    {
        prevPos = new Vector2(transform.position.x, transform.position.y);
        transform.Translate(new Vector2(0, 1) * speed * Time.deltaTime);
        currentPos = new Vector2(transform.position.x, transform.position.y);
        var distance = (prevPos - currentPos).magnitude;

        RaycastHit2D[] hits = Physics2D.RaycastAll(prevPos, transform.up, distance);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];

            if (hit.collider.gameObject.tag == "Wall") { Destroy(gameObject); }

        }

    }

}
