//using UnityEngine;
//using UnityEngine.Events;

//namespace BW.GameCode.UI
//{
//    [System.Serializable]
//    public class RadioEvent : UnityEvent<int> { }

//    /// <summary>
//    /// UI 单选框
//    /// </summary>
//    [SelectionBase]
//    public class BWRadio : BWToggleGroup
//    {
//        [Header("允许取消选择,可以不选择任何物体,默认没有任何选择,一旦选中一个,旧无法再变成0选")]
//        [SerializeField] bool m_allowSwitchOff = true;
//        [SerializeField] RadioEvent m_valueChangedEvent = new RadioEvent();
//        /// <summary>
//        /// int will be -1 while select nothing
//        /// </summary>
//        public RadioEvent OnValueChanged => m_valueChangedEvent;
//        public bool AllowSwitchOff { get => m_allowSwitchOff; set => m_allowSwitchOff = value; }

//        BWToggle mSelected;

//        // 同步子选项状态
//        bool mValidate = true;
//        /// <summary>
//        /// 选中的索引
//        /// </summary>
//        public int Value => GetIndexOf(mSelected);

//        public void SelectFirstAviable() {
//            foreach (var item in this) {
//                if (item.interactable) {
//                    item.IsOn = true;
//                    //Select(item);
//                    break;
//                }
//            }
//        }

//        protected override void OnRemoveItem(BWToggle removedOne) {
//            base.OnRemoveItem(removedOne);
//            if (removedOne == mSelected) {
//                mSelected.IsOn = false;
//                mSelected = null;
//            }
//        }

//        public void ClearSelected() {
//            if (mSelected != null) {
//                mValidate = false;
//                mSelected.IsOn = false;
//                mSelected = null;
//                mValidate = true;
//            }
//        }

//        protected override void OnItemValueChanged(BWToggle item, bool isOn) {
//            if (item == null) return;
//            if (!mValidate) return;
//            if (isOn) {
//                // 切换选项
//                if (mSelected != null && mSelected != item) {
//                    // 选择了新的项目
//                    var old = mSelected;
//                    mSelected = item;           // 这行和下一行注意顺序
//                    old.IsOn = false;           // 辞旧迎新
//                    OnSelectItem(mSelected);
//                    //Debug.LogWarning($"Select change from {old.name} to {mSelected.name}");
//                    return;
//                } else if (mSelected == null) {
//                    mSelected = item;
//                    OnSelectItem(mSelected);
//                    //Debug.LogWarning($"第一次选择了一个对象{mSelected.name}");
//                    return;
//                }
//            } else {
//                // 取消选项
//                if (mSelected != null && mSelected == item) {
//                    if (AllowSwitchOff) {
//                        Debug.LogWarning($"清空选择{mSelected.name}");
//                        mSelected = null;
//                        OnSelectItem(mSelected);
//                    } else {
//                        Debug.LogWarning($"你必须选择一项");
//                        mSelected.IsOn = true;
//                        OnSelectItem(mSelected);
//                    }
//                }
//            }
//        }

//        private void OnSelectItem(BWToggle toggle) {
//            OnSelectedChanged(toggle);
//            m_valueChangedEvent?.Invoke(Value);
//        }

//        protected sealed override void OnItemClicked(BWToggle item) {
//            base.OnItemClicked(item);
//            item.IsOn = !item.IsOn;
//        }

//        protected virtual void OnSelectedChanged(BWToggle item) {
//        }
//    }
//}