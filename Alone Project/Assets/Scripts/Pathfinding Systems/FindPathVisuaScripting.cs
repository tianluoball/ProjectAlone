using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPathVisuaScripting : MonoBehaviour
{
    private AStarManager _astarManager;

    public OverlayTile start;
    public OverlayTile end;
    public int moveRange;

    public List<OverlayTile> finalPath;

    private void Start()
    {
        _astarManager = new AStarManager();
    }

    public void findPath()
    {
        finalPath = _astarManager.FindPath(start, end);
    }
}
