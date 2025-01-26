using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
     TPCharacterController characterController;

    [SerializeField]
    bool canAttack = false;
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
                characterController.gameObject.SetActive(false);
            }
        }
    }
}
