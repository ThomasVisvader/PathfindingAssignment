using System.Collections;
using Pathfinding;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Script : MonoBehaviour
{
    private IAstarAI _ai;
    // private GridGraph _grid;
    public Tilemap tilemap;
    private float _delta;
    private const float DelayInSeconds = 2.0f;
    
    void Start()
    {
        _ai = GetComponent<IAstarAI>();
        transform.position = PickRandomTile();
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
        var index = Random.Range(0, tilemap.transform.childCount);
        var randomTile = tilemap.transform.GetChild(index).gameObject;
        return randomTile.transform.position;
    }
}
