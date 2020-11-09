using UnityEngine;

public class StickBehaviour : MonoBehaviour
{
    public PlayerBehaviour Player;
    public Transform[] Positions;

    public float Power { get; set; }
    public Transform Disc { get; private set; }

    private GameObject stage;
    private float deflectionMul;

    private void Start()
    {
        stage = GameObject.Find("Stage");
        deflectionMul = 4.0f;
    }

    private void Update()
    {
        if (!Player.IsStopping && Disc != null)
        {
            Disc.SetParent(stage.transform);
            Disc = null;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Disc")
        {
            if (Player.IsStopping)
                Control(other);
            else if (Player.IsRotating)
                Shot(other);
        }
    }

    private void Control(Collision other)
    {
        GameObject discGO = other.gameObject;
        discGO.GetComponent<DiscBehaviour>().SetVelocity(Vector3.zero);
        discGO.transform.SetParent(transform);
        Disc = discGO.transform;
    }

    private void Shot(Collision other)
    {
        DiscBehaviour disc = other.gameObject.GetComponent<DiscBehaviour>();
        Transform currentPosition = null;
        float currentDistance = float.MaxValue;

        for (int i = 0; i < Positions.Length; i++)
        {
            float newDistance = (other.contacts[0].point - Positions[i].position).magnitude;

            if (newDistance < currentDistance)
            {
                currentDistance = newDistance;
                currentPosition = Positions[i];
            }
        }

        if (Player.IsShooting)
        {
            //----------new direction is the normal of the collision point
            //disc.SetVelocity(-other.contacts[0].normal * Power);
            //----------


            //----------new direction is scripted to eight points with cross method
            //if (Vector3.Cross(Player.transform.forward, currentPosition.position - Player.transform.position).y > 0.0f)
            //    disc.SetVelocity(currentPosition.forward * Power);
            //else
            //    disc.SetVelocity(-currentPosition.forward * Power);
            //----------


            //----------new direction is scripted to eight points with rotation method
            if (Player.RotationDirection > 0.0f)
                disc.SetVelocity(currentPosition.forward * Power);
            else
                disc.SetVelocity(-currentPosition.forward * Power);
            //----------
        }
        else
        {
            //----------new direction is the normal of the collision point
            //disc.SetVelocity((currentPosition.position - other.contacts[0].point).normalized * deflectionMul);
            //----------


            //----------new direction is scripted to eight points with cross method
            //if (Vector3.Cross(Player.transform.forward, currentPosition.position - Player.transform.position).y > 0.0f)
            //    disc.SetVelocity(currentPosition.forward * deflectionMul);
            //else
            //    disc.SetVelocity(-currentPosition.forward * deflectionMul);
            //----------


            //----------new direction is scripted to eight points with rotation method
            if (Player.RotationDirection > 0.0f)
                disc.SetVelocity(currentPosition.forward * deflectionMul);
            else
                disc.SetVelocity(-currentPosition.forward * deflectionMul);
            //----------
        }
    }
}
