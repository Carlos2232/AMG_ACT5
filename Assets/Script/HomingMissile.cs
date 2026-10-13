using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 9f;
    public float rotationSpeed = 4f;
    public float lifeTime = 5f; 

    private Transform player;
    private float timer = 0f;

    void Start()
    {
        GameObject pObj = GameObject.FindGameObjectWithTag("Player");
        if (pObj != null) player = pObj.transform;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
            return;
        }

        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, player.position) < 1.5f)
            {
                Player jet = player.GetComponent<Player>();
                if (jet != null)
                {
                    jet.TakeHit();
                }
                Destroy(gameObject); 
            }
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}