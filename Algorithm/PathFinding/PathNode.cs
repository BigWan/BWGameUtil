using UnityEngine;
using System.Collections;
using BigRogue.Ground;
namespace BigRogue.PathFinding {

    /// <summary>
    /// 寻路的结点
    /// </summary>
    public class PathNode {

        public Vector3Int coord;       // 坐标

        //public int y { get; set; }
        //public int x { get { return coordinate2D.x; } }
        //public int z { get { return coordinate2D.y; } }

        public bool aviable = true;   // 能否行走

        public float height { get { return coord.y ; } }


        //public Vector3Int coordinate3D {
        //    get { return new Vector3Int(x, y, z); }
        //}

        //public Vector3 localPosition {
        //    get {
        //        return new Vector3(x, height, z);
        //    }
        //}

        /// <summary>
        /// 到终点的Cost,只需计算一次
        /// </summary>
        public float h;

        /// <summary>
        /// 到起点的Cost,会不断更新
        /// </summary>
        public float g;

        /// <summary>
        /// 整体的Cost
        /// </summary>
        public float f { get { return h + g; } }

        /// <summary>
        /// 从Block构造一个节点
        /// </summary>
        /// <param name="block"></param>
        public PathNode(Block block) {
            coord = block.coord;
            //coordinate2D = new Vector2Int(block.x, block.z);
            //y = block.y;
            aviable = true;
        }
        /// <summary>
        /// 从2d坐标构造一个寻路节点
        /// </summary>
        /// <param name="coordinate2D"></param>
        public PathNode(Vector3Int coord) {
            this.coord =coord;
            aviable = true;
        }


        public PathNode parent;             // 
        public PathNode child;

        public override bool Equals(object obj) {
            if (!(obj is PathNode)) return false;
            PathNode node = (PathNode)obj;
            return coord == node.coord;
        }

        public bool Equals(PathNode node) {
            return coord == node.coord;
        }

        public override int GetHashCode() {
            return coord.x ^ coord.y;
        }

        /// <summary>
        /// 是否需要更新 center 为新的 parent
        /// </summary>
        /// <param name="center"></param>
        /// <param name="func"></param>
        public void UpdateFrom(PathNode center,HeuristicsDelegate func) {
            float deltaG = func(this.coord, center.coord);
            float newG = center.g + deltaG;
            if(newG < g) {
                parent = center;
                g = newG;
            }
        }

        // 发现一个新点
        public void GetDataFrom(PathNode center,PathNode end,HeuristicsDelegate func) {
            float deltaG = func(this.coord, center.coord);
            g = center.g + deltaG;
            h = func(this.coord, end.coord);
            parent = center;
        }

    }

}
