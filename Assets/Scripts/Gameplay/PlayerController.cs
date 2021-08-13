using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float touchSensitivity;

    [SerializeField] private float mouseCurrentPosition;
    [SerializeField] private float mouseBoundaryRight;
    [SerializeField] private float mouseBoundaryLeft;

    [SerializeField] private Vector3[] movementPositions;
    [SerializeField] private Material[] carGraphics;

    private bool oneTimeMove;
    private bool oneTimeShoot;
    private bool onTheMove;
    private Vector3 targetPos;

    private int indexPosition = 1;

    void Start()
    {
        // movementPositions = movementPositions;
        this.transform.position = movementPositions[indexPosition];
    }

    void Update()
    {
        if (ManagerRacing.Instance.GameStarted)
        {
            if ((Input.GetMouseButton(0)))
            {
                if (!oneTimeMove)
                {
                    mouseCurrentPosition = Input.mousePosition.x;
                    if (!oneTimeShoot)
                    {
                        mouseBoundaryLeft = (mouseCurrentPosition - touchSensitivity);
                        mouseBoundaryRight = (mouseCurrentPosition + touchSensitivity);
                        oneTimeShoot = true;
                    }
                    if (mouseCurrentPosition < mouseBoundaryLeft)
                    {
                        if (this.transform.position != movementPositions[0])
                        {
                            if (indexPosition > 0)
                            {
                                indexPosition -= 1;
                                targetPos = movementPositions[indexPosition];
                                onTheMove = true;
                            }
                        }
                        oneTimeMove = true;
                    }
                    else if (mouseCurrentPosition > mouseBoundaryRight)
                    {
                        if (this.transform.position != movementPositions[2])
                        {
                            if (indexPosition < 2)
                            {
                                indexPosition += 1;
                                targetPos = movementPositions[indexPosition];
                                onTheMove = true;
                            }
                        }
                        oneTimeMove = true;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                mouseCurrentPosition = 0;
                oneTimeShoot = false;
                oneTimeMove = false;
            }


            if ((Input.GetKeyDown(KeyCode.A))|| (Input.GetKeyDown("left")))
            {
                if (indexPosition > 0)
                {
                    indexPosition -= 1;
                    targetPos = movementPositions[indexPosition];
                    onTheMove = true;
                    //Debug.Log("Kiri:" + indexPosition.ToString());
                }
            }

            if ((Input.GetKeyDown(KeyCode.D))|| (Input.GetKeyDown("right")))
            {
                //Debug.Log("Kanan:" + indexPosition.ToString());
                if (indexPosition < 2)
                {
                    indexPosition += 1;
                    targetPos = movementPositions[indexPosition];
                    onTheMove = true;
                    //Debug.Log("Kanan:" + indexPosition.ToString());
                }
            }

           // Debug.Log(targetPos.ToString() + ":" + indexPosition.ToString());
            OnPlayerMove(targetPos, indexPosition);
        }
    }

    private void OnPlayerMove(Vector3 TargetPosition, int indexPos)
    {
        if (onTheMove)
        {
            Vector3 movement = Vector3.MoveTowards(this.transform.position, TargetPosition, (Time.deltaTime * (ManagerRacing.Instance.RacingSpeed * 15)));
            this.transform.position = movement;
            if (Vector3.Distance(this.transform.position, TargetPosition) == 0)
            {
                this.GetComponent<MeshRenderer>().material = carGraphics[indexPos];
                onTheMove = false;
            }
        }
    }
}
