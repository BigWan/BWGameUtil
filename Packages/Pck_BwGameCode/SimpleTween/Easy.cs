using System;

namespace BW.GameCode.Foundation
{

    /// <summary>
    /// EasyFunction
    /// In Function he Out Function 关于点(0.5,0.5)对称
    /// </summary>
    public static class EaseFunctions {

        private const double pi = 3.14159265358979;


        private static float Clamp01(float t) {
            return Math.Max(Math.Min(t, 1f), 0);

        }


        /// <summary>
        /// x^n次方增长,凹函数
        /// </summary>
        /// <param name="t">0-1的值</param>
        /// <param name="power">指数</param>
        /// <returns></returns>
        private static float InPower(float t, float power = 1f) {
            return (float)Math.Pow(t, power);
        }

        /// <summary>
        /// x^n关于(0.5,0.5)对称的凸函数
        /// </summary>
        /// <param name="t">0-1</param>
        /// <param name="power">指数</param>
        /// <returns></returns>
        private static float OutPower(float t, float power = 1f) {
            return 1f - InPower(1f - t, power);
        }

        /// <summary>
        /// 先凹后凸
        /// </summary>
        /// <param name="t"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        private static float InOutPower(float t, float power = 1f) {
            if (t < 0.5f) return InPower(2f * t, power) * 0.5f;
            return OutPower((t - 0.5f) * 2f, power) * 0.5f + 0.5f;
        }

        /// <summary>
        /// 先凸后凹
        /// </summary>
        /// <param name="t"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        private static float OutInPower(float t, float power = 1f) {
            if (t < 0.5f) return OutPower(2f * t, power) * 0.5f;
            return InPower((t - 0.5f) * 2, power) * 0.5f + 0.5f;
        }


        /// <summary>In平方 </summary>
        public static float InSquare(float t) => InPower(t, 2);
        /// <summary>Out平方 </summary>
        public static float OutSquare(float t) => OutPower(t, 2);
        /// <summary>InOut平方 </summary>
        public static float InOutSquare(float t) => InOutPower(t, 2);
        /// <summary>OutIn平方 </summary>
        public static float OutInSquare(float t) => OutInPower(t, 2);


        /// <summary>In立方 </summary>
        public static float InCube(float t) => InPower(t, 3);
        /// <summary>Out立方 </summary>
        public static float OutCube(float t) => OutPower(t, 3);
        /// <summary>InOut立方 </summary>
        public static float InOutCube(float t) => InOutPower(t, 3);
        /// <summary>OutIn立方 </summary>
        public static float OutInCube(float t) => OutInPower(t, 3);


        /// <summary>In4次方 </summary>
        public static float InQuartic(float t) => InPower(t, 4);
        /// <summary>Out4次方 </summary>
        public static float OutQuartic(float t) => OutPower(t, 4);
        /// <summary>InOut4次方 </summary>
        public static float InOutQuartic(float t) => InOutPower(t, 4);
        /// <summary>OutIn4次方 </summary>
        public static float OutInQuartic(float t) => OutInPower(t, 4);


        /// <summary>In五次方 </summary>
        public static float InQuintic(float t) => InPower(t, 5);
        /// <summary>Out五次方 </summary>
        public static float OutQuintic(float t) => OutPower(t, 5);
        /// <summary>InOut五次方 </summary>
        public static float InOutQuintic(float t) => InOutPower(t, 5);
        /// <summary>OutIn五次方 </summary>
        public static float OutInQuintic(float t) => OutInPower(t, 5);


        /// <summary> 0-pi/2之间的sin图像 </summary>
        public static float InSine(float t) => 1 - OutSine(1 - t);
        /// <summary> 0-pi/2之间的cos图像 </summary>
        public static float OutSine(float t) => (float)Math.Sin(t * pi * 0.5f);


        public static float InOutSine(float t) {
            if (t < 0.5f) return InSine(2f * t) * 0.5f;
            return OutSine(2 * (t - 0.5f)) * 0.5f + 0.5f;
        }


        public static float OutInSine(float t) {
            if (t < 0.5f) return OutSine(2 * t) * 0.5f;
            return InSine(2 * (t - 0.5f)) * 0.5f + 0.5f;
        }

        /// <summary>
        /// y = 2^x 指数函数图像[-无穷,0]
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float InExp(float t) {
            if (Math.Abs(t) <= 0.0000001f) return 0;
            return (float)Math.Pow(2f, 10f * (t - 1f));
        }


        public static float OutExp(float t) {
            return 1 - InExp(1 - t);
        }

        public static float InOutExp(float t) {
            if (t < 0.5f) return InExp(2 * t) * 0.5f;
            return OutExp(2 * (t - 0.5f)) * 0.5f + 0.5f;
        }

        public static float OutInExp(float t) {
            if (t < 0.5f) return OutExp(2f * t) * 0.5f;
            return InExp(2f * (t - 0.5f)) * 0.5f + 0.5f;
        }


        public static float InCircle(float t) {
            t = Clamp01(t);
            return 1f - (float)Math.Sqrt(1f - t * t);
        }

        public static float OutCircle(float t) {
            return 1f - InCircle(1f - t);
        }


        public static float InOutCircle(float t) {
            if (t < 0.5f) return InCircle(2f * t) * 0.5f;
            return OutCircle(2 * (t - 0.5f)) * 0.5f + 0.5f;
        }

        public static float OutInCircle(float t) {
            if (t < 0.5f)
                return OutCircle(2f * t) * 0.5f;
            return InCircle(2f * t - 1f) * 0.5f + 0.5f;
        }

        /// <summary>
        /// Perlin 函数作者提出的Smooth函数
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float PerlinSmooth(float t) {
            return t * t * t * (t * (t * 6f - 15f) + 10f);
        }

    }
}
