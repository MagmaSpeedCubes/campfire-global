using UnityEngine;
using UnityEngine.UI;
using MagmaLabs;
namespace MagmaLabs.UI{
    public class WaypointPointer : InfographicBaseEnhanced
    {
        [SerializeField] private Image pointerImage;
        [SerializeField] private Sprite[] firstQuadrantArrows;
        public Transform target;
        public Transform position;

        void Start()
        {
            SetRange(0, 360);
        }

        override public void Refresh()
        {
            float quarterRotation = (float)valueRange.length / 4;
            float angleInQuadrant = currentValue % (quarterRotation);
            int quadrant = Mathf.FloorToInt(currentValue / quarterRotation);
            pointerImage.sprite = firstQuadrantArrows[quadrant];//individual rotated sprites for pixel consistency
            pointerImage.transform.rotation = Quaternion.Euler(0, 0, -quadrant*90);//rotate in intervals of 90 to reuse sprites
            
        }

        override public void SetColor(Color color)
        {
            pointerImage.color = color;
        }

        private void SetTarget(Transform target)
        {
            this.target = target;
        }

        private void SetTrackedPosition(Transform position)
        {
            this.position = position;
        }
        ///FIX THIS LATER SEPARATE THE INFOGRAPHIC FROM THE TRACKING LOGIC

        
    }
}
