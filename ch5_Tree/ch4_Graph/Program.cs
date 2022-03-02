using System;
using System.Collections.Generic;

namespace ch4_Graph
{

    // 스택 : LIFO(후입선출 Last in First Out)
    // 큐 : FIFO(선입선출 First in First Out), 먼저 들어온 순서대로 먼저 나감



    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(101); 
            stack.Push(102);
            stack.Push(103);
            stack.Push(104);
            stack.Push(105);

            if (stack.Count > 0)  // stack이 비어있을때 Pop이나 Peek을 하면 Crash 발생
            {
                int data = stack.Pop();  // 마지막 데이터를 뽑아냄
                int data2 = stack.Peek();   // 마지막 데이터를 엿봄
            }


            Queue<int> queue = new Queue<int>();
            queue.Enqueue(101);
            queue.Enqueue(102);
            queue.Enqueue(103);
            queue.Enqueue(104);
            queue.Enqueue(105);

            if(queue.Count > 0)
            {
                int data3 = queue.Dequeue();
                int data4 = queue.Peek();
            }

            // Stack과 Queue를 LinkedList나 List로도 만들수 있지만 굳이 Stack이나 Queue를 사용하는 이유는?
            // List는 Stack의 경우에 마지막에 숫자를 넣거나 빼는 작업은 상수시간이 걸리므로 상관없지만,
            // Queue의 경우 첫번째 자리를 꺼내면 나머지 숫자들을 한칸씩 앞으로 당겨야 하므로 비효율적
            // 실제로는 순환 버퍼를 사용해서 구현

            // 또 , 추상적으로 사용할때 Stack과 
            // 예를들어 개발자들끼리 얘기할때 LinkedList로 FIFO를 이용해서 만들거에요~ 보다는
            // Queue를 이용해서 만들거에요~ 라고 말하는게 의사소통에도 도움된다

            // Stack을 게임에 어떻게 응용할 수 있을까?
            // 게임 클리어를 '축하합니다' 라는 완료 팝업이 뜨고 거기서 구매 버튼을 누르면 그 위에 팝업이 다시 뜨게 됨
            // 이렇게 중첩된 팝업이 뜰때 Stack을 이용해 마지막에 띄운 팝업부터 순차적으로 끄는데 사용할 수 있다

            // Queue를 
            // 온라인 게임을 만들면 네트워크 패킷을 보내게 되는데
            // 어떤 이용자가 옆에있는 몬스터를 때리고 싶다는 요청을 보낸다면
            // 수만명의 유저가 동시에 보낼텐데 동시에 실행하기 어려운 경우에는 큐를 만들고 줄을 세워서 패킷이 들어오는 순서대로 실행
        }
    }
}
