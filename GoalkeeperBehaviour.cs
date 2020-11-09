using UnityEngine;

public class GoalkeeperBehaviour : MonoBehaviour
{
    public Transform Body;
    public float MovementSpeed, Edge, Sensibility, Angle, ReboundPower, StopTime;
    public bool UseKeyboard;

    private Rigidbody rigidBody;
    private Vector3 newVelocity;
    private Quaternion defaultRotation;
    private Quaternion rightRotation;
    private Quaternion leftRotation;
    private bool isStopping;
    private float stoppingTimer;
    private GameObject disc;
    private GameObject stage;

    private void Start()
    {
        stage = GameObject.Find("Stage");
        rigidBody = GetComponent<Rigidbody>();
        stoppingTimer = StopTime;

        defaultRotation = transform.rotation;
        transform.Rotate(Vector3.up, Angle);
        rightRotation = transform.rotation;
        transform.rotation = defaultRotation;
        transform.Rotate(Vector3.up, -Angle);
        leftRotation = transform.rotation;
        transform.rotation = defaultRotation;
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

        CheckPosition();

        if (disc != null && !isStopping)
            Shot();

        if (disc != null && isStopping)
            Control();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Disc")
        {
            disc = other.gameObject;

            if (isStopping)
                disc.GetComponent<DiscBehaviour>().SetVelocity(Vector3.zero);
            else
                Shot();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Disc")
        {
            if (other.gameObject.GetComponent<DiscBehaviour>().GetVelocity().magnitude <= 1.0f)
                Shot();

            disc = null;
        }
    }

    private void CheckPosition()
    {
        if (transform.position.x > Edge || transform.position.x < -Edge)
        {
            if (transform.position.x < 0.0f)
                transform.position = new Vector3(-Edge, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(Edge, transform.position.y, transform.position.z);
        }
    }

    private void InputKeyboardForDebug()
    {
        if (Input.GetKey(KeyCode.W))
            transform.position = new Vector3(transform.position.x + MovementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        else if (Input.GetKey(KeyCode.S))
            transform.position = new Vector3(transform.position.x - MovementSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.D))
            transform.rotation = rightRotation;
        else if (Input.GetKeyDown(KeyCode.A))
            transform.rotation = leftRotation;

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            transform.rotation = defaultRotation;

        isStopping = Input.GetKey(KeyCode.LeftShift);
    }

    private void InputKeyboard()
    {
        if (gameObject.tag == "GoalkeeperBlue")
        {
            //movement
            if (Input.GetKey(KeyCode.W))
                transform.position = new Vector3(transform.position.x + MovementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            else if (Input.GetKey(KeyCode.S))
                transform.position = new Vector3(transform.position.x - MovementSpeed * Time.deltaTime, transform.position.y, transform.position.z);

            //rotation
            if (Input.GetKey(KeyCode.D))
                transform.rotation = rightRotation;
            else if (Input.GetKey(KeyCode.A))
                transform.rotation = leftRotation;
            else
                transform.rotation = defaultRotation;

            //stopping
            isStopping = Input.GetKey(KeyCode.X);
        }
        else if (gameObject.tag == "GoalkeeperRed")
        {
            //movement
            if (Input.GetKey(KeyCode.I))
                transform.position = new Vector3(transform.position.x + MovementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            else if (Input.GetKey(KeyCode.K))
                transform.position = new Vector3(transform.position.x - MovementSpeed * Time.deltaTime, transform.position.y, transform.position.z);

            //rotation
            if (Input.GetKey(KeyCode.L))
                transform.rotation = rightRotation;
            else if (Input.GetKey(KeyCode.J))
                transform.rotation = leftRotation;
            else
                transform.rotation = defaultRotation;

            //stopping
            isStopping = Input.GetKey(KeyCode.M);
        }
    }

    private void InputPad()
    {
        if (gameObject.tag == "GoalkeeperBlue")
        {
            //movement
            if (Input.GetAxis("LeftVertical1") < -Sensibility)
                transform.position = new Vector3(transform.position.x + MovementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            else if (Input.GetAxis("LeftVertical1") > Sensibility)
                transform.position = new Vector3(transform.position.x - MovementSpeed * Time.deltaTime, transform.position.y, transform.position.z);

            //rotation
            if (Input.GetAxis("RT1") >= 0.1f)
                transform.rotation = rightRotation;
            else if (Input.GetAxis("LT1") >= 0.1f)
                transform.rotation = leftRotation;
            if (Input.GetAxis("RT1") < Sensibility && Input.GetAxis("LT1") < Sensibility)
                transform.rotation = defaultRotation;

            //stopping
            isStopping = Input.GetButton("A1");
        }
        else if (gameObject.tag == "GoalkeeperRed")
        {
            //movement
            if (Input.GetAxis("LeftVertical2") < -Sensibility)
                transform.position = new Vector3(transform.position.x + MovementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            else if (Input.GetAxis("LeftVertical2") > Sensibility)
                transform.position = new Vector3(transform.position.x - MovementSpeed * Time.deltaTime, transform.position.y, transform.position.z);

            //rotation
            if (Input.GetAxis("RT2") >= 0.1f)
                transform.rotation = rightRotation;
            else if (Input.GetAxis("LT2") >= 0.1f)
                transform.rotation = leftRotation;
            if (Input.GetAxis("RT2") < Sensibility && Input.GetAxis("LT2") < Sensibility)
                transform.rotation = defaultRotation;

            //stopping
            isStopping = Input.GetButton("A2");
        }
    }

    private void Control()
    {
        stoppingTimer -= Time.deltaTime;

        if (disc != null)
        {
            disc.transform.SetParent(transform);
        }

        if (stoppingTimer <= 0.0f)
            Shot();
    }

    private void Shot()
    {
        if (disc == null)
            return;

        DiscBehaviour discBehaviour = disc.gameObject.GetComponent<DiscBehaviour>();

        //rebound in coming direction with same power
        //discBehaviour.SetVelocity(-discBehaviour.GetVelocity());

        //rebound always forward with new power
        discBehaviour.SetVelocity(Body.forward * ReboundPower);

        isStopping = false;
        stoppingTimer = StopTime;
        disc.transform.SetParent(stage.transform);
        disc = null;
    }
}
