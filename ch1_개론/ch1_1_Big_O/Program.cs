using System;

namespace ch1_1_Big_O
{
    class Program
    {
        static void Main(string[] args)
        {
            // 알고리즘을 연구하는 이유 : 효율적인 코드를 만들기 위해서
            // 프로그램 짜서 실행 속도 비교하는 방법? -> 환경에 의존적인 문제가 있음
            // 입력이 적은 구간과 많은 구간에서의 성능 차이가 날 수 있음

            // Big-O 표기법은 수행되는 연산의 개수를 대략적으로 판단

            Program a = new Program();
            a.Add(1);

            
        }

        // 1번 연산
        public int Add(int n)
        {
            return n + n;
        }

        // N+1번 연산
        public int Add2(int n)
        {
            int sum = 0;
            for(int i=0; i < n; i++)
            {
                sum += i;
            }
            return sum;
        }

        // N^2 + 1 번 연산
        public int Add3(int n)
        {
            int sum = 0;
            for (int i=0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    sum += 1;
                }
            }
            return sum;
        }

        // 영향력이 가장 큰 대표 항목만 남기고 삭제
        // 상수는 무시(2N -> N)
        // 아래 Add4는 1 + N + 4*N^2 + 1 번 연산하지만 O(N^2) 으로 표기한다. 
        public int Add4(int n)
        {
            int sum = 0;   // 1번 연산

            for (int i=0; i < n; i++)   // N번 연산
            {
                sum += 1;
            }

            for(int i=0; i< 2*n; i++)  // 4*N^2번 연산
            {
                for (int j = 0; j < 2*n; j++)
                {
                    sum += 1;
                }
            }

            sum += 1234567;  // 1번 연산 

            return sum;
        }
    }
}
