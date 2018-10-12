using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BigRogue.PathFinding {

    /// <summary>
    /// 寻路功能主体
    /// </summary>
    public static class AStar {

        static List<PathNode> openList;
        static List<PathNode> closeList;

        static AStar() {
            openList = new List<PathNode>();
            closeList = new List<PathNode>();
        }

        /// <summary>
        /// 检测能否到达邻点
        /// </summary>
        /// <param name="current">起点</param>
        /// <param name="neighbour">邻点</param>
        /// <param name="hightLimit">高差限制(能从高处走到低处,不能从低处走向高处)</param>
        /// <returns></returns>
        static bool CanMoveTo(PathNode current, PathNode neighbour, int hightLimit) {
            //if (!neighbour.aviable) return false;

            return neighbour.height - current.height <= hightLimit;
        }


        /// <summary>
        /// 寻路主算法
        /// </summary>
        /// <param name="mesh">寻路网格</param>
        /// <param name="start">开始结点</param>
        /// <param name="end">结束结点</param>
        /// <param name="hType">启发函数类型</param>
        /// <param name="allowDiagonal">允许走对角线</param>
        /// <param name="hightLimit">高度限制</param>
        /// <returns></returns>
        public static List<PathNode> FindPath(NodeMesh mesh, PathNode start, PathNode end, HeuristicsType hType, bool allowDiagonal, int hightLimit = 0) {

            if (!mesh.Exists(start) || !mesh.Exists(end)) return null;

            HeuristicsDelegate hFunc = Heuristics.HeuristicsFactory(hType);

            openList.Clear();
            closeList.Clear();

            // 算法
            PathNode current;
            openList.Add(start);

            bool canReach = false;

            // 循环处理开放节点列表(边界)
            while (openList.Count > 0) {

                current = openList.OrderBy(n => n.f).First();

                if (current.Equals(end)) {
                    canReach = true;
                    break;
                }

                openList.Remove(current);
                closeList.Add(current);

                // 获取相邻节点
                List<PathNode> neighbours = mesh.GetNeighbour(current, allowDiagonal);

                // 遍历处理邻居节点
                foreach (PathNode nb in neighbours) {
                    // 已经在关闭列表则跳过
                    if (!CanMoveTo(current, nb, hightLimit)) continue;
                    if (closeList.IndexOf(nb) > -1) continue;

                    // 在开放列表(检测边界)
                    if (openList.IndexOf(nb) > -1) {   // nb在open列表中
                        // 检测,是否需要 (更新G值,设置parent为current)
                        nb.UpdateFrom(current, hFunc);
                    } else {
                        // 计算H 和parent和G// 新出现的节点
                        nb.GetDataFrom(current, end, hFunc);
                        openList.Add(nb);
                    }
                }
            }

            if (!canReach) return null;

            current = end;
            List<PathNode> result = new List<PathNode>();
            while (current.parent != null) {
                result.Add(current);
                current = current.parent;
            }

            return result ;
        }

    }
}
