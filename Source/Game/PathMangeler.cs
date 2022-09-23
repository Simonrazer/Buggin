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
        public override void OnStart()
        {
            // Here you can add code that needs to be called when script is created, just before the first game update
            
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
        public override void OnUpdate()
        {
            // Here you can add code that needs to be called every frame
            stopwatch.Reset();
                stopwatch.Start();
            NodeGraph gameGraph = new NodeGraph(200, 200);
            gameGraph.getPath(0,0,10,10);
            stopwatch.Stop();
            FlaxEngine.Debug.Log(stopwatch.ElapsedMilliseconds);
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

        public NodeGraph (int w, int h){
            graph = new Node[w,h];
            for (int x = 0; x < graph.GetLength(0); x++){
                for (int y = 0; y < graph.GetLength(1); y++){
                    graph[x,y] = new Node();
                }
            }
            this.fullRelink();
        }

        public void fullRelink(){
            for (int x = 1; x < graph.GetLength(0)-1; x++){
                for (int y = 1; y < graph.GetLength(1)-1; y++){
                    if(graph[x,y] == null) continue;
                    Node changing = graph[x,y];
                    changing.edges[7] = (graph[x-1,y-1] != null);
                    changing.edges[0] = (graph[x,y-1] != null);
                    changing.edges[1] = (graph[x+1,y-1] != null);
                    changing.edges[6] = (graph[x-1,y] != null);
                    changing.edges[2] = (graph[x+1,y] != null);
                    changing.edges[5] = (graph[x-1,y+1] != null);
                    changing.edges[4] = (graph[x,y+1] != null);
                    changing.edges[3] = (graph[x+1,y+1] != null);
                }
            }

            for (int x = 1; x < graph.GetLength(0)-1; x++){
                Node changing;
                Top:
                    int y = 0;
                    if(graph[x,y] == null) goto Bottom;
                    changing = graph[x,y];
                    changing.edges[5] = (graph[x-1,y+1] != null);
                    changing.edges[4] = (graph[x,y+1] != null);
                    changing.edges[3] = (graph[x+1,y+1] != null);

                Bottom:
                    y = graph.GetLength(1)-1;
                    if(graph[x,y] == null) continue;
                    changing = graph[x,y];
                    changing.edges[7] = (graph[x-1,y-1] != null);
                    changing.edges[0] = (graph[x,y-1] != null);
                    changing.edges[1] = (graph[x+1,y-1] != null);
            }

            for (int y = 1; y < graph.GetLength(1)-1; y++){
                Node changing;
                Right:
                    int x = 0;
                    if(graph[x,y] == null) goto Left;
                    changing = graph[x,y];
                    changing.edges[1] = (graph[x+1,y-1] != null);
                    changing.edges[2] = (graph[x+1,y] != null);
                    changing.edges[3] = (graph[x+1,y+1] != null);

                Left:
                    x = graph.GetLength(0)-1;
                    if(graph[x,y] == null) continue;
                    changing = graph[x,y];
                    changing.edges[7] = (graph[x-1,y-1] != null);
                    changing.edges[6] = (graph[x-1,y] != null);
                    changing.edges[5] = (graph[x-1,y+1] != null);
            }

            {
                int x = 0; int y = 0;
                if(graph[x,y] != null){
                    Node changing = graph[x,y];
                    changing.edges[2] = (graph[x+1,y] != null);
                    changing.edges[3] = (graph[x+1,y+1] != null);
                    changing.edges[4] = (graph[x,y+1] != null);
                }
            }

            {
                int x = graph.GetLength(0)-1; int y = 0;
                if(graph[x,y] != null){
                    Node changing = graph[x,y];
                    changing.edges[6] = (graph[x-1,y] != null);
                    changing.edges[5] = (graph[x-1,y+1] != null);
                    changing.edges[4] = (graph[x,y+1] != null);
                }
            }

           {
                int x = graph.GetLength(0)-1; int y = graph.GetLength(1)-1;
                if(graph[x,y] != null){
                    Node changing = graph[x,y];
                    changing.edges[7] = (graph[x-1,y-1] != null);
                    changing.edges[0] = (graph[x,y-1] != null);
                    changing.edges[6] = (graph[x-1,y] != null);
                }
            }

            {
                int x = 0; int y = graph.GetLength(1)-1;
                if(graph[x,y] != null){
                    Node changing = graph[x,y];
                    changing.edges[2] = (graph[x+1,y] != null);
                    changing.edges[0] = (graph[x,y-1] != null);
                    changing.edges[1] = (graph[x+1,y-1] != null);
                }
            }
        }

        public Node GetNode(int x, int y){
            return graph[x+graph.GetLength(0)/2, y+graph.GetLength(1)/2];
        }
        public void getPath(int sx, int sy, int dx, int dy){
            Vector2I currentPos = new Vector2I(sx, sy);
            Vector2I dest = new Vector2I(dx, dy);

            String path = currentPos.toString();
            while(!Vector2I.EqualVals(currentPos, dest)){
                Vector2I dir = Vector2I.Sub(dest,currentPos);
                double angle = Math.Acos(Vector2I.Dot(new Vector2I(0,-1), dir)/dir.Length());
                
                int slicedDir = (int)(angle*8/(2*Math.PI));
                //Debug.Log(slicedDir);
                if(this.GetNode(currentPos.x, currentPos.y).edges[slicedDir]){
                    currentPos = Vector2I.Add(currentPos, Node.dirLookup(slicedDir));
                }
                else{
                    path+="FUCK";
                    break;
                }
                path+="->"+currentPos.toString();
            }
            //Debug.Log(path);
        }
    }

    public class Node{
        //Has neigbours in dir lookup. Dir:
        //       x ---->
        // y     7    0    1
        // |     6    X    2
        // V     5    4    3
        public bool[] edges;

        public Node(){
            edges = new bool[8];
        }

        static public Vector2I dirLookup(int d){
            switch(d){
                case 0: return new Vector2I(0,-1);
                case 1: return new Vector2I(1,-1);
                case 2: return new Vector2I(1,0);
                case 3: return new Vector2I(1,1);
                case 4: return new Vector2I(0,1);
                case 5: return new Vector2I(-1,1);
                case 6: return new Vector2I(-1,0);
                case 7: return new Vector2I(-1,-1);
                default: return null;
            }
        }
    }
}
