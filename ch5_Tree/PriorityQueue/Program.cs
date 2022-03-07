using System;
using System.Collections.Generic;

namespace PriorityQueue
{
    class PriorityQueue
    {
        // 22-3. 힙 트리와 비슷. 엄밀이 얘기하면 바이너리 힙.
        List<int> _heap = new List<int>();    // 22-4. 동적 배열로 관리

        public void Push(int data)
        {
            // 22-5. 힙의 맨 끝에 새로운 데이터를 삽입한다
            _heap.Add(data);

            int now = _heap.Count - 1;  // 22-6. 시작 위치 now

            // 22-7. 도장 꺠기를 시작
            while(now > 0)
            {
                // 22-8. 도장 꺠기 시도
                int next = (now - 1) / 2; // 22-9. 부모 노드 위치
                if (_heap[now] < _heap[next])  // 22-10 부모 노드와 비교
                    break;

                // 22-11 두 값을 교체
                int temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                // 22-12 검사 위치를 이동

                now = next;
            }
        }

        public int Pop()
        {
            // 22-13 반환할 데이터를 따로 저장. 무조건 하나 이상의 데이터가 _heap에 있다고 가정한다
            int ret = _heap[0];

            // 22-14 마지막 데이터를 루트로 이동
            int lastIndex = _heap.Count - 1;
            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex); // 22-15 마지막 인덱스에 있는 노드를 삭제
            lastIndex--;

            // 22-15
            // 역으로 도장꺠기를 시작한다
            // 자식 노드 양쪽 모두가 부모 노드보다 크다면 자식 노드 중 큰 숫자와 부모 노드를 바꿔 준다
            int now = 0;
            while (true)
            {
                int left = 2 * now + 1;
                int right = 2 * now + 2;

                int next = now;
                // 22-16 여기서 조심해야할건 노드를 타고 내려가다 보면 left와 right가 범위를 벗어날 수 있다. left <= lastIndex 조건 필요
                // 왼쪽값이 현재값보다 크면 왼쪽으로 이동
                if (left <= lastIndex && _heap[next] < _heap[left])
                {
                    next = left;
                }
                // 22-17 오른쪽값이 현재값(왼쪽 이동 포함)보다 크면 오른쪽으로 이동
                if (right <= lastIndex && _heap[next] < _heap[right])
                {
                    next = right;
                }
                // 22-18 왼쪽 오른쪽 모두 현재 값보다 작으면 종료
                if (next == now)
                    break;

                // 22-19 두 값을 교체
                int temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                // 22-20 검사 위치 이동
                now = next;

            }

            return ret;
        }

        public int Count()
        {
            return _heap.Count;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 22-1. PriorityQueue가 아니라 일반적인 int형 queue였다면 선입선출이기 떄문에 데이터를 넣은 순서대로 나온다.
            // 22-2. PriorityQueue는 우선순위가 높은 순으로 데이터가 나온다(힙 트리와 관련이 있음)
            PriorityQueue q = new PriorityQueue();
            q.Push(20);
            q.Push(10);
            q.Push(30);
            q.Push(90);
            q.Push(40);

            while (q.Count() > 0)
            {
                Console.WriteLine(q.Pop());
            }

            // 22-21 PriorityQueue는 시간복잡도 log(N)
        }
    }
}
