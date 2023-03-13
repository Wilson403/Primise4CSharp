using System;

namespace Primise4CSharp
{
    public class Promise
    {
        private bool _isResolved;
        private Action _onResolve;

        private void AddAction (Action action)
        {
            _onResolve += action;
        }

        public Promise Then (Action action)
        {
            if ( _isResolved )
            {
                action?.Invoke ();
            }
            else 
            {
                AddAction (action);
            }
            return this;
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