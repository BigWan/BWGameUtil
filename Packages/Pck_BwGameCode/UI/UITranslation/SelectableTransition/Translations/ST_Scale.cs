namespace BW.GameCode.UI
{
#if UNITY_EDITOR
    using UnityEditor;
#endif

    using BW.GameCode.Foundation;

    using UnityEngine;

    using static BW.GameCode.UI.SelectableAnimationController;

    /// <summary>
    /// 缩放
    /// </summary>
    public class ST_Scale : SelectableTransition
    {
        [SerializeField] Transform m_scalePart = default;
        [SerializeField] STValue_Float m_value = STValue_Float.DEFAULT_SCALE_VALUE;
        //[SerializeField] float m_selectScale = 1.1f;

        [SerializeField] float m_animTime = 0.25f;

        SimpleTween_Float runner = new SimpleTween_Float();

        private void Awake() {
            runner.SetCallback((x) => {
                if (m_scalePart != null) {
                    m_scalePart.localScale = Vector3.one * x;
                }
            })

            ;
        }

        internal override void DoStateTransition(SelectableState state, bool instant) {
            if (m_scalePart != null && m_value != null) {
                DOScale(m_value.GetValue(state), instant);
            }
        }

        private void DOScale(float value, bool instant) {
            //m_scalePart.DOKill();
            if (instant) {
                m_scalePart.localScale = value * Vector3.one;
            } else {
                runner.SetStartAndEnd(m_scalePart.localScale.x, value)
                    .SetDuration(m_animTime)
                    .StartTween(this);
            }
        }

        private void OnValidate() {
            if (m_scalePart == null) {
                m_scalePart = this.transform;
            }
        }

#if UNITY_EDITOR

        void SetAll(int value) {
            m_value.Reset(value);
        }

#endif
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(ST_Scale))]
    internal class ST_ScaleEditor : Editor
    {
        ST_Scale prop;

        private void OnEnable() {
            prop = target as ST_Scale;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if (GUILayout.Button("全部设置为1")) {
            }
        }
    }

#endif
}