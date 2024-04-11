//using System.Collections.Generic;
//using System.Linq;

//using UnityEngine;
//using UnityEngine.Events;

//namespace BW.GameCode.UI
//{
//    /// <summary>
//    /// 一个支持多选的按钮组
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public class BWCheckBox : BWToggleGroup
//    {
//        [SerializeField] int m_maxSelectCount = 1;
//        [SerializeField] UnityEvent m_onChangedEvent;

//        public UnityEvent OnSelectedChanged => m_onChangedEvent;

//        /// <summary>
//        /// 返回选中的索引
//        /// </summary>
//        public IEnumerable<int> Value {
//            get {
//                foreach (var item in this) {
//                    if (item.IsOn) {
//                        yield return GetIndexOf(item);
//                    }
//                }
//            }
//        }

//        public int SelectedCount => Value.Count();

//        protected override void OnItemValueChanged(BWToggle item, bool isOn) {
//            m_onChangedEvent?.Invoke();
//        }

//    }
//}