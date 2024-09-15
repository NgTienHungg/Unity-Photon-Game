﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BaseSource
{
    public static class Utils
    {
        private static PointerEventData eventDataCurrentPosition;
        private static List<RaycastResult> results;

        public static bool IsMouseOverUI()
        {
            eventDataCurrentPosition = new PointerEventData(EventSystem.current)
            {
                position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
            };
            results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

        #region === CAMERA ===
        private static Camera mainCam;

        private static Camera MainCam
        {
            get
            {
                if (mainCam == null)
                {
                    mainCam = Camera.main;
                }

                return mainCam;
            }
        }

        public static Vector3 GetMouseWorldPosition()
        {
            var mousePos = Input.mousePosition;
            var mouseWorldPos = MainCam.ScreenToWorldPoint(mousePos); // z = -10
            return new Vector3(mouseWorldPos.x, mouseWorldPos.y);
        }

        public static Vector3 WorldToScreenPoint(Vector3 worldPos)
        {
            return MainCam.WorldToScreenPoint(worldPos);
        }
        #endregion
    }
}