using System;
using System.Collections.Generic;
using FlaxEngine;
using System.Diagnostics;
using System.Threading;

namespace Game
{
    /// <summary>
    /// PathMangeler Script.
    /// </summary>
    public class PathMangeler : Script
    {
        /// <inheritdoc/>
        NodeGraph gameGraph;
        List<Vector2I> path;
        public override void OnStart()
        {
            // Here you can add code that needs to be called when script is created, just before the first game update
            stopwatch.Reset();
            stopwatch.Start();
            gameGraph = new NodeGraph(21, 21);
            //path = gameGraph.getPath(0,0,10,10);
            gameGraph.debugDraw();
            stopwatch.Stop();
            //FlaxEngine.Debug.Log(stopwatch.ElapsedMilliseconds);
            
        }
        
        /// <inheritdoc/>
        public override void OnEnable()
        {
            // Here you can add code that needs to be called when script is enabled (eg. register for events)
            
        }

        /// <inheritdoc/>
        public override void OnDisable()
        {
            // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        }

        /// <inheritdoc/>
        Stopwatch stopwatch = new Stopwatch();
        double counter = 0;
        public override void OnUpdate()
        {
            // Here you can add code that needs to be called every frame
            gameGraph.debugDraw();
            counter+=0.02;
            path = gameGraph.getPath(5,0,-5,0);
            //path = gameGraph.getPath(0,0,-1,5);
            NodeGraph.drawPathRealCoords(path);
            gameGraph.removeNodeRealCoords(0,0);
            gameGraph.removeNodeRealCoords(0,-1);
            var pos = Input.MousePosition;
            var ray = Camera.MainCamera.ConvertMouseToRay(pos);

            if (Physics.RayCast(ray.Position, ray.Direction, out var hit, layerMask: 1U << 3))
            {
                FlaxEngine.Debug.Log(hit.Point);
            }
        }
    }


    public class Vector2I{
        public int x, y;
        public Vector2I(int xx, int yy){
            x = xx; y = yy;
        }
        public Vector2I(float xx, float yy){
            x = (int)xx; y = (int)yy;
        }

        public double Length(){
            return Math.Sqrt(x*x+y*y);
        }

        public String toString(){
            return "("+x+"|"+y+")";
        }

        static public int Dot(Vector2I l, Vector2I r){
            return l.x*r.x+r.y*l.y;
        }

        static public bool EqualVals(Vector2I l, Vector2I r){
            return (l.x == r.x && l.y == r.y);
        }

        static public Vector2I Sub(Vector2I l, Vector2I r){
            return new Vector2I(l.x-r.x, l.y-r.y);
        }

        static public Vector2I Add(Vector2I l, Vector2I r){
            return new Vector2I(l.x+r.x, l.y+r.y);
        }
    }

    public class NodeGraph{
        Node[,] graph;

        public Node rawGetNode(Vector2I pos){
            return graph[pos.x, pos.y];
        }
        public NodeGraph (int w, int h){
            graph = new Node[w,h];
            for (int x = 0; x < w; x++){
                for (int y = 0; y < h; y++){
                    graph[x,y] = new Node();
                }
            }
            this.fullRelink();
        }

        public void refreshEdgesIndices(int x, int y){
            Vector2I pos = new Vector2I(x,y);
            //FlaxEngine.Debug.Log(pos.toString());
            Node changing = graph[x,y];
            if(changing == null) return;
            
            
            for(int i = 0; i < 8; i++){
                changing.edges[i] = hasNeighbour(pos,i);
            }
        }

        public void refreshNeighIndices(int x, int y){
            for(int i = x-1; i < x+2; i++){
                for(int j = y-1; j < y+2; j++){
                    refreshEdgesIndices(i,j);
                }
            }
        }

        public void removeNodeRealCoords(int x, int y){
            Vector2I indi = RealCordsToIndicies(x,y);
            graph[indi.x, indi.y] = null;
            refreshNeighIndices(indi.x, indi.y);
        }

        public bool hasNeighbour(Vector2I pos, int dir){
            Vector2I n = Vector2I.Add(pos, Node.dirLookup(dir));
            
            return (rawGetNode(n) != null);
        }
        public void fullRelink(){
            for (int x = 1; x < graph.GetLength(0)-1; x++){
                for (int y = 1; y < graph.GetLength(1)-1; y++){
                    if(graph[x,y] == null) continue;
                    Node changing = graph[x,y];
                    Vector2I pos = new Vector2I(x,y);
                    for(int i = 0; i < 8; i++){
                        changing.edges[i] = hasNeighbour(pos,i);
                    }
                }
            }

            
        }

        static public void drawPathRealCoords(List<Vector2I> path){
            Vector2I start = path[0];
            for(int i = 1; i < path.Count; i++){
                DebugDraw.DrawLine(new Vector3(start.x*100,0.1, start.y*100), new Vector3(path[i].x*100,0.1, path[i].y*100), Color.Red,0, false);
                start = path[i];
            }
        }
        
        public Vector2I RealCordsToIndicies(int x, int y){
            return new Vector2I(x+graph.GetLength(0)/2, y+graph.GetLength(1)/2);
        }
        public Node GetNodeRealCoords(int x, int y){
            Vector2I indi = RealCordsToIndicies(x,y);
            return graph[indi.x, indi.y];
        }
        public List<Vector2I> getPath(int sx, int sy, int dx, int dy){
            Vector2I currentPos = new Vector2I(sx, sy);
            Vector2I dest = new Vector2I(dx, dy);

            String path = currentPos.toString();
            List<Vector2I> result = new List<Vector2I>();
            result.Add(currentPos);
            while(!Vector2I.EqualVals(currentPos, dest)){
                Vector2I dir = Vector2I.Sub(dest,currentPos);
                double angle = Math.Atan2(dir.y, dir.x);
                
                double slicedDirRaw = (((angle+Math.PI)/(Math.PI*1.9999))*7);
                int slicedDir = (int) slicedDirRaw;
                //FlaxEngine.Debug.Log(angle+"  "+slicedDir + " " + currentPos.toString());
                Node standingHere = this.GetNodeRealCoords(currentPos.x, currentPos.y);
                int spinCounter = 0;
                if(standingHere != null) {
                        while(!standingHere.edges[slicedDir]){
                        //Wenn das nächst beste auch nicht geht dreh dich einfach bis was geht
                        double dirDiff = spinCounter == 0 ? slicedDirRaw - slicedDir : spinCounter;
                        slicedDir+= dirDiff > 0 ? 1 : -1;
                        if(slicedDir == -1) slicedDir = 7;
                        else if(slicedDir == 8) slicedDir = 0;
                        spinCounter--;
                        //FlaxEngine.Debug.Log(spinCounter);
                    }
                }
                currentPos = Vector2I.Add(currentPos, Node.dirLookup(slicedDir));
                result.Add(currentPos);
                path+="->"+currentPos.toString();
            }
            //FlaxEngine.Debug.Log(path);
            return result;
        }
        public Vector3 indexToRealCoords(int x, int y){
            return new Vector3(100*(x-graph.GetLength(0)/2),0,100*(y-graph.GetLength(0)/2));
        }
        public void debugDraw(){
            for (int x = 0; x < graph.GetLength(0); x++){
                for (int y = 0; y < graph.GetLength(1); y++){
                    if(graph[x,y] == null) {
                        //FlaxEngine.Debug.Log(x+" "+y);
                        continue;
                    }
                    Node changing = graph[x,y];

                    for(int i = 0; i < 8; i++){
                        if(changing.edges[i]){
                            Vector3 start = indexToRealCoords(x,y);
                            Vector3 end = start;
                            end += new Vector3(Node.dirLookup(i).x*100, 0, Node.dirLookup(i).y*100);
                            DebugDraw.DrawLine(start, end, Color.Blue,0, false);
                        }
                    }
                }
            }
        }
    }

    public class Node{
        //Has neigbours in dir lookup. Dir:
        //  -  <---- x ----> +
        //  +   6    5    4
        //  y   7    X    3
        //  -   0    1    2
        public bool[] edges;

        public Node(){
            edges = new bool[8];
        }

        static public int inverseDir(int d){
            switch(d){
                case 3: return 7;
                case 4: return 0;
                case 5: return 1;
                case 6: return 2;
                case 7: return 3;
                case 0: return 4;
                case 1: return 5;
                case 2: return 6;
                default: return -1;
            }
        }

        static public Vector2I dirLookup(int d){
            switch(d){
                case 3: return new Vector2I(1,0);
                case 4: return new Vector2I(1,1);
                case 5: return new Vector2I(0,1);
                case 6: return new Vector2I(-1,1);
                case 7: return new Vector2I(-1,0);
                case 0: return new Vector2I(-1,-1);
                case 1: return new Vector2I(0,-1);
                case 2: return new Vector2I(1,-1);
                default: return null;
            }
        }
    }
}
