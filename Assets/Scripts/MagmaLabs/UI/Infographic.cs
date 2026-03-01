using System;

using UnityEngine;

using MagmaLabs;

namespace MagmaLabs.UI{

    public interface IInfographic
    {
        void SetValue(float value);
        void SetRange(float min, float max);                      

        void Refresh();                             

        float currentValue { get; }
        Range<float> valueRange { get; }


    }


    public interface IInfographicEnhanced : IInfographic
    {
        void SetColor(Color color);                 
        void SetAnimationCurve(AnimationCurve curve); 
        void AnimateToValue(float targetValue, float duration); 
        void StopAnimation();
        void SetPrecision(int decimals);
        event Action<float> OnValueChanged;
        event Action OnAnimationComplete;

    }
    [System.Serializable]
    public abstract class InfographicBase : MonoBehaviour, IInfographic
    {
        public virtual float currentValue { get; protected set; }
        public virtual Range<float> valueRange { get; protected set; }

        public virtual void SetValue(float value)
        {
            currentValue = value;
            Refresh();
        }

        public virtual void SetRange(float min, float max)
        {
            valueRange = new Range<float>() { min = min, max = max };
        }

        public virtual float GetPercentage()
        {
            if (valueRange.max - valueRange.min == 0) return 0f;
            return (currentValue - valueRange.min) / (valueRange.max - valueRange.min);
        }
        public virtual void Refresh() { }

    }
    [System.Serializable]
    public abstract class InfographicBaseEnhanced : InfographicBase, IInfographicEnhanced
    {
        public virtual AnimationCurve animationCurve{get; protected set;}
        public int precision { get; protected set; }
        
        public event Action<float> OnValueChanged;
        public event Action OnAnimationComplete;

        public virtual void SetColor(Color color) { }
        public virtual void SetAnimationCurve(AnimationCurve curve)
        {
            animationCurve = curve;
        }
        public virtual void AnimateToValue(float targetValue, float duration) { }
        public virtual void StopAnimation() { }
        public virtual void SetPrecision(int decimals)
        {
            precision = decimals;
        }

    }
}
