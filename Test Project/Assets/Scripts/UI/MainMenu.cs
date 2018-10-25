using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BW.Core.UI {

    /// <summary>
    /// 窗口类
    /// </summary>
    public class MainMenu : MonoBehaviour {

        protected IPage m_currentPage;

        protected readonly Stack<IPage> m_pageStack = new Stack<IPage>();

        /// <summary>
        /// 切换页面
        /// </summary>
        /// <param name="newPage"></param>
        protected virtual void ChangePage(IPage newPage) {
            DeactivateCurrentPage();

            ActivateCurrentPage(newPage);
        }

        /// <summary>
        /// 关闭当前页面
        /// </summary>
        protected void DeactivateCurrentPage() {
            if (m_currentPage != null) {
                m_currentPage.Hide();
            }
        }

        /// <summary>
        /// 设置并显示当前页面
        /// </summary>
        /// <param name="newPage"></param>
        protected void ActivateCurrentPage(IPage newPage) {
            m_currentPage = newPage;
            m_currentPage.Show();
            m_pageStack.Push(m_currentPage);
        }


        protected void Back() {
            if (m_pageStack.Count == 0)
                return;

            DeactivateCurrentPage();
            m_pageStack.Pop();
            ActivateCurrentPage(m_pageStack.Pop());
        }

        /// <summary>
        /// 返回到特定页面
        /// </summary>
        /// <param name="backPage"></param>
        protected void Back(IPage backPage) {
            int count = m_pageStack.Count;
            // 如果栈空,则直接打开backPage
            if (count == 0) {
                ChangePage(backPage);
                return;
            }

            // pop栈,直到发现backPage保留backPage之前的栈
            for (int i=count-1;i>=0;i--) {
                IPage currentPage = m_pageStack.Pop();
                if (currentPage == backPage) {
                    ChangePage(backPage);
                    return;
                }
            }
            // 栈里不存在backpage,则直接打开backpage
            ChangePage(backPage);
        }

    }
}