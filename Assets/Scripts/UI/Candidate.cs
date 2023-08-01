using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class Candidate : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static List<Candidate> All;

        public int Id;
        public TMP_Text Text;
        private Canvas _canvas;
        private RectTransform _transform;
        private Vector3 _originalPosition;

        private void Awake()
        {
            All ??= new List<Candidate>();
            All.Add(this);
            _canvas = transform.root.GetComponent<Canvas>();
            _transform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            StartCoroutine(SaveOriginalPosition());
        }

        private IEnumerator SaveOriginalPosition()
        {
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            _originalPosition = _transform.anchoredPosition;
        }

        public void Init(int id, string content)
        {
            Id = id;
            Text.text = content;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _transform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            foreach (var item in All.Where(item => item != this).Where(item => item.CheckDrop(_transform.anchoredPosition)))
            {
                (item.Id, Id) = (Id, item.Id);
                (_transform.anchoredPosition, item._transform.anchoredPosition) = (item._originalPosition,
                    _originalPosition);

                (_originalPosition, item._originalPosition) = (item._originalPosition, _originalPosition);

                return;
            }

            _transform.anchoredPosition = _originalPosition;
        }

        public bool CheckDrop(Vector2 position)
        {
            var rect = _transform.rect;
            var slotPosition = _transform.anchoredPosition;
            return position.x > slotPosition.x - rect.width / 2
                   && position.x < slotPosition.x + rect.width / 2
                   && position.y > slotPosition.y - rect.height / 2
                   && position.y < slotPosition.y + rect.height / 2;
        }

        private void OnDisable()
        {
            Id = -1;
        }

    }
}