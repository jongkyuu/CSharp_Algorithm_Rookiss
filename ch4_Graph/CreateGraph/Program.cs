﻿using System;
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
    }

    class Program
    {
        static void Main(string[] args)
        {
            // DFS (Depth First Search 깊이 우선 탐색)
            // BFS (Breadth Firsh Search 너비 우선 탐색)
        }

    }
}
