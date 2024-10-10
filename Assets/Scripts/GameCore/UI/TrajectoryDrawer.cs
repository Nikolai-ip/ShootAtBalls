using System;
using GameCore.GameControllers;
using Input;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryDrawer : MonoBehaviour
{
    private Transform _startPos;
    private LineRenderer _lineRenderer;
    private IInputHandler _inputHandler;
    [SerializeField] private ShotController _shotController;

    public void Init(Transform startPos, IInputHandler inputHandler, ShotController shotController)
    {
        _inputHandler = inputHandler;
        _startPos = startPos;
        _shotController = shotController;
        _lineRenderer = GetComponent<LineRenderer>();
        _inputHandler.OnPointMoved += DrawTrajectory;
    }

    private void OnDestroy()
    {
        _inputHandler.OnPointMoved -= DrawTrajectory;
    }

    private void DrawTrajectory(Vector2 target)
    {
        Vector2 startPos = _startPos.position;
        float currentRatio = _shotController.SpeedRatio; 
        float maxRatio = _shotController.MaxSpeedRatio;
        float maxAngle = _shotController.MaxScatterAngle;
        float coef = ((currentRatio - 1) / (maxRatio - 1));
        float currentAngle = maxAngle * coef;
        float distance = Vector2.Distance(startPos, target);
        
        float baseAngle =  Mathf.Repeat(_shotController.transform.localEulerAngles.z + 90,360);
        float rightScatterAngle = (baseAngle - currentAngle) * Mathf.Deg2Rad;
        float leftScatterAngle = (baseAngle + currentAngle) * Mathf.Deg2Rad;

        Vector2 rightScatterPos = new Vector2(startPos.x+Mathf.Cos(rightScatterAngle)*distance,startPos.y+Mathf.Sin(rightScatterAngle)*distance);
        Vector2 leftScatterPos = new Vector2(startPos.x+Mathf.Cos(leftScatterAngle)*distance,startPos.y+Mathf.Sin(leftScatterAngle)*distance);
        
        _lineRenderer.positionCount = 4;
        //Line1
        _lineRenderer.SetPosition(0, startPos); 
        _lineRenderer.SetPosition(1, rightScatterPos); 
        
        //Line2
        _lineRenderer.SetPosition(2, startPos); 
        _lineRenderer.SetPosition(3, leftScatterPos); 
    }
}
