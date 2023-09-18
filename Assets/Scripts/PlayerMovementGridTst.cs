using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementGridTst : MonoBehaviour
{
    public bool isMoving = false;
    private Vector3 origPos, targetPos;
    float timeToMove = 0.2f;

    public LayerMask whatStops;
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(y) == 1 && !isMoving)
        {
            if (!Physics2D.OverlapCircle(transform.position + new Vector3(0, y, 0) , 0.5f, whatStops))
                StartCoroutine(Move(y * Vector3.up));
        }
        if (Mathf.Abs(x) == 1 && !isMoving)
        {
            if (!Physics2D.OverlapCircle(transform.position + new Vector3(0, y, 0), 0.2f, whatStops))
                StartCoroutine(Move(x * Vector3.right));
        }
    }

    public IEnumerator Move(Vector3 direction)
    {
        isMoving = true;
        float time =0;

        origPos = transform.position;
        targetPos = origPos + direction;

        while(time < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (time/timeToMove));
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }
}
