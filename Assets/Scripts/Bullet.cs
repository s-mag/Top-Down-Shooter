using UnityEngine;

public class Bullet : MonoBehaviour
{
    //serialize fields
    [SerializeField] public int damage = 10;
    [SerializeField] float speed;


    //cache and declarations
    Vector2 prevPos;
    Vector2 currentPos;


    //LayerMask wallLayerMask;

    private void Start()    
    {
        //TODO wat is this?
        //wallLayerMask = LayerMask.GetMask("Walls");
    }
    
    private void Update()
    {
        MoveBulletAndDoRaycastThings();
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
