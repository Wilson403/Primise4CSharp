using System;

namespace Primise4CSharp
{
    public interface IPromise<PromiseT>
    {
        IPromise<PromiseT> Then (Action<PromiseT> onResolved);

        IPromise<T> Then<T> (Func<T> predicate);

        IPromise<T> Then<T> (Func<Promise<T>> predicate);

        IPromise Then (Func<Promise> predicate);
    }

    public class Promise<PromiseT> : IPromise<PromiseT>
    {
        private bool _isResolved;
        private Action<PromiseT> _onResolve;
        private PromiseT _currentParam;

        private void AddAction (Action<PromiseT> onResolved)
        {
            if ( _isResolved )
            {
                onResolved?.Invoke (_currentParam);
            }
            else
            {
                _onResolve += onResolved;
            }
        }

        public IPromise<PromiseT> Then (Action<PromiseT> onResolved)
        {
            Promise<PromiseT> promise = new Promise<PromiseT> ();
            AddAction ((t) =>
            {
                onResolved?.Invoke (t);
                promise.Resolve (t);
            });
            return promise;
        }

        public IPromise<T> Then<T> (Func<T> predicate)
        {
            Promise<T> promise = new Promise<T> ();
            AddAction ((t) =>
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

        public IPromise<T> Then<T> (Func<Promise<T>> predicate)
        {
            Promise<T> promise = new Promise<T> ();
            AddAction ((t) =>
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

        public IPromise Then (Func<Promise> predicate)
        {
            Promise promise = new Promise ();
            AddAction ((t) =>
            {
                if ( predicate == null )
                {
                    promise.Resolve ();
                    return;
                }

                Promise param = predicate.Invoke ();
                if ( param == null )
                {
                    promise.Resolve ();
                    return;
                }

                param.Then (promise.Resolve);
            });
            return promise;
        }

        /// <summary>
        /// 解决
        /// </summary>
        /// <param name="param"></param>
        public void Resolve (PromiseT @param)
        {
            if ( _isResolved )
            {
                return;
            }

            _isResolved = true;
            _onResolve?.Invoke (param);
            _currentParam = param;
            _onResolve = null;
        }
    }
}