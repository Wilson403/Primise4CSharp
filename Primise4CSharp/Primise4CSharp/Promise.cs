﻿using System;

namespace Primise4CSharp
{
    public interface IPromise
    {
        public IPromise Then (Action onResolved);
        public IPromise<T> Then<T> (Func<T> predicate);
        public IPromise Then (Func<Promise> predicate);
        IPromise<T> Then<T> (Func<Promise<T>> predicate);
    }

    public class Promise : IPromise
    {
        private bool _isResolved;
        private Action _onResolve;

        private void AddAction (Action onResolved)
        {
            if ( _isResolved )
            {
                onResolved?.Invoke ();
            }
            else
            {
                _onResolve += onResolved;
            }
        }

        public IPromise Then (Action onResolved)
        {
            Promise promise = new Promise ();
            AddAction (() =>
            {
                onResolved?.Invoke ();
                promise.Resolve ();
            });
            return promise;
        }

        public IPromise<PromiseT> Then<PromiseT> (Func<PromiseT> predicate)
        {
            Promise<PromiseT> promise = new Promise<PromiseT> ();
            AddAction (() =>
            {
                if ( predicate != null )
                {
                    promise.Resolve (predicate.Invoke ());
                }
                else
                {
                    promise.Resolve (default);
                }
            });
            return promise;
        }

        public IPromise Then (Func<Promise> predicate)
        {
            Promise promise = new Promise ();
            AddAction (() =>
            {
                if ( predicate == null )
                {
                    promise.Resolve ();
                    return;
                }

                IPromise param = predicate.Invoke ();
                if ( param == null )
                {
                    promise.Resolve ();
                    return;
                }

                param.Then (promise.Resolve);
            });
            return promise;
        }

        public IPromise<T> Then<T> (Func<Promise<T>> predicate)
        {
            Promise<T> promise = new Promise<T> ();
            AddAction (() =>
            {
                if ( predicate == null )
                {
                    promise.Resolve (default);
                    return;
                }

                Promise<T> param = predicate.Invoke ();
                if ( param == null )
                {
                    promise.Resolve (default);
                    return;
                }

                param.Then (promise.Resolve);
            });
            return promise;
        }

        /// <summary>
        /// 解决
        /// </summary>
        public void Resolve ()
        {
            if ( _isResolved )
            {
                return;
            }

            _isResolved = true;
            _onResolve?.Invoke ();
            _onResolve = null;
        }
    }
}