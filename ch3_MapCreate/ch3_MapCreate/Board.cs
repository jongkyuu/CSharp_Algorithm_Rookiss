using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch3_MapCreate
{
    class Board
    {
        public TileType[,] Tile { get; private set; }  // 배열
        public int Size { get; private set; }
        const char CIRCLE = '\u25cf';
        Player _player;

        public int DestY { get; set; }
        public int DestX { get; set; }

        public enum TileType
        {
            Empty,
            Wall
        }

        public void Initialize(int size, Player player)
        {
            if (size % 2 == 0)
                return;

            _player = player;
            Tile = new TileType[size, size];
            Size = size;

            DestY = Size - 2;
            DestX = Size - 2;

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x == 0 || x == Size - 1 || y == 0 || y == Size - 1)
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;

                }
            }
        }

        public void BinaryTreeMaze(int size)
        {
            if (size % 2 == 0)
                return;
            Tile = new TileType[size, size];
            Size = size;

            // Mazes for Programmers 책에 나오는 알고리즘
            //GenerateByBinaryTree();
            GenerateBySideWinder();
        }

        void GenerateByBinaryTree()
        {
            // 우선 길을 다 막아버리는 작업
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;
                }
            }

            // 랜덤으로 우측 or 아래로 길을 뚫는 작업
            // Binary Tree Algorithm, 맨 마지막 앞줄은 다 일렬로 연결되는 단점이 있음 
            Random rand = new Random();
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    #region 맨 오른쪽, 맨 아래 벽 뚫리는걸 방지하는 부분
                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }
                    #endregion

                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                    }
                    else
                        Tile[y + 1, x] = TileType.Empty;

                }
            }
        }

        void GenerateBySideWinder()
        {
            // 우선 길을 다 막아버리는 작업
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)  // 0, 2,  4, 8 ... 막음
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;
                }
            }

            // 랜덤으로 우측 or 아래로 길을 뚫는 작업
            // 맨 오른쪽 -1, 맨 아래 -1 부분은 일렬로 연결되는 단점 있음
            Random rand = new Random();
            for (int y = 0; y < Size; y++)
            {
                int count = 1;
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    #region 맨 오른쪽, 맨 아래 벽 뚫리는걸 방지하는 부분
                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    if (y == Size - 2)  // 맨 아래 앞줄일때 오른쪽으로만 뚫기
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == Size - 2)  // 멘 오른쪽 앞줄일때 아래쪽으로만 뚫기
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }
                    #endregion

                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        count++;
                    }
                    else
                    {
                        int randomIndex = rand.Next(0, count);
                        Tile[y + 1, x - randomIndex * 2] = TileType.Empty;
                        count = 1;
                    }

                }
            }
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    // 플레이어 좌표를 가지고 와서, 그 좌표랑 현재 (y, x) 가 일치하면 플레이어 전용 색삭으로 표시
                    if (y == _player.PosY && x == _player.PosX)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if (y == DestY && x == DestX)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = GetTileColor(Tile[y, x]);
                    }

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
