using System;
using System.Threading.Tasks;
using Primise4CSharp;

namespace Primise4CSharp_Example
{
    class Program
    {
        static async Task Main (string [] args)
        {
            Promise promise1 = new Promise ();
            Promise promise2 = new Promise ();
            Promise<string> promise3 = new Promise<string> ();

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

            promise3
                .Then ((v) =>
                {
                    Console.WriteLine (v);
                });

            promise3.Catch ((ex) =>
                {
                    Console.WriteLine ($"error:{ex}");
                });

            PromiseHelper.All (new IPromise [3] { promise1 , promise2 , promise3.Then (default) })
                .Then (() =>
                {
                    Console.WriteLine ("All Solved");
                });

            PromiseHelper.Any (new Promise [2] { promise1 , promise2 })
                .Then (() =>
                {
                    //invoke on any of the promise resolved
                });

            promise1.Resolve ();
            await Task.Delay (1000);
            promise2.Resolve ();
            await Task.Delay (1000);
            promise3.Resolve ("Step:04");
            Console.ReadLine ();
        }
    }
}