using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    [SerializeField]
     TPCharacterController characterController;

    [SerializeField]
    bool canAttack = false;

    [SerializeField]
    GameObject gameOverScreen;

    [SerializeField]
    Button resetButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("AttackPlayer", .5f, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        canAttack = true;
    }


    private void OnTriggerExit(Collider other)
    {
        canAttack = false;
    }

    void AttackPlayer() 
    {
        if (canAttack) 
        {
            //play attack anim
            characterController.playerHealth--;
            if(characterController.playerHealth <= 0) 
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
}
