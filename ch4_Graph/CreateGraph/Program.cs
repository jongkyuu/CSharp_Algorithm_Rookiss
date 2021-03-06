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
            { 0, 1, 0, 1, 0, 0 },
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

        // 방문한 시점에서의 추가적인 정보를 잘 기입하면 많은 정보를 도출할 수 있다.
        public void BFS(int start)
        {
            bool[] found = new bool[6];
            int[] parent = new int[6];
            int[] distance = new int[6];

            Queue<int> q = new Queue<int>();
            q.Enqueue(start);

            found[start] = true;
            parent[start] = start;
            distance[start] = 0;

            while(q.Count > 0)
            {
                int now = q.Dequeue();
                Console.WriteLine(now);

                for(int next=0; next < adj.GetLength(0); next++)
                {
                    if (adj[now, next] == 0)  // 인접하지 않았으면 스킵
                        continue;
                    if (found[next])  // 이미 발견했다면 스킵
                        continue;

                    q.Enqueue(next);

                    found[next] = true;
                    parent[next] = now;
                    distance[next] = distance[now] + 1;
                }
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
        // adj3은 3번과 4번 사이 연결된 엣지를 끊음

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



        int[,] adj4 = new int[6, 6]
        {
            { -1, 15, -1, 35, -1, -1 },
            { 15, -1, 05, 10, -1, -1 },
            { -1, 05, -1, -1, -1, -1 },
            { 35, 10, -1, -1, 05, -1 },
            { -1, -1, -1, 05, -1, 05 },
            { -1, -1, -1, -1, 05, -1 },
        };

        public void Dijikstra(int start)
        {
            bool[] visited = new bool[6]; // find 여부보다 실제로 방문 했는지가 중요함
            int[] distance = new int[6];  // 여기서는 최단거리를 기입
            int[] parent = new int[6]; // 부모 정점을 기입

            // 초기값이 0인지, 방문을 못해서 0인지 헷갈리기 떄문에 기본값을 매우 큰 값으로 설정
            Array.Fill(distance, Int32.MaxValue);

            distance[start] = 0;
            parent[start] = 0;

            while(true)
            {
                // 제일 좋은 후보를 찾는다.(가장 가까이에 있는)

                // 가장 유력한 후보의 거리와 번호를 저장한다
                int closest = Int32.MaxValue; // 엄청 큰 값을 초기값으로 셋팅
                int now = -1;  // -1이라는 비현실적인 값을 셋팅

                for (int i = 0; i < adj4.GetLength(0); i++)
                {
                    // 이미 방문한 정점은 스킵
                    if (visited[i])
                        continue;
                    // 아직 발견(예약)된 적이 없거나 기존 후보보다 멀리 있으면 스킵
                    if (distance[i] == Int32.MaxValue || distance[i] >= closest)
                        continue;

                    // 여기까지 왔으면 가장 좋은 후보이므로 정보를 갱신
                    closest = distance[i];
                    now = i;

                }

                // 다음 후보가 하나도 없다 -> 종료
                // 이미 모든 점을 다 찾았거나 연결이 단절되어 있음 
                if (now == -1)
                    break;

                // 제일 좋은 후보를 찾았으니 방문한다
                visited[now] = true;

                // 방문한 정점과 인접한 정점들을 조사해서, 상황에 따라 발견한 최단거리를 갱신한다.
                for (int next = 0; next < 6; next++)
                {
                    // 연결되지 않은 정점 스킵
                    if (adj4[now, next] == -1)
                        continue;
                    // 이미 방문한 정점은 스킵 
                    if (visited[next])
                        continue;

                    // 새로 조사된 정점의 최단거리를 계산한다
                    int nextDist = distance[now] + adj4[now, next];
                    // 만약 기존에 발견한 최단거리가 새로 조사된 최단거리보다 크면 정보를 갱신
                    // next가 한번도 방문하지 않은 지점일 경우 distance[next]는 매우 큰값이므로 if 안쪽으로 들어감
                    if(nextDist < distance[next])
                    {
                        distance[next] = nextDist;
                        parent[next] = now;
                    }
                }
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            // DFS (Depth First Search 깊이 우선 탐색)
            Graph graph = new Graph();
            //graph.DFS(3);
            //graph.DFS2(0);
            //graph.SearchAll();

            // BFS (Breadth Firsh Search 너비 우선 탐색)
            // 최단거리 길찾기에 주로 사용
            //graph.BFS(0);

            // Dajikstra (다익스트라)
            graph.Dijikstra(0);

        }

    }
}
