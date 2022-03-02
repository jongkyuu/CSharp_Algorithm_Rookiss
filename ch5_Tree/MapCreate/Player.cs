using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreate
{
    class Pos
    {
        public int Y { get; set; }
        public int X { get; set; }

        public Pos(int y, int x)
        {
            Y = y;
            X = x;
        }
    }
    class Player
    {
        // 외부에서도 사용할 것이기 때문에 프로퍼티로 만듬
        public int PosY { get; private set; }
        public int PosX { get; private set; }  // 플레이어의 위치는 외부에서 수정할 수 없음
        Random _random = new Random();

        Board _board;

        int _direction = (int)Direction.Up;
        List<Pos> _points = new List<Pos>();

        enum Direction
        {
            // 반시계 방향
            Up = 0,
            Left = 1,
            Down = 2,
            Right = 3
        }

        public void Initialize(int posY, int posX, Board board)
        {
            PosY = posY;
            PosX = posX;
            _board = board;

            //RightHand();
            BFS();
        }

        // 현재 30FPS로 설정되어서 1/30 초마다 Update()가 실행됨
        // 1/30초마다 한칸씩 이동하면 너무 빠르므로 이전 틱과 현재 틱 사이의 경과된 시간을 넘겨줌.  시간에 따라 행동 결정
        const int MOVE_TICK = 10; // 100ms (0.1초)
        int _sumTick = 0;
        int _lastIndex = 0;

        public void Update(int deltaTick)
        {
            if (_lastIndex >= _points.Count)
                return;

            _sumTick += deltaTick;
            if (_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                // 여기에 0.1초마다 실행될 로직을 넣어줌
                PosY = _points[_lastIndex].Y;
                PosX = _points[_lastIndex].X;
                _lastIndex++;
            }
        }

        void BFS()
        {
            // Up Left Down Right 순서
            int[] deltaY = new int[]{-1, 0, 1, 0}; 
            int[] deltaX = new int[]{0, -1, 0, 1};

            bool[,] found = new bool[_board.Size, _board.Size];
            Pos[,] parent = new Pos[_board.Size, _board.Size];

            Queue<Pos> q = new Queue<Pos>();
            q.Enqueue(new Pos(PosY, PosX));   // 시작점
            found[PosY, PosX] = true;
            parent[PosY, PosX] = new Pos(PosY, PosX);

            while (q.Count > 0)
            {
                Pos pos = q.Dequeue();
                int nowY = pos.Y;
                int nowX = pos.X;

                for (int i = 0; i < 4; i++)
                {
                    int nextY = nowY + deltaY[i];
                    int nextX = nowX + deltaX[i];

                    if (nextX < 0 || nextX > _board.Size || nextY < 0 || nextY > _board.Size)
                        continue;

                    if (_board.Tile[nextY, nextX] == Board.TileType.Wall)
                        continue;

                    if(found[nextY, nextX] == true)
                        continue;

                    q.Enqueue(new Pos(nextY, nextX));
                    found[nextY, nextX] = true;
                    parent[nextY, nextX] = new Pos(nowY, nowX);
                }
            }

            int y = _board.DestY;
            int x = _board.DestX;

            while (parent[y, x].Y != y || parent[y, x].X != x)   // parent와 현재 좌표가 일치하는 점은 시작점 딱 하나임. 시작점에 도달할때 까지 거슬러 올라감
            {
                _points.Add(new Pos(y, x));
                Pos pos = parent[y, x];
                y = pos.Y;
                x = pos.X;
            }

            _points.Add(new Pos(y, x));
            _points.Reverse();

        }

        void RightHand()
        {
            // 현재 바라보고 있는 방향을 기준으로 좌표 변화를 나타낸다.
            int[] frontY = new int[] { -1, 0, 1, 0 };
            int[] frontX = new int[] { 0, -1, 0, 1 };
            int[] rightY = new int[] { 0, -1, 0, 1 };
            int[] rightX = new int[] { 1, 0, -1, 0 };

            _points.Add(new Pos(PosY, PosX));

            // 목적지 도착하기 전에는 계속 실행
            while (PosY != _board.DestY || PosX != _board.DestX)
            {
                // 1. 현재 바라보는 방향을 기준으로 오른쪽으로 갈 수 있는지 확인
                if (_board.Tile[PosY + rightY[_direction], PosX + rightX[_direction]] == Board.TileType.Empty)
                {
                    // 오른쪽 방향으로 90도 회전
                    _direction = (_direction - 1 + 4) % 4;
                    // 앞으로 한 보 전진
                    PosY = PosY + frontY[_direction];
                    PosX = PosX + frontX[_direction];
                    _points.Add(new Pos(PosY, PosX));

                }
                // 2. 현재 바라보는 방향을 기준으로 전진할 수 있는지 확인
                else if (_board.Tile[PosY + frontY[_direction], PosX + frontX[_direction]] == Board.TileType.Empty)
                {
                    // 앞으로 한 보 전진
                    PosY = PosY + frontY[_direction];
                    PosX = PosX + frontX[_direction];
                    _points.Add(new Pos(PosY, PosX));

                }
                else
                {
                    // 왼쪽 방향으로 90도 회전
                    _direction = (_direction + 1 + 4) % 4;
                }
            }
        }

    }
}
