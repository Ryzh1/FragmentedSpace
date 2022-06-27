using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGun : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    private Vector3 _startAngle;
    public Vector2 Angle;
    private void Awake()
    {
        _camera = Camera.main;
        var testAngle = Mathf.Atan2(transform.up.x, transform.up.y) * 180 / Mathf.PI;
        _startAngle.z = testAngle;
    }
    // Update is called once per frame
    void Update()
    {
        var mouseScreenPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseScreenPosition - transform.position);
        var directionNormal = direction.normalized;
        var testAngle = Mathf.Atan2(directionNormal.x, directionNormal.y) * 180 / Mathf.PI;

        var diff = _startAngle.z - testAngle;
        if ( diff > Angle.x && diff < Angle.y)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -testAngle);
        }
    }
}
