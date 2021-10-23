using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch2_1_Array_LinkedList
{
    class Board   // 맵 정보 관리 클래스
    {
        // 정보를 어떻게 들고있어야 하는가?
        // 배열로 가지고 있는 경우
        public int[] _data = new int[25];
        // 동적 배열로 가지고 있는 경우
        public List<int> _data2 = new List<int>;
        // LinkedList 로 가지고 있는 경우
        public LinkedList<int> _data3 = new LinkedList<int>();

        // 프로그래밍은 데이터를 저장하고 잘 활용하는것

        public void Initialize()  // 맵 정보 초기화
        {

        }
    }
}
