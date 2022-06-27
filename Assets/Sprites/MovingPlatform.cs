using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer),typeof(BoxCollider2D),typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour
{
    public bool BackTrackRoute = true;
    public float Speed = 3f;
    private bool isBackTracking = false;
    private Rigidbody2D rb;
    private LineRenderer lr;
    private List<Vector3> positions = new List<Vector3>();
    private int currentNode = 0;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Awake()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        var pos = new Vector3[lr.positionCount];
        lr.GetPositions(pos);
        positions = pos.ToList();
        if (positions.Any(x => x.z != 0))
        {
            throw new InvalidOperationException("Moving platform should not have values on z axis");
        }

        lr.enabled = false;
        if(positions.Count < 2)
        {
            throw new InvalidOperationException("Moving platform needs at least 2 positions");
        }
        transform.position = startPos + positions[0];
        rb.position = startPos + positions[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var dir = (startPos + positions[currentNode]) - transform.position ;
        if(Mathf.Abs(dir.x) <= 0.3 && Mathf.Abs(dir.y) <= 0.3)
        {
            if(currentNode + 1 < positions.Count && !isBackTracking)
            {
                currentNode++;
            }
            else
            {
                if (BackTrackRoute && currentNode > 0)
                {
                    isBackTracking = true;
                    currentNode--;
                }
                else
                {
                    currentNode = 0;
                    isBackTracking = false;
                }
                
            }
          
        }
        else
        {
            rb.MovePosition(transform.position + dir.normalized * Speed * Time.fixedDeltaTime);
        }
        
    }
}
