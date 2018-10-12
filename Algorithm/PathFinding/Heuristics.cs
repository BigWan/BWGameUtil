using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace BigRogue.PathFinding {

    /// <summary>
    /// 2D网格结点的距离计算
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public delegate float HeuristicsDelegate(Vector3Int p1,Vector3Int p2);


    public enum HeuristicsType {
        Manhattan,
        Manhattan2D,
        Chebyshev,
        Chebyshev2D,
        Euclidean,
        Euclidean2D
    }


    /// <summary>
    /// 启发函数
    /// </summary>
    public class Heuristics {

        /// <summary>
        /// 曼哈顿
        /// </summary>
        public static float Manhattan(Vector3Int n1, Vector3Int n2) {
            return Mathf.Abs(n1.x - n2.x) + Mathf.Abs(n1.y - n2.y) + Mathf.Abs(n1.z - n2.z);
        }

        public static float Manhattan2D(Vector3Int n1,Vector3Int n2) {
            return Mathf.Abs(n1.x-n2.x) + Mathf.Abs(n1.z-n2.z);
        }


        /// <summary>
        /// 欧几里得
        /// </summary>
        public static float Euclidean(Vector3Int n1, Vector3Int n2) {
            return Vector3Int.Distance(n1, n2);
        }
        public static float Euclidean2D(Vector3Int n1, Vector3Int n2) {
            return Mathf.Sqrt((n1.x - n2.x) ^ 2 + (n1.z - n2.z) ^ 2);
        }


        /// <summary>
        /// 切比雪夫
        /// </summary>
        public static float Chebyshev(Vector3Int n1, Vector3Int n2) {
            return Mathf.Max(Mathf.Abs(n1.x-n2.x), Mathf.Abs(n1.y - n2.y), Mathf.Abs(n1.z-n2.z));
        }

        public static float Chebyshev2D(Vector3Int n1,Vector3Int n2) {
            return Mathf.Max(Mathf.Abs(n1.x - n2.x), Mathf.Abs(n1.y - n2.y));
        }


        public static HeuristicsDelegate HeuristicsFactory(HeuristicsType hType) {

            switch (hType) {
                case HeuristicsType.Manhattan:return Manhattan;
                case HeuristicsType.Chebyshev:return Chebyshev;
                case HeuristicsType.Euclidean:return Euclidean;
                default: return Manhattan;
            }

        }

    }
}
