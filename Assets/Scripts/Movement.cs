using System;
using System.Collections;
using Pathfinding;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Movement : MonoBehaviour
{
    private IAstarAI _ai;
    private GridGraph _gridGraph;
    private float _delta;
    private bool _isAtLake;
    
    private const float DelayInSeconds = 2.0f;
    
    void Start()
    {
        _ai = GetComponent<IAstarAI>();
        _gridGraph = AstarPath.active.data.gridGraph;
        _delta = 0.0f;
    }
    
    void Update()
    {
        if (!_ai.pathPending && (_ai.reachedEndOfPath || !_ai.hasPath))
        {
            if (_delta > DelayInSeconds)
            {
                _delta = 0.0f;
                _ai.destination = PickRandomTile();
                _ai.SearchPath();
            }
            else
            {
                _delta += Time.deltaTime;
            }
        }
    }

    Vector3 PickRandomTile()
    {
        var walkable = false;
        GridNodeBase randomTile = null;
        while (!walkable)
        {
            randomTile = _gridGraph.nodes[Random.Range(0, _gridGraph.nodes.Length)];
            walkable = randomTile.Walkable;
        }
        Debug.Log("Picked a new tile");
        return (Vector3) randomTile.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.parent.CompareTag("Lake"))
        {
            if (!_isAtLake)
            {
                _isAtLake = true;
                _ai.isStopped = true;
                _ai.SetPath(null);
                _ai.destination = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
                _ai.isStopped = false;
                Debug.Log("Stopped at a lake");   
            }
        }
        else
        {
            _isAtLake = false;
        }
    }
    
    
}
