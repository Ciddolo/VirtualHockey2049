using UnityEngine;
using UnityEngine.SceneManagement;

public class DiscBehaviour : MonoBehaviour
{
    public float InitialSpeed;
    public Transform TableCenter;

    public GameObject ParticleWall;
    public GameObject ParticleStick;

    public AudioClip Wall0;
    public AudioClip Wall1;
    public AudioClip Wall2;

    public AudioClip Stick0;
    public AudioClip Stick1;
    public AudioClip Stick2;

    public AudioClip Reset;

    private Rigidbody rigidBody;
    private AudioSource audioSource;

    private float ZeroVelocityTime;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        ZeroVelocityTime = 3.0f;
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        if (SceneManager.GetActiveScene().name != "Play")
            ResetDiscPositionWithRandomVelocity();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
            ResetDiscPositionWithRandomVelocity();

        CheckVelocity();
    }

    public void ResetDiscPositionWithRandomVelocity()
    {
        transform.position = new Vector3(0.0f, 0.1f, 0.0f);
        rigidBody.velocity = new Vector3(Random.Range(-100.0f, 100.0f), 0.0f, Random.Range(100.0f, -100.0f)).normalized * InitialSpeed;
    }

    public void ResetDiscPosition()
    {
        rigidBody.velocity = Vector3.zero;
        transform.position = new Vector3(0.0f, 0.1f, 0.0f);
    }

    public Vector3 GetVelocity()
    {
        return rigidBody.velocity;
    }

    public void SetVelocity(Vector3 newVelocity)
    {
        rigidBody.velocity = newVelocity;
    }

    public void AddForce(Vector3 force)
    {
        rigidBody.AddForce(force);
    }

    public void CheckVelocity()
    {
        Debug.Log(ZeroVelocityTime);
        if (rigidBody.velocity.magnitude <= 0.3f)
        {
            ZeroVelocityTime -= Time.deltaTime;

            if (ZeroVelocityTime <= 0.0f)
            {
                audioSource.PlayOneShot(Reset);
                ResetDiscPosition();
                ZeroVelocityTime = 3.0f;
            }
        }
        else
            ZeroVelocityTime = 3.0f;
    }

    private void PlayWall0()
    {
        audioSource.PlayOneShot(Wall0);
    }

    private void PlayWall1()
    {
        audioSource.PlayOneShot(Wall1);
    }

    private void PlayWall2()
    {
        audioSource.PlayOneShot(Wall2);
    }

    private void PlayStick0()
    {
        audioSource.PlayOneShot(Stick0);
    }

    private void PlayStick1()
    {
        audioSource.PlayOneShot(Stick1);
    }

    private void PlayStick2()
    {
        audioSource.PlayOneShot(Stick2);
    }

    void OnCollisionEnter(Collision other)
    {
        int rng = Random.Range(0, 2);
        if (other.gameObject.tag == "Wall")
        {
            switch (rng)
            {
                case 0:
                    PlayWall0();
                    break;
                case 1:
                    PlayWall1();
                    break;
                case 2:
                    PlayWall2();
                    break;
            }
            ContactPoint contact = other.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(ParticleWall, pos, rot);
        }
        else if (other.gameObject.tag == "Stick")
        {
            switch (rng)
            {
                case 0:
                    PlayStick0();
                    break;
                case 1:
                    PlayStick1();
                    break;
                case 2:
                    PlayStick2();
                    break;
            }
            ContactPoint contact = other.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(ParticleWall, pos, rot);
        }
    }
}