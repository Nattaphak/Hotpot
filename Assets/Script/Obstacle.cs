using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int rotationSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentRotation = (rotationSpeed * GameManager.Instance.gameSpeed);
        transform.Rotate(0, 0, currentRotation * Time.deltaTime);
    }
}
