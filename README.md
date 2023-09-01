# SpartaDungeonBattle

### 프로그래밍 심화 팀과제

#### data폴더 하위 myItemData.json, playerData.json, shopData.json 파일 모두 필요
#### Json 관련 Newtonsoft.Json 페키지 필요함

##### 구현기능
1. 게임 시작 화면
2. 상태 보기
3. 전투 시작
4. 캐릭터 생성 기능
5. 직업 선택 기능
6. 스킬 기능
7. 치명타 기능
8. 회피 기능
9. 레벨업 기능
10. 보상 추가
11. 콘솔 꾸미기
12. 몬스터 종류 추가해보기
13. 아이템 적용
14. 회복 아이템
15. 스테이지 추가
16. 게임 저장하기

##### 주요기능 설명
Program.cs : 솔루션을 실행하는 Main 메소드(이외 별 기능 없음)
Common.cs : 스파르타 던전 배틀 Text 게임에서 사용되는 공통 기능, 객체를 담당
Battle.cs : 전투 시작, 몬스터 생성, 공격, 방어, 스킬 사용을 담당
CharacterInfo.cs : 직업별 사용 가능 스킬 및 직업 선택시 초기 스텟을 담당
Inventory.cs : 인벤토리, 장착, 아이템 정렬 담당
MyInfo.cs : 내정보 화인 화면 출력 메소드
Portion.cs : 회복 아이템 사용 담당
Shop.cs : 상점 아이템 구매 및 아이템 판매 담당

###### 팀 노션
https://www.notion.so/06-8e70aeedeccb423f90c348b49ab40fa9

##### 과제 노션
https://teamsparta.notion.site/Chapter-2-1-699e6d8907464d049aa63d3bd198d537
