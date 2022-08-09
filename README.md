﻿# YSAutoPlayer

XX 游戏弹琴机

## 内容列表

- [使用说明](#使用说明)
- [曲谱格式](#曲谱格式)
  - [文件格式](#文件格式)
  - [音符说明](#音符说明)
  - [示例](#示例)
- [相关仓库](#相关仓库)

## 使用说明

先安装[.NET 6 桌面运行时](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.7-windows-x64-installer)

将曲谱放到 MusicScores 文件夹下, 重新打开程序会自动识别, 或者点击 refresh 按钮识别

弹奏键为<kbd>F8</kbd>, 按下开始弹奏, 再次按下可停止弹奏

> 记得使用管理员权限运行, 不然会无效
>
> 请勿在弹琴以外的界面按下弹奏键, 键盘会乱按导致预期外的后果

## 曲谱格式

曲谱文件类型为文本文件类型

### 文件格式

```
<标题>
<每分钟拍数>
<音轨数>
<音轨1音符>,<音轨1音符>,<音轨1音符>...
<音轨2音符>,<音轨2音符>,<音轨2音符>...
<音轨3音符>,<音轨3音符>,<音轨3音符>...
```

为了避免音轨过长, 允许换行, 格式如下

```
<标题>
<每分钟拍数>
<音轨数>
<音轨1音符>,<音轨1音符>,<音轨1音符>...
<音轨2音符>,<音轨2音符>,<音轨2音符>...
<音轨3音符>,<音轨3音符>,<音轨3音符>...
<音轨1音符>,<音轨1音符>,<音轨1音符>...
<音轨2音符>,<音轨2音符>,<音轨2音符>...
<音轨3音符>,<音轨3音符>,<音轨3音符>...
```

每个音符之间使用 `,` 分隔, 行尾不可包含 `,`

### 音符说明

`<音阶>[<音高>][<持续拍数>]`
|项目|必须|值范围|
|---|---|---|
|音阶|是|`0`, `1`, `2`, `3`, `4`, `5`, `6`, `7`|
|音高|否|"+": 高音, "-": 低音, 不标注为中音|
|持续拍数|否|任意正数, 不标注默认为 1, 表示 1 拍|

### 音轨说明

音轨间音符的同步需要使用 `0` 音符来进行控制

### 示例

#### 单音轨

```
小星星
150
1
1, 1, 5, 5, 6, 6, 52
4, 4, 3, 3, 2, 2, 12
5, 5, 4, 4, 3, 3, 22
5, 5, 4, 4, 3, 3, 22
1, 1, 5, 5, 6, 6, 52
4, 4, 3, 3, 2, 2, 12
```

```
让风告诉你
120
1
00.5,3+0.5,2+0.5,1+0.5
1+0.5,50.5,1+0.5,50.5,1+,2+
1+0.5,2+0.5,3+0.5,4+0.5,3+,2+0.5,1+0.5
1+,5+,1+,5+
1+,6,50.5,3+0.5,2+0.5,1+0.5
1+0.5,50.5,1+0.5,50.5,1+,2+
1+0.5,2+0.5,3+0.5,4+0.5,3+,4+0.5,3+0.5
1+,5+,1+,5+
1+,2+,1+2
```

#### 多音轨

```
友谊地久天长
104
2
5-,12,1,12,3,22,1,22,3,12,1,32,5,65
024
6,52,3,32,1,22,1,22,3,12,6-,6-2,5.,15
024
6,52,3,32,1,22,1,22,6,52,3,32,5,65
4,32,1,12,6-,7.2,6-,7.2,5.,32,1,12,3,45
1`,52,3,32,1,22,1,22,3,12,6,62,5-,15
1,32,1,12,6.,7-2,6.,7-2,5-,12,6,62,5-,15
```

## 相关仓库
- [YSAutoPlayer.MusicScores](https://github.com/fissssssh/YSAutoPlayer.MusicScores) — 弹琴机曲谱仓库
