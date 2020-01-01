- [项目简介](#项目简介)
  - [特点](#特点)
  - [工程环境与硬件需求](#工程环境与硬件需求)
  - [已知问题](#已知问题)
  - [场景截图](#场景截图)
  - [视频展示](#视频展示)
# 项目简介 #
一个基于 HTC Vive focus 的~~完成度很低的~~VR网球游戏。
## 特点 ##
- 可以通过手柄按键控制虚拟人物在球场上的移动、旋转手柄以控制球拍的姿态变换，完成接球
- 接球判断与计分统计
- 通过粒子系统实现的击球特效
- 游戏内自由切换白昼场景
- 待完善……
## 工程环境与硬件需求 ##
- VS 2015
- Unity 2017.4.25f1
- WaveSDK 3.0.2
- Android Studio bundle
- HTC Vive focus 及 3DoF 手柄
## 已知问题 ##
1. 由于手柄是3自由度的，因此只能实现简单的球拍姿态旋转检测，不能识别球拍的位移，这样也就无法像现实那样挥拍而只能旋转手腕去操纵。
2. 网球的触地反弹高度未能很好地模拟，目前是人为施加了一个反地面向上的力。
3. 由于硬件条件的限制，挥拍极不真实，目前的方案是在碰撞发生时在网球上人为施加了几个方向的力。~~（投机取巧的方法）~~
4. ~~（小概率发生，难复现；疑为Unity的[bug](https://issuetracker.unity3d.com/issues/skybox-field-doesnt-show-the-correct-value-when-working-with-multiple-scenes)，已在2017.4.27f1中修复，未测试）游戏内环境切换即改变天空盒材质后，即便调用了DynamicGI.UpdateEnvironment也无法更新环境纹理。比如，当从夜晚切换到白天时，球场的模型是暗黑而不是明亮的。~~

如果你有好的解决方案，或者对项目其他地方有改进建议，欢迎[邮箱](mailto:iAndrewZhao@outlook.com)联系。^_^

---
## 场景截图 ##
![](https://raw.githubusercontent.com/ixzhao/VR-Tennis-Game/master/Assets/SceenShots/StartGame.jpg)

![](https://raw.githubusercontent.com/ixzhao/VR-Tennis-Game/master/Assets/SceenShots/InGame.jpg)

![](https://raw.githubusercontent.com/ixzhao/VR-Tennis-Game/master/Assets/SceenShots/Menu.jpg)

部分模型借用自：
- [HTCVive-Game-VRTennis](https://github.com/str818/HTCVive-Game-VRTennis)
- [VR-Tennis](https://github.com/me4502/VR-Tennis)
## 视频展示 ##
- 开始界面及UI展示 [YouTube地址](https://www.youtube.com/watch?v=dyyPdwe0XVk)
- 演示 [YouTube地址](https://www.youtube.com/watch?v=OfhCFc3GkFI)
