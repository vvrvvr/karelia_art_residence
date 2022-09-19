using UnityEngine;


public class Dot : MonoBehaviour
{
    [SerializeField] Display2Manager _display2Manager;
    [SerializeField] private LayerMask wallLayer;
    public bool hasControl = true;
    public int speed = 1;
    public float factor = 1.1f;

    private int moveValue = 0;
    private float halfDotDimention = 0.5f;

    void Update()
    {
        if (hasControl)
        {
            var horVal = _display2Manager.horizontalMoveValue;
            //var vertVal = _display2Manager.verticalMoveValue;

            if (horVal > 0)
            {
                Debug.Log("right" + horVal);
                MoveRightLeft(Vector3.right, horVal);
            }
            if (horVal < 0)
            {
                Debug.Log("left" + horVal);
                MoveRightLeft(Vector3.left, horVal);
            }

            //if (vertVal > 0)
            //    MoveUpDown(Vector3.up, vertVal);
            //if (vertVal < 0)
            //    MoveUpDown(Vector3.down, vertVal);
        }
    }

    private void MoveRightLeft(Vector3 dir, int moveVal)
    {
        Vector3 newPos = transform.position + dir * Time.deltaTime * moveVal * factor;
        float dirLength = Mathf.Abs(newPos.x - transform.position.x);
        Vector3 upRayPoint = new Vector3(transform.position.x, transform.position.y + halfDotDimention, transform.position.z);
        Vector3 downRayPoint = new Vector3(transform.position.x, transform.position.y - halfDotDimention, transform.position.z);
        RaycastHit hit;

        if (Physics.Raycast(upRayPoint, dir, out hit, dirLength, wallLayer))
        {
            var point = new Vector3(0f, 0f, 0f);
            switch (dir)
            {
                case Vector3 v when v.Equals(Vector3.right):
                    point = hit.point - new Vector3(halfDotDimention, 0f, 0f);
                    point = new Vector3(point.x, transform.position.y, point.z);
                    transform.position = point;
                    break;
                case Vector3 v when v.Equals(Vector3.left):
                    point = hit.point + new Vector3(halfDotDimention, 0f, 0f);
                    point = new Vector3(point.x, transform.position.y, point.z);
                    transform.position = point;
                    break;
            }
        }
        else if (Physics.Raycast(downRayPoint, dir, out hit, dirLength, wallLayer))
        {
            var point = new Vector3(0f, 0f, 0f);
            switch (dir)
            {
                case Vector3 v when v.Equals(Vector3.right):
                    point = hit.point - new Vector3(halfDotDimention, 0f, 0f);
                    point = new Vector3(point.x, transform.position.y, point.z);
                    transform.position = point;
                    break;
                case Vector3 v when v.Equals(Vector3.left):
                    point = hit.point + new Vector3(halfDotDimention, 0f, 0f);
                    point = new Vector3(point.x, transform.position.y, point.z);
                    transform.position = point;
                    break;
            }
        }
        else
        {
            switch (dir)
            {
                case Vector3 v when v.Equals(Vector3.right):
                    transform.position += dir * Time.deltaTime * moveValue * factor;
                    break;
                case Vector3 v when v.Equals(Vector3.left):
                    transform.position -= dir * Time.deltaTime * moveValue * factor;
                    break;
            }
        }

    }

    private void MoveUpDown(Vector3 dir, int moveVal)
    {
        Vector3 newPos = transform.position + dir * Time.deltaTime * moveVal * factor;
        float dirLength = Mathf.Abs(newPos.y - transform.position.y);
        Vector3 leftRayPoint = new Vector3(transform.position.x - halfDotDimention, transform.position.y, transform.position.z);
        Vector3 rightRayPoint = new Vector3(transform.position.x + halfDotDimention, transform.position.y, transform.position.z);
        RaycastHit hit;

        if (Physics.Raycast(leftRayPoint, dir, out hit, dirLength, wallLayer))
        {
            var point = new Vector3(0f, 0f, 0f);
            switch (dir)
            {
                case Vector3 v when v.Equals(Vector3.up):
                    point = hit.point - new Vector3(halfDotDimention, 0f, 0f);
                    point = new Vector3(point.x, transform.position.y, point.z);
                    transform.position = point;
                    break;
                case Vector3 v when v.Equals(Vector3.down):
                    point = hit.point + new Vector3(halfDotDimention, 0f, 0f);
                    point = new Vector3(point.x, transform.position.y, point.z);
                    transform.position = point;
                    break;
            }
        }
        else if (Physics.Raycast(rightRayPoint, dir, out hit, dirLength, wallLayer))
        {
            var point = new Vector3(0f, 0f, 0f);
            switch (dir)
            {
                case Vector3 v when v.Equals(Vector3.up):
                    point = hit.point - new Vector3(0f, halfDotDimention, 0f);
                    point = new Vector3(transform.position.x, point.y, point.z);
                    transform.position = point;
                    break;
                case Vector3 v when v.Equals(Vector3.down):
                    point = hit.point + new Vector3(halfDotDimention, 0f, 0f);
                    point = new Vector3(transform.position.x, point.y, point.z);
                    transform.position = point;
                    break;
            }
        }
        else
        {
            switch (dir)
            {
                case Vector3 v when v.Equals(Vector3.up):
                    transform.position += dir * Time.deltaTime * moveValue * factor;
                    break;
                case Vector3 v when v.Equals(Vector3.down):
                    transform.position -= dir * Time.deltaTime * moveValue * factor;
                    break;
            }
        }

    }

  


}
