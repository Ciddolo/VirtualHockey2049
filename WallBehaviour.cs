using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Disc")
        {
            GameObject disc = other.gameObject;
            Rigidbody discRigidBody = disc.GetComponent<Rigidbody>();
            DiscBehaviour discBehaviour = disc.GetComponent<DiscBehaviour>();
            Vector3 newVelocity = Vector3.Reflect(discRigidBody.velocity, -other.contacts[0].normal);
            if (newVelocity.magnitude < 5.0f)
                newVelocity *= 3.0f;
            discBehaviour.SetVelocity(newVelocity);
        }
    }
}
