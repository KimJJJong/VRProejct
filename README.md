![image](https://github.com/user-attachments/assets/8c0eb907-729c-4326-b46e-56d2fc2540f6)

📌 Git Branch 전략 (Git Flow)
main 브랜치: 최종 배포 버전. 직접적으로 코드 Push 금지.

develop 브랜치: 통합 테스트 버전. 각 기능 브랜치가 Merge되는 브랜치.

feature/[기능명] 브랜치: 새로운 기능 개발. 각 팀원이 자신의 작업 영역에 따라 분리하여 작업.

예: feature/VRInteraction, feature/LevelDesign, feature/ManagerSystem

hotfix/[버그명] 브랜치: 긴급 버그 수정 (main 브랜치에서 파생).

🔧 Branch 작업 규칙

각 팀원은 자신에게 할당된 기능을 feature/ 브랜치에서 작업합니다.

작업 완료 후 develop 브랜치로 PR(Pull Request)을 생성합니다.

PR 생성 시 팀원들이 코드 리뷰를 진행하며, 2인 이상 최종 승인 후 Merge.

모든 작업은 PR로 Merge하며, 직접 Push는 지양합니다.

📌 작업 프로토콜
코드 스타일 가이드: Unity C# 코드 스타일 (PascalCase, 4 Space Indentation)

커밋 메시지 규칙:

feat: [기능] VR 상호작용 추가

fix: [버그] Grab 동작 오류 수정

docs: [문서] ReadMe 파일 수정

refactor: [리팩토링] 코드 구조 개선

PR(Pull Request) 규칙:

모든 PR은 코드 리뷰를 통해 Merge지향.


📌 프로젝트 파일 구조

📂 VR-Interaction-Project/
├── 📂 Assets/
│   ├── 📂 Scripts/          # 코드 파일 (Manager, VR Interact)
│   ├── 📂 Prefabs/          # VR 오브젝트 프리팹
│   ├── 📂 Scenes/           # 레벨 디자인 및 맵 배치
│   └── 📂 Materials/        # VR 오브젝트 소재
├── 📂 ProjectSettings/
├── 📂 Packages/
└── README.md                # 프로젝트 개요 및 협업 규칙
📌 협업 워크플로우 (Manager 프로그래머 기준)
팀원 브랜치 확인 (feature/[기능명] 브랜치 생성)

Pull Request(PR) 확인

코드 리뷰 진행 (코드 스타일, 로직, 최적화 확인)

필요한 경우 피드백 요청

승인 후 develop 브랜치로 Merge

일정 주기 (주 1회)로 develop에서 최종 테스트 후 main 브랜치로 Merge

