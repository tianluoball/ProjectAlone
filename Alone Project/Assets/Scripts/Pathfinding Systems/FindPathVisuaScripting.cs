using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPathVisuaScripting : MonoBehaviour
{
    private AStarManager _astarManager;
    private RangeFinder _rangeFinder;

    public OverlayTile start;
    public OverlayTile end;
    public int moveRange;

    public List<OverlayTile> finalPath;

    private void Start()
    {
        _astarManager = new AStarManager();
        _rangeFinder = new RangeFinder();
    }

    public void findPath()
    {
        finalPath = _astarManager.FindPath(start, end);
    }

    public List<OverlayTile> findPathInput(OverlayTile start, OverlayTile end)
    {
        return finalPath = _astarManager.FindPath(start, end);
    }

    public List<OverlayTile> findTrueDistance(OverlayTile selfPos, int range)
    {
        return _rangeFinder.GetTilesInRange(selfPos, range);
    }

    public OverlayTile findPathForAnimal(OverlayTile charaPos, OverlayTile selfPos, List<OverlayTile> inRangeTiles, bool isAttack)
    {
        return _astarManager.TrueNavigator(charaPos, selfPos, inRangeTiles, isAttack);
    }
}
