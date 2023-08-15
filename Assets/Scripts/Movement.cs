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
    private const float DelayInSeconds = 2.0f;
    
    void Start()
    {
        _ai = GetComponent<IAstarAI>();
        _gridGraph = AstarPath.active.data.gridGraph;
        _delta = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all
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
        int index;
        GridNodeBase randomTile = null;
        while (!walkable)
        {
            index = Random.Range(0, _gridGraph.nodes.Length);
            randomTile = _gridGraph.nodes[index];
            walkable = randomTile.Walkable;
        }

        return (Vector3)randomTile.position;
    }
    
}
