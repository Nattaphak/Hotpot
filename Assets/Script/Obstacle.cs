using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int rotationSpeed;

    private float timeToChange = 0;
    private float currentRotation;
    private bool ChangeRotate = true;

    void Update()
    {
        currentRotation = (rotationSpeed * GameManager.Instance.gameSpeed) / 10;
        timeToChange += Time.deltaTime;
        RotateObstacle();
        
        if(timeToChange >= 20f)
        {
            if(ChangeRotate)
            {
                ChangeRotate = false;
                timeToChange = 0;
                RotateObstacle();
            }
            else
            {
                ChangeRotate = true;
                timeToChange = 0;
                RotateObstacle();
            }
        }
    }

    public void RotateObstacle()
    {
        if(ChangeRotate)
        {
            transform.Rotate(0, 0, currentRotation * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0, 0, -currentRotation * Time.deltaTime);
        }
    }
}
