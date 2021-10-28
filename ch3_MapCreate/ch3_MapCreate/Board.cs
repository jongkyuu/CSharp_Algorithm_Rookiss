using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch3_MapCreate
{
    class Board
    {
        public TileType[,] _tile;  // 배열
        public int _size;
        const char CIRCLE = '\u25cf';

        public enum TileType
        {
            Empty,
            Wall
        }

        public void Initialize(int size)
        {
            _tile = new TileType[size, size];
            _size = size;

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (x == 0 || x == _size - 1 || y == 0 || y == _size - 1)
                        _tile[y, x] = TileType.Wall;
                    else
                        _tile[y, x] = TileType.Empty;

                }
            }
        }

        public void BinaryTreeMaze(int size)
        {
            if (size % 2 == 0)
                return;
            _tile = new TileType[size, size];
            _size = size;

            // Mazes for Programmers

            GenerateByBinaryTree();
        }

        void GenerateByBinaryTree()
        {
            // 우선 길을 다 막아버리는 작업
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        _tile[y, x] = TileType.Wall;
                    else
                        _tile[y, x] = TileType.Empty;
                }
            }

            // 랜덤으로 우측 or 아래로 길을 뚫는 작업
            // Binary Tree Algorithm, 맨 마지막 앞줄은 다 일렬로 연결되는 단점이 있음 
            Random rand = new Random();
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == _size - 2 && x == _size - 2)
                        continue;

                    if (y == _size - 2)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == _size - 2)
                    {
                        _tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                    }
                    else
                        _tile[y + 1, x] = TileType.Empty;

                }
            }
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;

            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    //if (y == 3 && x == 1)
                    //{
                    //    Console.ForegroundColor = ConsoleColor.Blue;
                    //    Console.Write(CIRCLE);
                    //    continue;
                    //}
                    Console.ForegroundColor = GetTileColor(_tile[y, x]);
                    Console.Write(CIRCLE);


                }
                Console.WriteLine();
            }

            Console.ForegroundColor = prevColor;
        }

        ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Green;
            }
        }

    }
}
