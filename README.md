# 2D Adventure Demo

基于 Unity 开发的 2D 动作游戏个人项目。

## 技术栈
- Unity 2022.3
- C#
- Addressables（场景动态加载与卸载）
- Cinemachine（相机跟随与边界限制）
- ScriptableObject（事件驱动架构）

## 核心功能
- 多场景动态加载与卸载，支持传送门跨场景切换
- 角色战斗、受伤、受击无敌时间、血量系统
- 场景与角色位置的存档 / 读档
- 相机跟随、镜头震动与边界限制

## 遇到并解决的问题
- Addressables 句柄生命周期导致的场景卸载失败
- Cinemachine 跨场景边界失效导致的相机不跟随
- 角色血量未初始化导致的秒杀 Bug

## 演示视频
（等你的B站视频做好后，把链接贴在这里）

## 如何运行
1. 使用 Unity 2022.3 打开项目
2. 打开 `Assets/Scenes/Initialization.unity`
3. 点击 Play
