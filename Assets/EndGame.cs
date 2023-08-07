using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] AudioSource levelComplete;

    // Start is called before the first frame update
    void Start()
    {
        levelComplete.Play(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
