using System;
using Input;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryDrawer : MonoBehaviour
{
    private Transform _startPos;
    private LineRenderer _lineRenderer;
    private IInputHandler _inputHandler;

    public void Init(Transform starPos, IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        _startPos = starPos;
        _lineRenderer = GetComponent<LineRenderer>();
        _inputHandler.OnPointMoved += DrawTrajectory;
    }

    private void OnDestroy()
    {
        _inputHandler.OnPointMoved -= DrawTrajectory;
    }

    private void DrawTrajectory(Vector2 target)
    {
        _lineRenderer.positionCount = 2; // Only two points needed for a straight line
        _lineRenderer.SetPosition(0, _startPos.position); // Start position
        _lineRenderer.SetPosition(1, target); // Target position
    }
}