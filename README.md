![image](https://github.com/user-attachments/assets/8c0eb907-729c-4326-b46e-56d2fc2540f6)
# VR-Interaction-Project

## 📌 프로젝트 소개

* **프로젝트명:** VR Interaction Project
* **프로젝트 설명:** VR 기반 상호작용 프로젝트로, 유저가 다양한 VR 오브젝트와 상호작용할 수 있는 시스템을 개발하는 것을 목표로 합니다.

---

## 📌 팀원 구성 및 역할

* **Manager 프로그래머 (1명):**

  * 게임 Manager 구현
  * 프로젝트 구조 설계 및 Git 브랜치 관리
  * 코드 리뷰 및 PR(Pull Request) 관리
  * 최종 빌드 및 프로젝트 통합

* **VR Interact 프로그래머 (1명):**

  * VR 상호작용 구현 (XR Interaction Toolkit 활용)
  * VR 오브젝트 상호작용 로직 (Raycast, Grab, UI Interaction 등)
  * Player 및 XR 설정 관리

* **맵 디자이너 (1명):**

  * 맵 배치 및 레벨 디자인
  * 상호작용 기획 및 사용자 경험(UX) 설계

---

## 📌 Git Branch 전략 (Git Flow)

* **`main` 브랜치:** 최종 배포 버전. 직접적으로 코드 Push 금지.
* **`develop` 브랜치:** 통합 테스트 버전. 각 기능 브랜치가 Merge되는 브랜치.
* **`feature/[기능명]` 브랜치:** 새로운 기능 개발. 각 팀원이 자신의 작업 영역에 따라 분리하여 작업.

  * 예: `feature/VRInteraction`, `feature/LevelDesign`, `feature/ManagerSystem`
* **`hotfix/[버그명]` 브랜치:** 긴급 버그 수정 (main 브랜치에서 파생).

### 🔧 Branch 작업 규칙

1. 각 팀원은 자신에게 할당된 기능을 `feature/` 브랜치에서 작업합니다.
2. 작업 완료 후 `develop` 브랜치로 PR(Pull Request)을 생성합니다.
3. PR 생성 시 팀원들이 코드 리뷰를 진행하며, 2인 이상 최종 승인 후 Merge.
4. 모든 작업은 PR로 Merge하며, 직접 Push는 지양합니다.

---

## 📌 작업 프로토콜


* **커밋 메시지 규칙:**

  * `feat: [기능] VR 상호작용 추가`
  * `fix: [버그] Grab 동작 오류 수정`
  * `docs: [문서] ReadMe 파일 수정`
  * `refactor: [리팩토링] 코드 구조 개선`

* **PR(Pull Request) 규칙:**

  * 모든 PR은 코드 리뷰를 통해 Merge지향.
  * 코드 리뷰 후 2인 이상의 최종 승인 필수.

---

## 📌 프로젝트 파일 구조

```
📂 VR-Interaction-Project/
├── 📂 Assets/
│    └── 📂 VR_Proejct/
│         ├── 📂 Scripts/          # 코드 파일 (Manager, VR Interact)
│         ├── 📂 Prefabs/          # VR 오브젝트 프리팹
│         ├── 📂 Scenes/           # 레벨 디자인 및 맵 배치
│         └── 📂 Materials/        # VR 오브젝트 소재
├── 📂 ProjectSettings/
├── 📂 Packages/
└── README.md                # 프로젝트 개요 및 협업 규칙
```

---

## 📌 협업 워크플로우 (Manager 프로그래머 기준)

1. 팀원 브랜치 확인 (feature/\[기능명] 브랜치 생성)
2. Pull Request(PR) 확인
3. 코드 리뷰 진행 (코드 스타일, 로직, 최적화 확인)
4. 필요한 경우 피드백 요청
5. 승인 후 develop 브랜치로 Merge
6. 일정 주기 (주 1회)로 develop에서 최종 테스트 후 main 브랜치로 Merge
