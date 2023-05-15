using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Debug.Log("GameOver");

            GameObject player= other.gameObject;
            player.GetComponent<Rigidbody>().useGravity = false;

            StartCoroutine(LoadingPanelController.Instance.LoadScene("Over"));
        }
    }



}
