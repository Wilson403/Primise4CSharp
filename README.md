#### Overview
Promise implemented in C# for use as an asynchronous programming solution for Unity

---

#### Project Descriptions

Primise4CSharp：Promise core code

Primise4CSharp-Example：demo

---

#### Implemented functions

Then,Catch,Resolve,Reject,All,Any

---

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

