
namespace EthernetIP_Fanuc
{
    #region Directives
    // Standard .NET Directives
    using System;
    #endregion

    /// <summary>
    /// ByteExtentions object
    /// </summary>
    public static class ByteExtentions
    {

        public static byte[] Chunk(this byte[] source, int start, int length)
        {
            byte[] returnValue = new byte[length];
            Array.Copy(source, start, returnValue, 0, length);
            return returnValue;
        }



    }
}
