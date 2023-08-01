using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Canvas _canvas;
        private RectTransform _rectTransform;
        private Vector3 _originalPosition;
        [HideInInspector] public Slot Slot;

        private void Awake()
        {
            _canvas = transform.root.GetComponent<Canvas>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            _originalPosition = _rectTransform.position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _rectTransform.SetSiblingIndex(100);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            foreach (var slot in Slot.All.Where(slot => slot.CheckDrop(_rectTransform.anchoredPosition)))
            {
                slot.SetObject(this);
                Slot = slot;
                return;
            }

            if (Slot != null)
                _rectTransform.position = Slot.transform.position;
            else
                Reset();
        }

        public void Reset()
        {
            _rectTransform.position = _originalPosition;
        }

        private void OnDrawGizmos()
        {
            if (_rectTransform != null && Slot != null)
                Gizmos.DrawLine(_rectTransform.position, Slot.transform.position);
        }
    }
}
