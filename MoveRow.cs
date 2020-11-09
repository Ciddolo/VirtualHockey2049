using UnityEngine;

public class MoveRow : MonoBehaviour
{
    public float Edge, Speed, Sensibility;
    public bool UseKeyboard;

    private void Update()
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
    }

    private void InputKeyboardForDebug()
    {
        if (Input.GetKey(KeyCode.W))
            transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y, transform.position.z);
        else if (Input.GetKey(KeyCode.S))
            transform.position = new Vector3(transform.position.x - Speed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    private void InputKeyboard()
    {
        if (gameObject.tag == "RowBlue")
        {
            if (Input.GetKey(KeyCode.W))
                transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y, transform.position.z);
            else if (Input.GetKey(KeyCode.S))
                transform.position = new Vector3(transform.position.x - Speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else if (gameObject.tag == "RowRed")
        {
            if (Input.GetKey(KeyCode.I))
                transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y, transform.position.z);
            else if (Input.GetKey(KeyCode.K))
                transform.position = new Vector3(transform.position.x - Speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }

    private void InputPad()
    {
        if (gameObject.tag == "RowBlue")
        {
            if (Input.GetAxis("LeftVertical1") < -Sensibility)
                transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y, transform.position.z);
            else if (Input.GetAxis("LeftVertical1") > Sensibility)
                transform.position = new Vector3(transform.position.x - Speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else if (gameObject.tag == "RowRed")
        {
            if (Input.GetAxis("LeftVertical2") < -Sensibility)
                transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y, transform.position.z);
            else if (Input.GetAxis("LeftVertical2") > Sensibility)
                transform.position = new Vector3(transform.position.x - Speed * Time.deltaTime, transform.position.y, transform.position.z);
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
}
