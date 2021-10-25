using System;

namespace ch2_1_Array_LinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false; // 커서 안보이도록 설정

            Board board = new Board();
            board.List_Initialize();
            board.MyList_Initialize();
            board.LinkedList_Initialize();
            board.MyLinkedList_Initialize();



            const int WAIT_TICK = (1 * 1000) / 30;  // ms 이기 때문에 1000을 곱해줘야함
            const char CIRCLE = '\u25cf';

            int lastTick = 0; // 마지막으로 측정한 시간 저장

            while (true) // 보통 게임에서는 사용자가 종료하지 않는 이상 무한 루프를 돈다
            {
                // 입력
                // 사용자가 키보드, 마우스를 입력하는 input을 감지
                // 로직
                // 입력에 따라 게임 로직이 실행. 
                // 몬스터의 AI 등 기타 로직 실행
                // 온라인 게임은 게임 로직 연산을 서버에서 하는 차이만 있음. 네트워크 통신을 통해 로직의 결과를 주고받는다
                // 렌더링
                // 연산된 게임세상?을 랜더링 단계에서 그려줌. DirectX나 OpenGL등의 그래픽스 라이브러리 등을 이용해서 화면에 출력

                // FPS 프레임 (60프레임이면 부드럽게 보이고, 30프레임 이하로 떨어지면 끊기는 느낌이 있음)
                // 렌더링 부분을 자주 해줄수록 화면이 부드럽게 보임

                #region 프레임관리
                int currentTick = System.Environment.TickCount;  // 현재시간 저장. TickCount는 절대적인 시간은 아니고 시스템이 시작된 이후 경과된 밀리세컨드임.
                //int elapsedTick = currentTick - lastTick;  // 경과 시간

                //만약 경과한 시간이 1/30 초보다 작다면 
                if (currentTick - lastTick < WAIT_TICK)
                    continue;
                lastTick = currentTick;
                #endregion

                Console.SetCursorPosition(0, 0); // 커서를 (0, 0)에 위치시켜줌

                for (int i = 0; i < 25; i++)
                {
                    for (int j = 0; j < 25; j++)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(CIRCLE);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
