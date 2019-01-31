# 基于深度学习的CIFAR-10 图片分类

> 作者： 赵文亮
>
> 班级： 自64
>
> 学号： 2016011452
>
> 邮箱： zhaowl16@mails.tsinghua.edu.cn

**本次实验中使用了三个模型（LSUV、ResNet、AllConv），关于模型的内容详见实验报告**

## 目录结构

- LSUV/

  - LSUV.ipynb: 主程序

  - lsuv_init.py：LSUV初始化函数

  - model/LSUV_model.h5: 训练完毕的模型

  - Graph/：log 目录（可使用tensorboard打开）

    ```bash
    tensorboard --logdir=Graph --port=9999
    ```

- ResNet/

  - ResNet.ipynb：主程序
  - model/ResNet_model.h5：训练完毕的模型
  - Graph/：log 目录（可使用tensorboard打开）

- AllConv/

  - AllConv.ipynb：主程序
  - model/AllConv_model.h5：训练完毕的模型
  - Graph/：log 目录（可使用tensorboard打开）

- report.pdf：实验报告

