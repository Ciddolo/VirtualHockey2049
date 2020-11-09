using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float Power;
    public float Speed;
    public float StopTime;
    public float ShotSpeedBoost;
    public bool UseKeyboard;

    public bool IsRotating { get; private set; }
    public bool IsStopping { get; private set; }
    public bool IsShooting { get; private set; }
    public Vector3 ShotDirection { get; private set; }
    public float RotationDirection { get; private set; }

    private GameObject stick;
    private StickBehaviour stickBehaviour;
    private float stoppingTimer;
    private float shotSpeedBoost;

    private void Start()
    {
        stick = transform.GetChild(0).gameObject;
        IsRotating = false;
        ShotDirection = Vector3.zero;
        stickBehaviour = stick.GetComponent<StickBehaviour>();
        stickBehaviour.Power = Power;
        stoppingTimer = StopTime;
    }

    void Update()
    {
        if (UseKeyboard)
            InputKeyboardForDebug();
        else
        {
            if (Input.GetJoystickNames().Length < 2)
                InputKeyboard();
            else
                InputPad();
        }

        Stopping();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Disc")
        {
            GameObject disc = other.gameObject;
            Rigidbody discRigidBody = disc.GetComponent<Rigidbody>();
            DiscBehaviour discBehaviour = disc.GetComponent<DiscBehaviour>();
            Vector3 newVelocity = Vector3.Reflect(discRigidBody.velocity, -other.contacts[0].normal);
            if (newVelocity.magnitude < 2.5f)
                newVelocity *= 2.0f;
            discBehaviour.SetVelocity(newVelocity);
        }
    }

    private void InputKeyboardForDebug()
    {
        // stop
        if (Input.GetKey(KeyCode.LeftShift))
            IsStopping = true;
        else
            IsStopping = false;

        //rotation
        if (Input.GetKey(KeyCode.D)) //clockwise
            Rotate(shotSpeedBoost, 1.0f);
        else if (Input.GetKey(KeyCode.A)) //counterclockwise
            Rotate(shotSpeedBoost, -1.0f);
        else
            IsRotating = false;

        //shot
        if (Input.GetKey(KeyCode.Space))
            Shot(1.0f, -1.0f);
        else
        {
            IsShooting = false;
            shotSpeedBoost = 1.0f;
        }
    }

    private void InputKeyboard()
    {
        // stop
        if (gameObject.tag == "DefenderBlue" || gameObject.tag == "AttackerBlue")
            IsStopping = Input.GetKey(KeyCode.X);
        else if (gameObject.tag == "DefenderRed" || gameObject.tag == "AttackerRed")
            IsStopping = Input.GetKey(KeyCode.M);

        //rotation
        float currentTriggerValue = 0.5f;
        float currentDirection = 1.0f;
        if (gameObject.tag == "DefenderBlue" || gameObject.tag == "AttackerBlue")
        {
            if (Input.GetKey(KeyCode.D)) //clockwise
            {
                currentTriggerValue = ShotSpeedBoost;
                currentDirection = 1.0f;

                Rotate(currentTriggerValue, currentDirection);
            }
            else if (Input.GetKey(KeyCode.A)) //counterclockwise
            {
                currentTriggerValue = ShotSpeedBoost;
                currentDirection = -1.0f;

                Rotate(currentTriggerValue, currentDirection);
            }
            else
                IsRotating = false;
        }
        else if (gameObject.tag == "DefenderRed" || gameObject.tag == "AttackerRed")
        {
            if (Input.GetKey(KeyCode.L)) //clockwise
            {
                currentTriggerValue = ShotSpeedBoost;
                currentDirection = 1.0f;

                Rotate(currentTriggerValue, currentDirection);
            }
            else if (Input.GetKey(KeyCode.J)) //counterclockwise
            {
                currentTriggerValue = ShotSpeedBoost;
                currentDirection = -1.0f;

                Rotate(currentTriggerValue, currentDirection);
            }
            else
                IsRotating = false;
        }

        //shot
        if (gameObject.tag == "DefenderBlue" || gameObject.tag == "AttackerBlue")
        {
            if (IsStopping)
                return;

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
                Shot(currentTriggerValue, currentDirection);
            else
            {
                IsShooting = false;
                shotSpeedBoost = 1.0f;
            }
        }
        else if (gameObject.tag == "DefenderRed" || gameObject.tag == "AttackerRed")
        {
            if (IsStopping)
                return;

            if (Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.J))
                Shot(currentTriggerValue, currentDirection);
            else
            {
                IsShooting = false;
                shotSpeedBoost = 1.0f;
            }
        }
    }

    private void InputPad()
    {
        // stop
        if (gameObject.tag == "DefenderBlue" || gameObject.tag == "AttackerBlue")
            IsStopping = Input.GetButton("A1");
        else if (gameObject.tag == "DefenderRed" || gameObject.tag == "AttackerRed")
            IsStopping = Input.GetButton("A2");

        //rotation
        float currentTriggerValue = 0.5f;
        float currentDirection = 1.0f;
        if (gameObject.tag == "DefenderBlue" || gameObject.tag == "AttackerBlue")
        {
            if (Input.GetAxis("RT1") >= 0.1f) //clockwise
            {
                if (Input.GetAxis("RT1") >= 0.5f)
                    currentTriggerValue = Input.GetAxis("RT1");

                currentDirection = 1.0f;

                if (Input.GetAxis("RT1") >= 0.9f)
                    currentTriggerValue = ShotSpeedBoost;

                Rotate(currentTriggerValue, currentDirection);
            }
            else if (Input.GetAxis("LT1") >= 0.1f) //counterclockwise
            {
                if (Input.GetAxis("LT1") >= 0.5f)
                    currentTriggerValue = Input.GetAxis("LT1");

                currentDirection = -1.0f;

                if (Input.GetAxis("LT1") >= 0.9f)
                    currentTriggerValue = ShotSpeedBoost;

                Rotate(currentTriggerValue, currentDirection);
            }
            else
                IsRotating = false;
        }
        else if (gameObject.tag == "DefenderRed" || gameObject.tag == "AttackerRed")
        {
            if (Input.GetAxis("RT2") >= 0.1f) //clockwise
            {
                if (Input.GetAxis("RT2") >= 0.5f)
                    currentTriggerValue = Input.GetAxis("RT2");

                currentDirection = 1.0f;

                if (Input.GetAxis("RT2") >= 0.9f)
                    currentTriggerValue = ShotSpeedBoost;

                Rotate(currentTriggerValue, currentDirection);
            }
            else if (Input.GetAxis("LT2") >= 0.1f) //counterclockwise
            {
                if (Input.GetAxis("LT2") >= 0.5f)
                    currentTriggerValue = Input.GetAxis("LT2");

                currentDirection = -1.0f;

                if (Input.GetAxis("LT2") >= 0.9f)
                    currentTriggerValue = ShotSpeedBoost;

                Rotate(currentTriggerValue, currentDirection);
            }
            else
                IsRotating = false;
        }

        //shot
        if (gameObject.tag == "DefenderBlue" || gameObject.tag == "AttackerBlue")
        {
            if (IsStopping)
                return;

            if (Input.GetAxis("RT1") >= 0.9f || Input.GetAxis("LT1") >= 0.9f)
                Shot(currentTriggerValue, currentDirection);
            else
            {
                IsShooting = false;
                shotSpeedBoost = 1.0f;
            }
        }
        else if (gameObject.tag == "DefenderRed" || gameObject.tag == "AttackerRed")
        {
            if (IsStopping)
                return;

            if (Input.GetAxis("RT2") >= 0.9f || Input.GetAxis("LT2") >= 0.9f)
                Shot(currentTriggerValue, currentDirection);
            else
            {
                IsShooting = false;
                shotSpeedBoost = 1.0f;
            }
        }
    }

    private void Stopping()
    {
        if (IsStopping && stickBehaviour.Disc != null)
        {
            stoppingTimer -= Time.deltaTime;

            if (stoppingTimer <= 0.0f)
            {
                IsStopping = false;
                stoppingTimer = StopTime;
            }
        }
    }

    private void Rotate(float rotationMul, float direction)
    {
        IsRotating = true;
        RotationDirection = direction;
        transform.Rotate(Vector3.up, Speed * rotationMul * RotationDirection * Time.deltaTime);
    }

    private void Shot(float powerMul, float direction)
    {
        IsShooting = true;
        stickBehaviour.Power = Power * powerMul;
        ShotDirection = stick.transform.right * RotationDirection;
        shotSpeedBoost = ShotSpeedBoost;
        stoppingTimer = StopTime;
    }
}
