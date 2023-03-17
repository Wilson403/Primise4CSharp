#### 概叙
使用C#语言来实现的Promise，适用于作为Unity的异步编程方案



#### 工程架构

Primise4CSharp：Promise核心代码

Primise4CSharp-Example：演示案例工程



#### 已实现的接口

Then,Catch,Resolve,Reject,All,Any



#### How to use it

```c#
promise1
        .Then (() =>
        {
            Console.WriteLine ("Step:01");
        })
        .Then (() =>
        {
            return "Step:02";
        })
        .Then ((v) =>
        {
            Console.WriteLine (v);
        })
        .Then (() =>
        {
            return promise2;
        })
        .Then (() =>
        {
            Console.WriteLine ("Step:03");
        })
        .Then (() =>
        {
            return promise3;
        });
```

