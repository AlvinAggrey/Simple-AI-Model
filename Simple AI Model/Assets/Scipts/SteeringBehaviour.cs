//using System.Numerics;
using UnityEngine;

public class SteeringBehaviour : MonoBehaviour
{
    [SerializeField] public Rigidbody rb;
    [SerializeField] public float speed;
    [SerializeField] public Transform target;
    [SerializeField] public Transform fleeTarget;
    Transform tf;
    public Vector3 desiredVel;
    public Vector3 steering;

    public Vector3 Arrive(Vector3 target, float strength)
    {
        Vector3 desiredVel = (target - tf.position).normalized * speed;

        Vector3 steering = (desiredVel - rb.velocity);
        Vector3.ClampMagnitude(steering, strength);
        return steering;
    }
    public Vector3 Flee(Vector3 target, float strength)
    {
        if((target - tf.position).magnitude < 30 )
        {
            Vector3 desiredVel = (target - tf.position).normalized * speed * -1;
            Vector3 steering = (desiredVel - rb.velocity);
            Vector3.ClampMagnitude(steering, strength);
            return steering;
        }
        return new Vector3(0, 0, 0);
    }
    public void Steering ()
    {
        Vector3 totalSteer = Flee(fleeTarget.position, speed * 5) + Arrive(target.position, 0.1f);
        rb.AddForce(totalSteer);
    }

    // Start is called before the first frame update
    void Start()
    {
        tf = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Steering();
    }
}
