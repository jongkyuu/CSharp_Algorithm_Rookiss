using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch3_MapCreate
{
    class Player
    {
        // 외부에서도 사용할 것이기 때문에 프로퍼티로 만듬
        public int PosY { get; private set; }
        public int PosX { get; private set; }  // 플레이어의 위치는 외부에서 수정할 수 없음
        Random _random = new Random();

        Board _board;

        public void Initialize(int posY, int posX, int destY, int destX, Board board)
        {
            PosY = posY;
            PosX = posX;
            _board = board;
        }

        // 현재 30FPS로 설정되어서 1/30 초마다 Update()가 실행됨
        // 1/30초마다 한칸씩 이동하면 너무 빠르므로 이전 틱과 현재 틱 사이의 경과된 시간을 넘겨줌.  시간에 따라 행동 결정
        const int MOVE_TICK = 100; // 100ms (0.1초)
        int _sumTick = 0;
        public void Update(int deltaTick)
        {
            _sumTick += deltaTick;
            if(_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                // 여기에 0.1초마다 실행될 로직을 넣어줌
                int randomValue = _random.Next(0, 5);
                switch (randomValue)
                {
                    case 0: // 상
                        if (PosY - 1 >= 0 && _board.Tile[PosY - 1, PosX] == Board.TileType.Empty)
                            PosY -= 1;
                        break;
                    case 1: // 하
                        if (PosY + 1 < _board.Size && _board.Tile[PosY + 1, PosX] == Board.TileType.Empty)
                            PosY += 1;
                            break;
                    case 2: // 좌
                        if (PosX - 1 >= 0 && _board.Tile[PosY, PosX - 1] == Board.TileType.Empty)
                            PosX -= 1;
                        break;
                    case 3: // 우
                        if (PosX + 1 < _board.Size &&_board.Tile[PosY, PosX + 1] == Board.TileType.Empty)
                            PosX += 1;
                        break;
                }
            }
        }


    }
}
