using System;
using System.Collections.Generic;
using System.Runtime;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// PathManager Script.
    /// </summary>
    public class PathManager : Script
    {
        /// <inheritdoc/>
        public override void OnStart()
        {
            /*
            //Testing Path finding funcionality
            DateTime StartingTime = DateTime.Now;
            Graph testGraph = new Graph();
            testGraph.populateGraph(Vector3.Zero, 10);
            Vector3 start = new Vector3(300, 0, 500);
            Vector3 end = new Vector3(-200, 0, -100);
            Path p = testGraph.findPath(start, end);
            Debug.Log(p.ToString());
            Debug.Log((DateTime.Now - StartingTime).ToString());
            */
            
            /* 

            //testing edge removal
            Graph testGraph = new Graph();
            testGraph.addEdgeDoubleEucledianUnsure(Vector3.Zero, Vector3.UnitX);
            testGraph.removeEdge(new Edge(Vector3.Zero, Vector3.UnitX, 1));
            
            */
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
        public override void OnUpdate()
        {
            // Here you can add code that needs to be called every frame
        }
    }

public class Edge
{
    private Vector3 from;
    private Vector3 to;
    private float distance;

    public Edge()
    {
        from = Vector3.Zero;
        to = Vector3.Zero;
        distance = 0;
    }

    public Edge(Vector3 nFrom, Vector3 nTo, float nDist)
    {
        from = nFrom;
        to = nTo;
        distance = nDist;
    }

    public void setFrom(Vector3 nFrom) { from = nFrom; }
    public void setTo(Vector3 nTo) { to = nTo; }
    public void setDistance(float nDistance) { distance = nDistance; }

    public Vector3 getFrom() { return from; }
    public Vector3 getTo() { return to; }
    public float getDistance() { return distance; }

    public override String ToString(){
        return "Edge from " + from.ToString() + " to " + to.ToString();
    }

    public override bool Equals(object obj)
    {    
        if (obj == null || GetType() != obj.GetType()) return false;
        
        Edge other = (Edge)obj;
        return (getFrom().Equals(other.getFrom()) && getTo().Equals(other.getTo()) && (getDistance() == other.getDistance()));
    }
    
    public override int GetHashCode()
    {
        return from.GetHashCode() + to.GetHashCode() - Mathd.RoundToInt(distance);
    }

}

    public class Graph
    {
        private List<Vector3> Nodes;

        private Dictionary<Vector3, List<Edge>> Edges;

        public Graph(){
            Nodes = new List<Vector3>();
            Edges = new Dictionary<Vector3, List<Edge>>();
        }

        public void addNode(Vector3 n)
        {
            if (Nodes.Contains(n)) return;
            Nodes.Add(n);
            Edges.Add(n, new List<Edge>());
        }

        public void removeNode(Vector3 n){
            if(!Nodes.Contains(n)) return;
            Nodes.Remove(n);
            foreach(Edge e in Edges[n]){
                Edge reverseE = new Edge(e.getTo(), e.getFrom(), e.getDistance());
                Edges[e.getTo()].Remove(reverseE);
            }
            Edges.Remove(n);
        }

        public void addEdge(Vector3 from, Vector3 to, float dist){
            Edge newEdge = new Edge(from, to, dist);
            Edges[from].Add(newEdge);
        }
        public void addEdgeDouble(Vector3 point1, Vector3 point2, float dist){
            addEdge(point1, point2, dist);
            addEdge(point2, point1, dist);
        }
        public void addEdgeEucledian(Vector3 from, Vector3 to)
        {
            addEdge(from, to, (from - to).Length);
        }
        public void addEdgeDoubleEucledian(Vector3 point1, Vector3 point2){
            addEdgeEucledian(point1, point2);
            addEdgeEucledian(point2, point1);
        }
        public void addEdgeUnsure(Vector3 from, Vector3 to, float dist)
        {
            addNode(from);
            addNode(to);
            addEdge(from, to, dist);
        }
        public void addEdgeDoubleUnsure(Vector3 point1, Vector3 point2, float dist){
            addEdgeUnsure(point1, point2, dist);
            addEdge(point2, point1, dist);
        }
        public void addEdgeEucledianUnsure(Vector3 from, Vector3 to)
        {
            addEdgeUnsure(from, to, (from - to).Length);
        }
        public void addEdgeDoubleEucledianUnsure(Vector3 point1, Vector3 point2){
            addEdgeEucledianUnsure(point1, point2);
            addEdgeEucledian(point2, point1);
        }

        public void removeEdge(Edge e){
            Edges[e.getFrom()].Remove(e); 
        }

        //Here be Dijkstra
        public Path findPath(Vector3 from, Vector3 to){
            //initialize
            double MaxDistance = (to-from).Length + 300; //optimization
            HashSet<Vector3> Q = new HashSet<Vector3>();
            Q.Add(from);
            Dictionary<Vector3, float> distance = new Dictionary<Vector3, float>();
            Dictionary<Vector3, Vector3> predecessor = new Dictionary<Vector3, Vector3>();
            distance[from] = 0;

            //Dijkstras Algorithm
            while(Q.Count != 0){

                //find Node with smallest Distance
                Vector3 nextNode = Vector3.Maximum;
                float smallest = float.MaxValue;
                foreach(Vector3 n in Q){
                    if(distance[n] < smallest){
                        nextNode = n;
                        smallest = distance[n];
                    }
                }
                

                List<Edge> successors = Edges[nextNode];
                foreach(Edge e in successors){

                    Vector3 nextFrom = e.getFrom();
                    Vector3 nextTo = e.getTo();

                    if(!distance.ContainsKey(nextTo)) distance.Add(nextTo, float.MaxValue);
                    if(!predecessor.ContainsKey(nextTo)) predecessor.Add(nextTo, Vector3.Maximum);

                    float altDist = distance[nextFrom] + e.getDistance();
                    if(altDist < distance[nextTo]){
                        distance[nextTo] = altDist;
                        predecessor[nextTo] = nextFrom;
                        if((nextTo-to).Length < MaxDistance && !Q.Contains(nextTo)) Q.Add(nextTo); //first is optimization
                    }
                }
                Q.Remove(nextNode);
                if(nextNode.Equals(to)) break;
            }


            Path result;
            //if Q is 0 and target node hasnt been reached, no path exists
            if(Q.Count == 0){
                result = new Path();
                return result;
            }

            //otherwise create shortest path
            LinkedList<Vector3> shortest = new LinkedList<Vector3>();
            Vector3 nextPre = to;
            while(!nextPre.Equals(Vector3.Maximum)){
                shortest.AddFirst(nextPre);
                nextPre = predecessor[nextPre];
            }

            result = new Path(shortest, true);
            return result;
        }

        public void populateGraph(Vector3 middle, int radius){
            Vector3 rowreference = middle;
            Vector3 Xinc = Vector3.UnitX * 100;
            Vector3 Zinc = Vector3.UnitZ * 100;
            rowreference.Z -= radius*100;

            for(int h = -radius; h <= radius; h++){
                for(int i = -radius; i <= radius; i++){
                    addEdgeDoubleEucledianUnsure(rowreference + i*Xinc, rowreference + (i+1)* Xinc);
                }

                for(int i = -radius; i < radius; i++){
                    addEdgeDoubleEucledianUnsure(rowreference + i*Xinc, rowreference + (i+1)*Xinc - Zinc);
                }

                for(int i = -radius; i < radius; i++){
                    addEdgeDoubleEucledianUnsure(rowreference + (i+1)*Xinc, rowreference + i* Xinc - Zinc);
                }

                for(int i = -radius; i < radius; i++){
                    addEdgeDoubleEucledianUnsure(rowreference + i*Xinc, rowreference + i*Xinc - Zinc);
                }

                rowreference += Zinc;
            }
        }
        
        public void clear(){
            Nodes.Clear();
            Edges.Clear();
        }
    }

    public class Path
    {
        private LinkedList<Vector3> calculatedPath;
        private bool pathExists;

        public Path(LinkedList<Vector3> nCP, bool exists){
            calculatedPath = nCP;
            pathExists = exists;
        }
        
        public Path(){
            calculatedPath = new LinkedList<Vector3>();
            pathExists = false;
        }

        public Vector3 getNextNode(){
            return calculatedPath.First.Value;
        }

        public void addNode(Vector3 newNode){
            calculatedPath.AddLast(newNode);
        }

        public int nodeReached(){
            calculatedPath.RemoveFirst();
            if (calculatedPath.Count == 0) return -1;
            else return 0;
        }

        public bool isFinished() { return(0 == calculatedPath.Count); }

        public bool getPathExists(){ return pathExists; }

        public override String ToString(){
            String res = "Exists: " + pathExists + " ";

            foreach(Vector3 n in calculatedPath){
                res += n.ToString() + " --> ";
            }

            res += "END";
            return res;
        }
    }

}
