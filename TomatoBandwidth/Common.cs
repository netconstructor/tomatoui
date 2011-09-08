namespace TomatoBandwidth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Common helper methods.
    /// </summary>
    internal static class Common
    {
        /// <summary>
        /// Logical right shift operation.
        /// </summary>
        /// <param name="shiftedValue">
        /// The value to be shifted.
        /// </param>
        /// <param name="shiftCount">
        /// The shift count.
        /// </param>
        /// <returns>
        /// Shifted value.
        /// </returns>
        public static int LogicalRightShift(int shiftedValue, int shiftCount)
        {
            // .NET does not have >>> operator which was in the original javascript code.
            // At the moment this seems to work. See stackoverflow for more information:
            // http://stackoverflow.com/questions/5253194/implementing-logical-right-shift-in-c
            return (int)((uint)shiftedValue >> shiftCount);
        }
    }
}
