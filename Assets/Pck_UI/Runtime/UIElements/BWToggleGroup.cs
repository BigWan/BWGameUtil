//using System.Collections;
//using System.Collections.Generic;

//using UnityEngine;
//using UnityEngine.Events;

//namespace BW.GameCode.UI
//{

//    /// <summary>
//    /// 一个Toggle的容器
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public abstract class BWToggleGroup : MonoBehaviour, IReadOnlyList<BWToggle>
//    {
//        [SerializeField] List<BWToggle> mItems = new List<BWToggle>();

//        /// <summary>
//        /// 不存在返回-1;
//        /// </summary>
//        /// <param name="slot"></param>
//        /// <returns></returns>
//        public int GetIndexOf(BWToggle slot) {
//            if (slot == null) return -1;
//            return mItems.IndexOf(slot);
//        }

//        public int Count => mItems.Count;

//        public BWToggle this[int index] => mItems[index];

//        struct ActionPair
//        {
//            public UnityAction click;
//            public UnityAction<bool> change;
//        }

//        readonly Dictionary<BWToggle, ActionPair> mItemActions = new Dictionary<BWToggle, ActionPair>();

//        protected virtual void OnAwake() {
//        }

//        protected void Awake() {
//            foreach (var item in mItems) {
//                RegItemEvent(item);
//            }
//            OnAwake();
//        }

//        void RegItemEvent(BWToggle item) {
//            ActionPair action = new ActionPair() {
//                click = () => OnItemClicked(item),
//                change = x => OnItemValueChanged(item, x)
//            };

//            mItemActions.Add(item, action);
//            //item.Event_OnClick.AddListener(action.click);// += Result_onSelected; //.AddListener(x => Item_OnToggle(result,                                                                      // x));
//            item.Event_OnValueChanged.AddListener(action.change);
//        }

//        void UnRegItemEvent(BWToggle item) {
//            if (mItemActions.TryGetValue(item, out var action)) {
//                //item.Event_OnClick.RemoveListener(action.click);
//                item.Event_OnValueChanged.RemoveListener(action.change);
//                mItemActions.Remove(item);
//            }
//        }

//        public void RemoveItem(BWToggle item) {
//            if (item != null && mItems.Contains(item)) {
//                mItems.Remove(item);
//                UnRegItemEvent(item);
//                OnRemoveItem(item);
//                //DestroyItem(item);
//            }
//        }

//        public void AddItem(BWToggle item) {
//            if (item != null && !mItems.Contains(item)) {
//                item.IsOn = false;
//                RegItemEvent(item);
//                mItems.Add(item);
//                OnAddItem(item);
//            }
//        }

//        protected virtual void OnItemClicked(BWToggle clickItem) {
//        }

//        protected virtual void OnItemValueChanged(BWToggle item, bool isOn) {
//        }

//        protected virtual void OnAddItem(BWToggle newOne) {
//        }

//        protected virtual void OnRemoveItem(BWToggle removedOne) {
//        }

//        public void Clear() {
//            while (mItems.Count > 0) {
//                RemoveItem(mItems[0]);
//            }
//            //OnClear();
//        }

//        public IEnumerator<BWToggle> GetEnumerator() {
//            return ((IEnumerable<BWToggle>)mItems).GetEnumerator();
//        }

//        IEnumerator IEnumerable.GetEnumerator() {
//            return ((IEnumerable)mItems).GetEnumerator();
//        }
//    }
//}