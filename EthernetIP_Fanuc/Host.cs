
// See http://eeip-library.de/codesamples/

namespace EthernetIP_Fanuc
{
    #region Directives
    // Standard .NET Directives
    using System;
    using System.Text;

    using Sres.Net.EEIP;

    #endregion





    /// <summary>
    /// Host object
    /// </summary>
    public class Host
    {
        private const int CLASS_REGISTER_INTEGERS = 107;    //0x6B
        private const int CLASS_REGISTER_FLOAT = 108;       //0x6C
        private const int CLASS_REGISTER_STRING = 109;       //0x6D

        EEIPClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="Host"/> Class.
        /// </summary>
        public Host()
        {
            _client = new EEIPClient()
            {
                IPAddress = "192.168.125.1"
            };
            try
            {
                _client.RegisterSession();

                //ReadWriteToIntegerRegister();
                //ReadWriteStringRegister();
                ReadWritePositionRegister();

                _client.UnRegisterSession();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        public void Execute()
        {

        }


        /// <summary>
        /// Read Position register according  Table 7-22
        /// </summary>
        private void ReadWritePositionRegister()
        {

            byte[] xPosBytes = new byte[4];




            byte[] response = _client.GetAttributeSingle(0x7c, 1, 1);

            Array.Copy(response, 4, xPosBytes, 0, 4);

            Array.Reverse(xPosBytes);

            float posX = BitConverter.ToSingle  (xPosBytes,0);






        }






        /// <summary>
        /// Read / Write a string to the string registers.
        /// </summary>
        /// <remarks>
        /// See figure 7-12
        /// </remarks>
        private void ReadWriteStringRegister()
        {
            string writeSting = "Whohoo hihaa";
            byte[] writeReg = new byte[88];

            // Copy the string
            Array.Copy(Encoding.ASCII.GetBytes(writeSting), 0, writeReg, 4, writeSting.Length);
            // Add the length
            Array.Copy(BitConverter.GetBytes(writeSting.Length), writeReg, 4);
            // Write a string.
            _client.SetAttributeSingle(CLASS_REGISTER_STRING, 1, 1, writeReg);

            // Read the string.
            byte[] responseStruct = _client.GetAttributeSingle(CLASS_REGISTER_STRING, 1, 1);
            if (responseStruct.GetUpperBound(0) >4)
            {
                int responceLength = BitConverter.ToInt32(responseStruct.Chunk(0, 4),0);
                string responce = Encoding.UTF8.GetString(responseStruct,4 , responceLength );
            }
        }


        /// <summary>
        /// Read / Write integer values to the Integer registers.
        /// </summary>
        private void ReadWriteToIntegerRegister()
        {
            // Reads 4 bytes integers (byte[0] = LSB)
            // See page 7-16 / 7-17

            // Max R[200]
            // Reads Register R[1]
            byte[] response = _client.GetAttributeSingle(CLASS_REGISTER_INTEGERS, 1, 1);

            // Writes to R[1]
            _client.SetAttributeSingle(CLASS_REGISTER_INTEGERS, 1, 1, BitConverter.GetBytes(12345));
            int returnValue = BitConverter.ToInt32(response, 0);
        }




    }
}
