# 2D 勇士传说（2D Adventure Demo）

一款用 Unity 独立完成的 2D 动作游戏，从玩法代码、资源管理到打包发布都由我一个人完成。开发周期约 9 个月，30+ 个脚本、5 个场景，包含完整的角色控制与战斗、敌人 AI、存档、场景切换和 UI 流程，已打包 Windows 版。

- 演示视频：https://www.bilibili.com/video/BV18SeF6BEGX
- 开发环境：Unity 2022.3 LTS + C#（URP 2D）


## 技术栈

- 引擎：Unity 2022.3 LTS（URP 2D）
- 输入：Input System（新版）
- 资源与场景：Addressables
- 相机：Cinemachine（Confiner2D）
- UI 与表现：UGUI、DOTween
- 架构：ScriptableObject 事件驱动、状态机、接口驱动

## 核心功能

### 角色与战斗

- 移动、跳跃、攻击使用新版 Input System，包含受击击退、无敌帧、死亡和落水判定
- 受伤动画的结束时机用 StateMachineBehaviour 同步；按是否着地切换物理材质，解决贴墙摩擦

### 敌人 AI

- 野猪敌人用状态机在巡逻和追击之间切换（抽象基类 + 两个状态类）
- 用 Physics2D.BoxCast 配合 LayerMask 检测玩家，带巡逻等待和追击目标丢失计时

### 场景系统

- 主菜单、游戏场景、附加场景用 Addressables 加法加载
- 传送门切换场景，带淡入淡出过渡

### 存档系统

- 需要存档的对象实现 ISaveable 接口，注册到 DataManager 统一管理
- 存档点保存场景、位置、血量，读档时恢复

### UI 与交互

- 血条（UGUI）做成延迟掉血效果；游戏结束和暂停面板用 Time.timeScale 控制
- 存档点、传送门、宝箱等交互物统一实现 IInteractable 接口

## 架构设计

角色、UI、场景、音频这些模块之间不直接互相引用，统一通过 6 个 ScriptableObject 事件资产通信，订阅和退订在 OnEnable / OnDisable 里成对写。

## 遇到并解决的问题

- **Addressables 场景卸载失败**：用 Addressables 卸载已加载场景不生效，最后改成保留 Scene 实例、用 SceneManager 卸载解决。
- **Cinemachine 跨场景相机不跟随**：切场景后 Confiner2D 的边界引用失效，相机不跟随、边界限制失效。通过监听场景加载完成事件、延迟一帧重新绑定新场景边界解决。
- **角色血量未初始化被秒杀**：出生时血量还没初始化就被伤害逻辑读取，导致一进场就死亡，梳理初始化顺序后解决。
- **贴墙摩擦**：角色贴墙移动时卡住，按是否着地切换物理材质解决。

## 操作说明

| 操作 | 按键 |
| --- | --- |
| 移动 | A / D（或方向键） |
| 跳跃 | 空格 |
| 攻击 | J |
| 交互 | E |
| 存档 | L |

## 如何运行

1. 克隆仓库到本地
2. 用 Unity 2022.3 LTS 打开项目（首次打开需等待资源导入）
3. 打开 `Assets/Scenes/Initialization.unity`
4. 点击 Play
