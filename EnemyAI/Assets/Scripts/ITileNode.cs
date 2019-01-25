using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITileNode  {

    public virtual bool IsPassable() { return false; }
    public virtual int GetNeighborCount() { return 0; }
    public virtual ITileNode GetNeighbor(int nodeIdx) { return null; }
    public virtual float GetNeigborMovingCost(int neigborIdx) { return 1f; }
    public virtual float GetHeuristic() { return 0f; } //H
    public virtual Vector3 Position { get; set; }

    // Internal parameters are filled in the path finding algorithm
    internal ITileNode ParentNode = null;
    internal float Cost = 0f; //G
    internal float Score = 0f; //F
    internal int Distance = 0;

    internal int openComputeId;
    internal int closeComputeId;
    internal bool isOpen = false;
    internal bool isClose = false;
}
