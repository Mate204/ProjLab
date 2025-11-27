using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerControllerFourthGame : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private bool isGrounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log("Sphere world position at Start: " + transform.position);
    }
    private void Update()
    {
        if (transform.position.y < -10f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        float h = Input.GetAxis("Horizontal"); //A/D
        float v = Input.GetAxis("Vertical"); //W/S

        Vector3 move = transform.right * h + transform.forward * v;
        Vector3 velocity = new Vector3(move.x * moveSpeed, rb.linearVelocity.y, move.z * moveSpeed);
        rb.linearVelocity = velocity;
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}
