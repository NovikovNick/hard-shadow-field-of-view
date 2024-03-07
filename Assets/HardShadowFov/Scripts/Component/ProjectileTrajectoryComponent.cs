using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Assertions;

public class ProjectileTrajectoryComponent : MonoBehaviour
{
    [Header("--------- General Settings --------- ")]
    [SerializeField] private LineRenderer trajectoryLine;
    [SerializeField] private GameObject trajectoryTarget;
    [SerializeField] private LayerMask trajectoryLayerMask;
    [SerializeField] private float width = 2f;

    [Header("--------- State --------- ")]
    [SerializeField] private Vector2 aim;

    void Start()
    {
        Assert.IsNotNull(trajectoryLine);
    }

    void LateUpdate()
    {
        var center3d = transform.position;
        var center = new Vector2 (center3d.x, center3d.y);
        aim = new Vector2(transform.right.x, transform.right.y);

        Vector3 point;
        var hit = Physics2D.Raycast(center, aim, width, trajectoryLayerMask);
        point = hit.collider != null                            //
            ? new Vector3(hit.point.x, hit.point.y, 0)          //
            : center3d + new Vector3(aim.x, aim.y, 0) * width;

        trajectoryLine.SetPosition(0, center3d);
        trajectoryLine.SetPosition(1, point);

        if (trajectoryTarget != null)
        {
            trajectoryTarget.transform.position = point;
        }
    }
}
