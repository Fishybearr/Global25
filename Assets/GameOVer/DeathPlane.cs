using UnityEngine;
using UnityEngine.UI;

public class DeathPlane : MonoBehaviour
{
    [SerializeField]
    TPCharacterController characterController;

    [SerializeField]
    GameObject gameOverScreen;

    [SerializeField]
    Button resetButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player") 
        {
            //Game over
            //activate game over
            gameOverScreen.SetActive(true);

            //change anim state
            characterController.GetComponent<Animator>().SetBool("IsDead", true);
            characterController.enabled = false;
            resetButton.Select();
        }
    }
}
