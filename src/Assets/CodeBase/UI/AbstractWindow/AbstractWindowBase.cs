using System;
using CodeBase.Animations;
using UniRx;
using UnityEngine;

namespace CodeBase.UI.AbstractWindow
{
    public abstract class AbstractWindowBase : MonoBehaviour
    {
        [SerializeField] protected CanvasAnimator CanvasAnimator;
        
        private readonly Subject<Unit> _onOpenStarted = new();
        private readonly Subject<Unit> _onOpened= new();

        public IObservable<Unit> OnOpenStartedEvent => _onOpenStarted;
        
        public IObservable<Unit> OnOpened => _onOpened;

        protected virtual void Awake()
        {
            if(CanvasAnimator == null)
                CanvasAnimator = GetComponent<CanvasAnimator>();

            OnAwake();
        }

        public void Open(Action onOpened = null)
        {
            _onOpenStarted?.OnNext(Unit.Default);
            
            OnOpenStarted();
            
            CanvasAnimator.Show(() => MarkOpened(onOpened));
        }

        public void Close(Action onClosed = null)
        {
            CanvasAnimator.Hide(() => MarkClosed(onClosed));
        }

        protected virtual void OnAwake()
        {
        }

        protected virtual void OnOpenStarted()
        {
        }

        protected virtual void OnClose()
        {
        }

        protected virtual void OnOpen()
        {
        }

        private void MarkOpened(Action onOpened)
        {
            OnOpen();
            onOpened?.Invoke();
            _onOpened?.OnNext(default);
        }

        private void MarkClosed(Action onClosed)
        {
            OnClose();
            onClosed?.Invoke();
            Destroy(gameObject);
        }
    }
}