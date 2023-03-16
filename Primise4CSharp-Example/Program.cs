using System;
using Primise4CSharp;

namespace Primise4CSharp_Example
{
    class Program
    {
        static void Main (string [] args)
        {
            Promise promise = new Promise ();

            Promise<int> promise2 = new Promise<int> ();

            promise
                .Then (() =>
                {
                    Console.WriteLine ("start1");
                })
                .Then (() =>
                {
                    Console.WriteLine ("start2");
                    return 1111;
                })
                .Then ((t) =>
                {
                    Console.WriteLine ($"start3 = {t}");
                })
                .Then (() =>
                {
                    return "HelloWorld";
                })
                .Then ((t) =>
                {
                    Console.WriteLine ($"start4 = {t}");
                })
                .Then (() => 
                {
                    return new Promise<int> ();
                });

            promise.Then (() => 
            {
                return new Promise ();
            });

            promise2.Then (() => 
            {
                return new Promise<int> ();
            });

            promise.Resolve ();
        }
    }
}