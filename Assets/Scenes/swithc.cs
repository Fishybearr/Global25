using UnityEngine;

public class swithc : MonoBehaviour
{

    public TPCharacterController tp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") 
        {
            tp.Boss = true;
        }
    }
}
