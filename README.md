# :deciduous_tree: Argandion : 그리운 나의 숲 :deciduous_tree:

![banner](https://user-images.githubusercontent.com/79687246/203693240-556fbe13-6203-4207-b0fa-2f30c5ecbfc8.png)


<!-- ----------------------- pv 영상 ----------------------- -->

<br/>

## beta 버전 pv
[![Argandion beta pv 1](http://img.youtube.com/vi/npUWMUzc4rA/sddefault.jpg)](https://youtu.be/npUWMUzc4rA?t=0s) 

--- 

> 아르간디온에서 다양한 체험을 하며 여유를 즐겨보세요!


### 'Argandion'는 가상의 숲 아르간디온을 가꿔나가는 3D 힐링 게임입니다.  
### 자체 개발한 오픈월드 맵에서 플레이하며 다양한 힐링요소를 느껴보세요!

<br/>

## Download
[Argandion 다운로드 바로가기](https://drive.google.com/uc?export=download&id=1QuTpq51AGaH3QMUH8K7SEvd-dQseZivy)

<br/>

## 주요기능
- 다양한 방법으로 자원을 획득하고, 숲의 구역을 정화해보세요.
- 정화한 구역에 찾아온 NPC와의 상호작용을 통해 여러 컨텐츠를 즐겨보세요.
- 사계절과 날씨, 발전도로 바뀌는 숲과 세계수, 집을 감상하세요.

## 세부기능

| 구분 | 기능                | 설명                                                         | 비고 |
| ---- | ------------------- | ------------------------------------------------------------ | ---- |
| 1    | 환경설정            | ESC 키를 사용해서 배경음 / 효과음 등 환경설정을 통한 변경이 가능합니다.        |      |
| 2    | 게임 내 정보 제공   | 게임 내에서 미니맵과 현재 시각이 표시되고, 인벤토리와 월드 맵을 통해 정보를 제공합니다. |      |
| 3    | 자원 획득 | 벌목, 채광, 채집, 농사, 목축, 사냥 등의 방법으로 자원 획득이 가능합니다. |      |
| 4    | 게임 내 상호작용    | NPC와의 대화, 거래와 아이템 제작 등 다양한 상호작용이 가능합니다.  |      |
| 5    | 버프       | 음식과 꽃과 제단을 이용하여 다양한 버프를 받을 수 있습니다.       |      |


## 아키텍처
- Game
  - Unity: 2021.3.11f1
- FE
  - React: 16.9.0
- Deploy
  - Nginx: nginx/1.18.0
- Server
  - EC2
- IDE
  - Unity Hub
  - VScode: 1.72.2

## 조작법
- 이동키 : WASD | 방향키
- 상호작용 : F | 우클릭
- 도구사용 : SPACE | 좌클릭
- 인벤토리 : I
- 맵 : M
- 옵션 : ESC
- 장비변경 : Number 1 ~ 7 | Q (좌) | E (우)
- 달리기 : L.shift

## Release Note 
- `2022. 11. 18` | `v.1.0.0 - beta` 
  - `NEW` 
    - 베타 버전 출시

- `2022. 11. 28` | `v.1.1.0 - Release`
  - `NEW`
    - 저장/불러오기 기능 추가
    - 인벤토리에서 마우스 좌클릭 상호작용 기능 추가
    - 인벤토리에서 커서를 올림으로써 아이템 설명 표시 추가
    - 월/일 과 시/분 표시 기능 추가
    - ESC 버튼으로 패널 닫기 기능 추가
    - 침대 상호작용으로 하루를 마무리하는 기능 추가
    - 마우스 커서 아이콘 추가
    - 장비 사용 시 스테미나 감소 추가
  - `FIX`
    - 밸런스 패치 - 전반적인 아이템 가격 조정, 달리기 스태미너 감소율 조정
    - '리데이사의 가호' 아이콘이 정상적으로 사라지지 않는 현상 수정
    - 건물 건설 시 필요한 자원이 정상적으로 표기되지 않는 현상 수정
    - 인벤토리의 아이템이 - 개수로 내려가는 현상 수정
    - 아이템 사용 시 데이터 값이 UI와 동기되지 않는 현상 수정
    - 미끼 사용 시, UI 동기 오류 수정
    - 인벤토리가 가득 찬 상태에서 드랍된 아이템과 접촉 시 정상적으로 동작하지 않는 현상 수정
    - 창고의 아이템이 정상적으로 반입/반출되지 않는 현상 수정
    - 일부 지형의 표면 수정
    - UI 화면의 종료가 비정상적으로 작동하는 현상 수정
    - 일부 누락된 이미지 수정
    - 창고의 아이템 반출 시 이미지가 검게 물드는 현상 수정
    
<br/>

## 리소스 출처

<table style={{fontSize:"16px", marginLeft:"40px", lineHeight:"40px"}}>
    <tr>
        <td>모루</td>
        <td><a href="https://www.flaticon.com/kr/free-icons/" title="모루 아이콘">모루 아이콘 제작자: Freepik - Flaticon</a></td>
        <!-- <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td> -->
        <td>헛간</td>
        <td><a href="https://www.flaticon.com/free-icons/barn" title="barn icons">Barn icons created by Freepik - Flaticon</a></td>
    </tr>
    <tr>
        <td>공방</td>
        <td><a href="https://www.flaticon.com/free-icons/carpentry" title="carpentry icons">Carpentry icons created by berkahicon - Flaticon</a></td>
        <!-- <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td> -->
        <td>발바닥&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        <td><a href="https://www.flaticon.com/free-icons/tiger" title="tiger icons">Tiger icons created by surang - Flaticon</a></td>
    </tr>
    <tr>
        <td>물고기&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        <td><a href="https://www.flaticon.com/free-icons/fish" title="fish icons">Fish icons created by Freepik - Flaticon</a></td>
        <!-- <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td> -->
        <td>튜닉</td>
        <td><a href="https://www.flaticon.com/free-icons/tunic" title="tunic icons">Tunic icons created by Freepik - Flaticon</a></td>
    </tr>
    <tr>
        <td>상의</td>
        <td><a href="https://www.flaticon.com/free-icons/t-shirt" title="t-shirt icons">T-shirt icons created by gariebaldy - Flaticon</a></td>
        <!-- <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td> -->
        <td>하의</td>
        <td><a href="https://www.flaticon.com/free-icons/pants" title="pants icons">Pants icons created by gariebaldy - Flaticon</a></td>
    </tr>
    <tr>
        <td>물방울</td>
        <td><a href="https://www.flaticon.com/free-icons/water" title="water icons">Water icons created by Freepik - Flaticon</a></td>
        <!-- <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td> -->
        <td>물고기</td>
        <td><a href="https://www.flaticon.com/free-icons/fish" title="fish icons">Fish icons created by Freepik - Flaticon</a></td>
    </tr>
    <tr>
        <td>괭이</td>
        <td><a href="https://www.flaticon.com/free-icons/hoe" title="hoe icons">Hoe icons created by Smashicons - Flaticon</a></td>
        <!-- <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td> -->
        <td>낚싯대</td>
        <td><a href="https://www.flaticon.com/free-icons/fishing" title="fishing icons">Fishing icons created by Freepik - Flaticon</a></td>
    </tr>
    <tr>
        <td>엘프</td>
        <td><a href="https://www.flaticon.com/free-icons/elf" title="elf icons">Elf icons created by Freepik - Flaticon</a></td>
        <!-- <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td> -->
        <td>세계수</td>
        <td><a href="https://www.flaticon.com/free-icons/tree" title="tree icons">Tree icons created by Freepik - Flaticon</a></td>
    </tr>
    <tr>
        <td>작물</td>
        <td><a href="https://www.flaticon.com/free-icons/plant" title="plant icons">Plant icons created by Smashicons - Flaticon</a></td>
        <!-- <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td> -->
        <td>집</td>
        <td><a href="https://www.flaticon.com/free-icons/real-estate" title="real estate icons">Real estate icons created by Freepik - Flaticon</a></td>
    </tr>
    <tr>
        <td>제단</td>
        <td><a href="https://www.flaticon.com/free-icons/archaeology" title="archaeology icons">Archaeology icons created by Vitaly Gorbachev - Flaticon</a></td>
        <!-- <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td> -->
        <td>제단 포탈</td>
        <td><a href="https://www.flaticon.com/free-icons/portal" title="portal icons">Portal icons created by Andrejs Kirma - Flaticon</a></td>
    </tr>
    <tr>
        <td>건설 전</td>
        <td><a href="https://www.flaticon.com/free-icons/brick" title="brick icons">Brick icons created by Smashicons - Flaticon</a></td>
        <!-- <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td> -->
        <td></td>
        <td></td>
    </tr>
</table>
