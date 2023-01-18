using System.Collections.Generic;
using UnityEngine;

public enum Swipe
{
    None,
    Up,
    Down,
    Left,
    Right
};

public class StepMove : MonoBehaviour
{
    private const int CUBELAYER = 7;

    [SerializeField] float upBound, downBound, leftBound, rightBound;
    [SerializeField] int x;
    [SerializeField] int y;

    private readonly float MinSwipeLength = 15f;

    [SerializeField] Transform startPosition;
    [SerializeField] Transform[] cubes;
    private int[,] grid; //grid with cube IDs

    // NEW CONTROLLER
    private Camera mainCamera;
    private Dictionary<int, GameObject> touchId_GO_Map;
    private Dictionary<int, Touch> touchId_Touch_Map;

    private static Swipe swipeDirection;
    private Touch curTouch;
    private Vector2 swipeVector;
    private Ray ray;
    private RaycastHit hit;
    //

    void Start()
    {
        mainCamera = Camera.main;
        grid = new int[x, y];
        int temp = 0;
        for (int indexY = 0; indexY < y; indexY++)
        {
            for (int indexX = 0; indexX < x; indexX++)
            {
                if (temp == cubes.Length) break;
                grid[indexX, indexY] = 1;
                cubes[temp].position = startPosition.position + indexX * new Vector3(1, 0, 0) + indexY * new Vector3(0, 1, 0);
                cubes[temp].GetComponent<Lose>().posX = indexX;
                cubes[temp].GetComponent<Lose>().posY = indexY;
                temp++;
            }
            if (temp == cubes.Length) break;
        }

        touchId_GO_Map = new Dictionary<int, GameObject>();
        touchId_Touch_Map = new Dictionary<int, Touch>();
    }

    private void Update()
    {
        if (Input.touches.Length > 0)
        {
            for (int i = 0; i < Input.touches.Length; i++)
            {
                curTouch = Input.touches[i];
                if(curTouch.phase == TouchPhase.Began)
                {
                    ray = mainCamera.ScreenPointToRay(curTouch.position);
                    if(Physics.Raycast(ray, out hit, 20f) && hit.collider.gameObject.layer == CUBELAYER)
                    {

                        touchId_GO_Map[curTouch.fingerId] = hit.collider.gameObject;
                        touchId_Touch_Map[curTouch.fingerId] = curTouch;
                    }
                }
                else if (curTouch.phase == TouchPhase.Moved)
                {
                    if (touchId_GO_Map.ContainsKey(curTouch.fingerId))
                    {
                        DetectSwipeNew();
                    }
                }
                else if(curTouch.phase == TouchPhase.Ended)
                {
                    if (touchId_GO_Map.ContainsKey(curTouch.fingerId)) touchId_GO_Map.Remove(curTouch.fingerId);
                    if (touchId_Touch_Map.ContainsKey(curTouch.fingerId)) touchId_Touch_Map.Remove(curTouch.fingerId);
                }
            }
        }
    }

    private void DetectSwipeNew()
    {
        swipeVector = touchId_Touch_Map[curTouch.fingerId].position - curTouch.position;
        if(swipeVector.magnitude >= MinSwipeLength)
        {
            swipeVector.Normalize();

            if (swipeVector.y > 0 && swipeVector.x > -0.5f && swipeVector.x < 0.5f)       swipeDirection = Swipe.Down;
            else if (swipeVector.y < 0 && swipeVector.x > -0.5f && swipeVector.x < 0.5f)  swipeDirection = Swipe.Up;
            else if (swipeVector.x < 0)                                                   swipeDirection = Swipe.Right;
            else if (swipeVector.x > 0)                                                   swipeDirection = Swipe.Left;

            SetDirection(swipeDirection, touchId_GO_Map[curTouch.fingerId]);
            touchId_GO_Map.Remove(curTouch.fingerId);
            touchId_Touch_Map.Remove(curTouch.fingerId);
        }
    }

    public void SetDirection(Swipe swipeDir, GameObject objecte)
    {
        Vector3 objPosition = objecte.transform.position;
        Vector3 newPosition = new Vector3();
        int posX = objecte.GetComponent<Lose>().posX;
        int posY = objecte.GetComponent<Lose>().posY;

        if (swipeDir == Swipe.Up && objPosition.y < upBound)
        {
            if (grid[posX, posY + 1] == 0)
            {
                newPosition = new Vector3(objPosition.x, objPosition.y + 1, objPosition.z);
                grid[posX, posY + 1] = 1;
                grid[posX, posY] = 0;
                objecte.GetComponent<Lose>().posY += 1;
                MoveObjectV2(objecte, newPosition);
            }
        }
        else if (swipeDir == Swipe.Down && objPosition.y > downBound)
        {
            if (grid[posX, posY - 1] == 0)
            {
                newPosition = new Vector3(objPosition.x, objPosition.y - 1, objPosition.z);
                grid[posX, posY - 1] = 1;
                grid[posX, posY] = 0;
                objecte.GetComponent<Lose>().posY -= 1;
                MoveObjectV2(objecte, newPosition);
            }
        }
        else if (swipeDir == Swipe.Left && objPosition.x > leftBound)
        {
            if (grid[posX - 1, posY] == 0)
            {
                newPosition = new Vector3(objPosition.x - 1, objPosition.y, objPosition.z);
                grid[posX - 1, posY] = 1;
                grid[posX, posY] = 0;
                objecte.GetComponent<Lose>().posX -= 1;
                MoveObjectV2(objecte, newPosition);
            }
        }
        else if (swipeDir == Swipe.Right && objPosition.x < rightBound)
        {
            if (grid[posX + 1, posY] == 0)
            {
                newPosition = new Vector3(objPosition.x + 1, objPosition.y, objPosition.z);
                grid[posX + 1, posY] = 1;
                grid[posX, posY] = 0;
                objecte.GetComponent<Lose>().posX += 1;
                MoveObjectV2(objecte, newPosition);
            }
        }
    }

    public void MoveObjectV2(GameObject cube, Vector3 newPosition)
    {
        LeanTween.move(cube, newPosition, 0.01f).setEaseInSine();
        LeanTween.scale(cube, Vector3.one * 0.9f, 0.05f).setOnComplete(() => Tween(cube)); //-------------------------------   0.95f
    }

    public void Tween(GameObject cube)
    {
        LeanTween.scale(cube, Vector3.one /** 0.8f*/, 0.05f);  //-------------------------------
    }
}