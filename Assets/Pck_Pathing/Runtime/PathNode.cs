using UnityEngine;
using System.Collections;
namespace Bw.BwGameCode.Pathing
{
    /// <summary>
    /// 寻路的结点
    /// </summary>
    public class PathNode
    {
        public Vector3Int Coord { get; set; }       // 坐标

        public bool Aviable = true;   // 能否行走

        public float Height { get { return Coord.y; } }

        /// <summary>
        /// 到终点的Cost,只需计算一次
        /// </summary>
        public float H { get; set; }

        /// <summary>
        /// 到起点的Cost,会不断更新
        /// </summary>
        public float G { get; set; }

        /// <summary>
        /// 整体的Cost
        /// </summary>
        public float F { get { return H + G; } }

        /// <summary>
        /// 从2d坐标构造一个寻路节点
        /// </summary>
        /// <param name="coordinate2D"></param>
        public PathNode(Vector3Int coord) {
            this.Coord = coord;
            Aviable = true;
        }

        public PathNode Parent { get; set; }             //
        public PathNode Child { get; set; }

        public override bool Equals(object obj) {
            if (!(obj is PathNode)) return false;
            PathNode node = (PathNode)obj;
            return Coord == node.Coord;
        }

        public bool Equals(PathNode node) {
            return Coord == node.Coord;
        }

        public override int GetHashCode() {
            return Coord.x ^ Coord.y;
        }

        /// <summary>
        /// 是否需要更新 center 为新的 parent
        /// </summary>
        /// <param name="center"></param>
        /// <param name="func"></param>
        public void UpdateFrom(PathNode center, HeuristicsDelegate func) {
            float deltaG = func(this.Coord, center.Coord);
            float newG = center.G + deltaG;
            if (newG < G) {
                Parent = center;
                G = newG;
            }
        }

        // 发现一个新点
        public void GetDataFrom(PathNode center, PathNode end, HeuristicsDelegate func) {
            float deltaG = func(this.Coord, center.Coord);
            G = center.G + deltaG;
            H = func(this.Coord, end.Coord);
            Parent = center;
        }
    }
}