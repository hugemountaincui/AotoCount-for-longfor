# Autocount for LongforApp
develop in longfor form 2019.11to 2020 .1

 New update Autocount V2.0.1 2020.1.14

首先感谢你们发现这个小程序，那么就让我做个简短的介绍：

一、软件两个核心功能：转换计算数据和复制进excel。
  准换数据是解析文字后面的数据，我的应用场景是：“10:30总计来访12组，扫码分配12组，竞品1组，外展3组，外拓3组，社区5组 ，专业市场0组，呼叫0组，大客户0组，约访0组，自然到访0组，追电0组，其他0组，中介0组（贝壳0组，房乐乐0组，吉家0组)”。将其中的数字对应的存在一个类里，然后与上个时段数据进行相减，得到变化；其次复制进入excel中就显得十分简单了。简单的\t制表符就可以实现.
  
二、软件的实现过程：string正则运算
  核心代码：
      Match m = Regex.Match(text, "(?<=" + name + @")\d{1,}");
  为简化程序流程，封装成一个函数，实际的表达式为:"(?<=" + "竞品" + @")\d{1,}"，最后的计算结果就是1.最后创建一个类来包含上述的数据，剩下就是简单的过程来实现具体的功能。
  
三、实际过程演示：
  1.两个项目对比结果：
  
  2.两种时段数据的准换：
  
  注：以上的数据都放在test文件夹中以供测试使用。
