using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch2_1_Array_LinkedList
{
    class MyList<T>   // List 만들기 연습
    {
        const int DEFAULT_SIZE = 1;
        T[] _data = new T[DEFAULT_SIZE];
        public int Count = 0; // 실제로 사용중인 데이터 개수
        public int Capacity { get { return _data.Length; } } // 예약된 데이터 개수

        // O(1) 상수시간복잡도라고 함.
        // 공간이 없을때 늘려주는 부분은 O(N)이지만 데이터가 많아질수록 이 부분은 가끔 실행되므로 Add는 좀 특이하게 이부분을 무시하고 O(1)로 본다
        public void Add(T item)
        {
            // 1. 공간이 충분히 남아 있는지 확인한다
            if (Count >= Capacity)
            {
                // 공간을 다시 늘려서 확보한다
                T[] newArray = new T[Count * 2];
                for (int i = 0; i < Count; i++)
                    newArray[i] = _data[i];
                _data = newArray;
            }

            // 2. 공간에다가 데이터를 넣어준다.
            _data[Count] = item;
            Count++;
        }

        // O(1)
        public T this[int index]   // 인덱서
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }

        // O(N)  최악의 경우를 생각하면 된다
        public void RemoveAt(int index)
        {
            for (int i = index; i < Count - 1; i++)
                _data[i] = _data[i + 1];
            _data[Count - 1] = default(T);
            Count--;
        }
    }

    // LinkedList 연습
    class Room<T>
    {
        public T Data;
        // 방을 가르키는 주소가 있음(참조)
        public Room<T> Next;  // 다음 방의 주소값
        public Room<T> Prev;  // 이전 방의 주소값
    }

    class RoomList<T>
    {
        public Room<T> Head = null; // 첫번째 방. 실시간으로 정보를 갱신해야함
        public Room<T> Tail = null; // 마지막 방. 실시간으로 정보를 갱신해야함
        public int Count = 0;

        //public int* pHead = &Head;

        public Room<T> AddLast(T data)
        {
            Room<T> newRoom = new Room<T>();
            newRoom.Data = data;
            // 만약에 아직 방이 없었다면 새로 추가한 방이 곧 Head이다.
            if (Head == null)
                Head = newRoom;

            // 101 102 103 104
            // 기존 [마지막 방]과 [새로 추가되는 방]을 연결해준다
            if(Tail != null)
            {
                Tail.Next = newRoom;
                newRoom.Prev = Tail;
            }

            // [새로 추가되는 방]을 [마지막 방]으로 지정
            Tail = newRoom;
            Count++;
            return newRoom;
        }

    }

    class Board   // 맵 정보 관리 클래스
    {
        // 정보를 어떻게 들고있어야 하는가?
        
        public int[] _data = new int[25];  // 배열로 가지고 있는 경우
        public List<int> _data2 = new List<int>();  // 동적 배열로 가지고 있는 경우
        public LinkedList<int> _data3 = new LinkedList<int>();  // LinkedList 로 가지고 있는 경우

        // 직접 만든 List 테스트
        public MyList<int> _mydata = new MyList<int>();

        // 직접 만든 LinkedList 테스트
        public RoomList<int> _myLinkedList = new RoomList<int>();

        // 맵은 대부분 고정적인 상태이므로 연결리스트를 사용하는건 딱히 좋은 선택이 아님
        // 배열 사이즈를 유동적으로 늘렸다 줄였다 할 수 있는 장점. 맵은 딱히 사이즈가 변하지 않음
        // 따라서 맵의 경우 배열을 사용하는게 가장 합리적임

        public void Initialize()  // 맵 정보 초기화
        {

        }

        public void List_Initialize()  // 맵 정보 초기화
        {
            _data2.Add(101);
            _data2.Add(102);
            _data2.Add(103);
            _data2.Add(104);
            _data2.Add(105);

            int temp = _data2[2];
            _data2.RemoveAt(2);
        }

        public void MyList_Initialize()
        {
            _mydata.Add(101);
            _mydata.Add(102);
            _mydata.Add(103);
            _mydata.Add(104);
            _mydata.Add(105);

            int temp = _mydata[2];
            _mydata.RemoveAt(2);
        }

        public void LinkedList_Initialize()
        {
            _data3.AddLast(101);
            _data3.AddLast(102);
            _data3.AddLast(103);
            LinkedListNode<int> node = _data3.AddLast(104);
            _data3.AddLast(105);

            _data3.Remove(node);
        }

        public void MyLinkedList_Initialize()
        {
            _myLinkedList.AddLast(101);
            _myLinkedList.AddLast(102);
            _myLinkedList.AddLast(103);
        }
    }
}
