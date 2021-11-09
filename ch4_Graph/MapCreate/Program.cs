using System;

namespace MapCreate
{
    class Program
    {
        static void Main(string[] args)
        {
            // Board의 타일 정보만 가지고도 그래프를 그릴 수 있음
            // BFS를 이용해 최단거리 탐색을 할 수 있음 

            Console.CursorVisible = false; // 커서 안보이도록 설정
            Player player = new Player();
            Board board = new Board();
            board.Initialize(25, player);
            player.Initialize(1, 1, board);

            const int WAIT_TICK = (1 * 1000) / 30;  // ms 이기 때문에 1000을 곱해줘야함
            int lastTick = 0; // 마지막으로 측정한 시간 저장

            while (true) // 보통 게임에서는 사용자가 종료하지 않는 이상 무한 루프를 돈다
            {
                #region 설명
                // 입력
                // - 사용자가 키보드, 마우스를 입력하는 input을 감지
                // 로직
                // - 입력에 따라 게임 로직이 실행. 
                // - 몬스터의 AI 등 기타 로직 실행
                // - 온라인 게임은 게임 로직 연산을 서버에서 하는 차이만 있음. 네트워크 통신을 통해 로직의 결과를 주고받는다
                // 렌더링
                // - 연산된 게임세상?을 랜더링 단계에서 그려줌. DirectX나 OpenGL등의 그래픽스 라이브러리 등을 이용해서 화면에 출력
                // FPS 프레임 (60프레임이면 부드럽게 보이고, 30프레임 이하로 떨어지면 끊기는 느낌이 있음)
                // -렌더링 부분을 자주 해줄수록 화면이 부드럽게 보임
                #endregion

                #region 프레임관리(30프레임으로 설정)
                int currentTick = System.Environment.TickCount;  // 현재시간 저장. TickCount는 절대적인 시간은 아니고 시스템이 시작된 이후 경과된 밀리세컨드임.

                //만약 경과한 시간이 1/30 초보다 작다면 
                if (currentTick - lastTick < WAIT_TICK)
                    continue;
                int deltaTick = currentTick - lastTick;  // 경과 시간
                lastTick = currentTick;
                #endregion

                // 로직
                player.Update(deltaTick);

                // 랜더링
                Console.SetCursorPosition(0, 0);
                board.Render();
            }
        }
    }
}
