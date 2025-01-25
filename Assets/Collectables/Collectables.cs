using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField]
     TPCharacterController characterController;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player") 
        {
            characterController.colletablCount++;
            gameObject.SetActive(false);
        }    
    }

}
