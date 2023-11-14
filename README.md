## SpartaDungeon


### 구성 요소


1. Json Save/Load
   + Json 파일로 아이템과 플레이어 데이터를 관리


2. 역할 분담
   + 매니저들을 관리하는 Manager 클래스
   + 클래스는 어떤 목적을 하나만 지녀야하는 원칙 준수
   + Manager_UI : 콘솔의 UI를 담당 (텍스트 정렬, 사각형 그리기 등)
   + Manager_Resource : 리소스(.txt / .json)파일 로드 및 관리
   + Manager_Scene : Scene형태로 관리 되는 GameNode 기반 클래스들 관리
   + Manager_Data : 리소스의 .json파일을 파싱 및 관리 / 저장 및 불러오기

  
3. 아이템 장착 및 해제
   + Inventory Class와 Player Class로 직렬화된 아이템들 상호작용

  
4. 비동기 (Async Update)
   + Scene_Title에서 비동기적으로 실행되는 네온사인 타이틀
   + 많은 입력을 진행할 시 화면깜박임 발생 - !!(더블 버퍼링으로 해결해야함)
  

5. 상점 페이지 기능
   + 특정 요소 수가 넘어가면 페이지처럼 형식으로 테이블 구성





>추가적으로
  >>해당 프로젝트는 프레임워크가 잘못 구성되었다 판단.
>  >
>  >
>  >새 리포지토리를 이용 새로 프레임워크를 구성할 예정
>  >[Repository](https://github.com/iamdeveloperz/ConsoleDungeon)
>  >


> 구성해야할 목록
* 더블버퍼링 (char[,] frontBuffer / char[,] backBuffer)를 사용한 렌더링 구현
* GameNode (Start, Update, Render)를 기반으로 하는 추상 클래스
* Scene은 GameNode를 상속받아 foreach를 통해 자기가 실행해야 할 Update들을 반복
  + 예시) MainGame Scene에서 GameNode를 지닌 Shop class와 dungeon 클래스가 있다면,
  + Scene이 지니고 있는 GameNode 리스트에 추가 및 지속적인 업데이트
* 더블버퍼링을 구현하기 위해서 비동기적(Async) 타이머를 사용하여 Render()함수 실행 - 콘솔(30fps 제한)
* Scene : GameNode 클래스가 지닌 리스트를 잘 관리해야한다. (필요없는 Update, Render가 발생하지 않게)
* 이를 이용해서 보다 효율적인 UIManager 사용이 가능. (생각해야 할 것 : 더티플래그 또는 출력 최적화)
