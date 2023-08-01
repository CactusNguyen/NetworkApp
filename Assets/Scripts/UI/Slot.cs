using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class Slot : MonoBehaviour
    {
        public static List<Slot> All;

        private DraggableObject _object;

        private void Start()
        {
            All ??= new List<Slot>();
            All.Add(this);
        }

        public bool CheckDrop(Vector2 position)
        {
            var rectTransform = GetComponent<RectTransform>();
            var rect = rectTransform.rect;
            var slotPosition = rectTransform.anchoredPosition;
            return position.x > slotPosition.x - rect.width / 2
                   && position.x < slotPosition.x + rect.width / 2
                   && position.y > slotPosition.y - rect.height / 2
                   && position.y < slotPosition.y + rect.height / 2;
        }

        public void SetObject(DraggableObject obj)
        {
            if (_object != null)
            {
                if (obj.Slot != null)
                {
                    // Swap
                    obj.Slot._object = _object;
                    _object.Slot = obj.Slot;
                    _object.transform.position = obj.Slot.transform.position;
                    
                    _object = obj;
                    obj.transform.position = transform.position;
                    return;
                }
                
                _object.Reset();
                _object.Slot = null;
                _object = null;
            }

            foreach (var other in All.Where(other => other._object == obj))
            {
                other._object.Reset();
                other._object = null;
            }

            _object = obj;
            _object.transform.position = transform.position;
        }
    }
}