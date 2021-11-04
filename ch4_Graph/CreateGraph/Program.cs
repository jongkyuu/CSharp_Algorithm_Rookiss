using System;
using System.Collections.Generic;

namespace CreateGraph
{
    class Graph
    {
        // 그래프를 표현하는 방법
        // 행렬(2차원 배열) 버전
        int[,] adj = new int[6, 6]
        {
            { 0, 1, 1, 0, 0, 0 },
            { 1, 0, 1, 1, 0, 0 },
            { 0, 1, 0, 0, 0, 0 },
            { 1, 1, 0, 0, 1, 0 },
            { 0, 0, 0, 1, 0, 1 },
            { 0, 0, 0, 0, 1, 0 },
        };

        bool[] visited = new bool[6];
        // 1) 우선 now부터 방문하고, (now는 시작점을 의미)
        // 2) now와 연결된 정점들을 하나씩 확인해서, 아직 미방문 상태라면 방문한다.
        public void DFS(int now)
        {
            Console.WriteLine(now);
            visited[now] = true;  // 우선 now 부터 방문, 방문한 곳은 true로 표시해준다

            for (int next = 0; next < adj.GetLength(0); next++)
            {
                if (adj[now, next] == 0)   // 현재 vertex와 next 정점 사이에 연결된 통로가 없으면 skip
                    continue;
                if (visited[next])  // 이미 방문했으면 skip
                    continue;
                DFS(next);

            }
        }


        // 리스트를 이용한 버전
        // 불필요한 정보를 제외하고 꼭 필요한 인덱스 정보만 넣어줌
        List<int>[] adj2 = new List<int>[]
        {
            new List<int>() {1, 3},
            new List<int>() {0, 2, 3},
            new List<int>() {1},
            new List<int>() {0, 1, 4},
            new List<int>() {3, 5},
            new List<int>() {4},
        };


        bool[] visited2 = new bool[6];
        public void DFS2(int now)
        {
            Console.WriteLine(now);
            visited2[now] = true;   // 1) 우선 now부터 방문하고

            foreach(int next in adj2[now])  // 연결되어 있지 않으면 스킵(adj2에는 연결된 정점만 리스트에 들어가 있음)
            {
                if (visited2[next])  // 이미 방문했으면 스킵
                    continue;
                DFS2(next);
            }
        }

        // 만약 모든 정점이 연결된게 아니라 중간에 길이 끊겨있다면 일부만 서치하고 끝나게 되는 문제가 있음
        // 혹시 방문하지 않은 정점이 있다면 그 정점을 기준으로 다시 DFS를 해야함

        int[,] adj3 = new int[6, 6]
        {
            { 0, 1, 1, 0, 0, 0 },
            { 1, 0, 1, 1, 0, 0 },
            { 0, 1, 0, 0, 0, 0 },
            { 1, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 1, 0 },
        };

        bool[] visited3 = new bool[6];

        public void DFS3(int now)
        {
            Console.WriteLine(now);
            visited3[now] = true;

            for (int next = 0; next < adj3.GetLength(0); next++)
            {
                if (visited3[next])
                    continue;
                if (adj3[now, next] == 0)
                    continue;
                DFS3(next);
            }
        }

        public void SearchAll()
        {
            visited3 = new bool[6];

            for (int now = 0; now< adj.GetLength(0); now++)
            {
                if (visited3[now] == false)
                    DFS3(now);
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            // DFS (Depth First Search 깊이 우선 탐색)
            // BFS (Breadth Firsh Search 너비 우선 탐색)
            Graph graph = new Graph();
            //graph.DFS(3);
            //graph.DFS2(0);
            graph.SearchAll();
        }

    }
}
