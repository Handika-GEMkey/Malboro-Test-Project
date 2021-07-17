using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomeCollider : MonoBehaviour 
{
    public Vector3 direction;
    public float MaxDistance = 10;
    public LayerMask LayerMask;

    public Animator CarAnimator;
    public AudioHandler AudioHandler;

    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(this.transform.position, direction * MaxDistance, Color.green);
        if (Physics.Raycast(this.transform.position, direction, out hit, MaxDistance, LayerMask))
        {
            if (hit.transform.CompareTag("Objective"))
            {
                hit.transform.gameObject.SetActive(false);
                ManagerRacing.Instance.TotalScore += 100;
                AudioHandler.PlaySFXCollecting();
            }
            else if (hit.transform.CompareTag("Obstacle"))
            {
                hit.transform.gameObject.SetActive(false);
                CarAnimator.Play("Damaged", 0, 0);
                ManagerRacing.Instance.TotalLife -= 1;
                AudioHandler.PlaySFXCollision();
            }
        }
    }

    /*private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Objective"))
        {
            col.gameObject.SetActive(false);
            ManagerRacing.Instance.TotalScore += 100;
        }
        else if (col.CompareTag("Obstacle"))
        {
            col.gameObject.SetActive(false);
            CarAnimator.Play("Damaged", 0, 0);
            ManagerRacing.Instance.TotalLife -= 1;
        }
    }*/


}
