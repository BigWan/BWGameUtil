using UnityEngine;

namespace BW.GameCode.UI
{
    /// <summary>
    /// 切换功能的东西,扩展为按钮,也能响应点击事件
    /// </summary>
    [DisallowMultipleComponent]
    public class BWToggle : BWButton
    {
        [SerializeField] BoolEvent m_onValueChanged = new BoolEvent();
        [Tooltip("运行时调整这个没有任何作用")]
        [SerializeField] bool m_IsOn = false;

        public BoolEvent Event_OnValueChanged => m_onValueChanged;

        public bool IsOn {
            get => m_IsOn;
            set {
                m_IsOn = value;
                OnValueChanged(value);
                Event_OnValueChanged?.Invoke(value);
            }
        }



        protected virtual void OnValueChanged(bool isSelected) {
        }
    }
}