using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigRogue.Util {

    /// <summary>
    /// Unity组件的扩展方法
    /// </summary>
    public static class ComponentExtension {

        /// <summary>
        /// component.gameObject.SetActive(value)的简写
        /// </summary>
        /// <param name="component"></param>
        /// <param name="value"></param>
        public static void _SetActive (this Component component, bool value) {
            component.gameObject.SetActive (value);
        }

    }
}