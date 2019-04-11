
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

                ReadWriteToIntegerRegister();

                ReadWriteStringRegister();

            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            _client.UnRegisterSession();

        }

        public void Execute()
        {

        }


        private void ReadWriteStringRegister()
        {
            // Write a string.
            _client.SetAttributeSingle(CLASS_REGISTER_STRING, 1, 1, Encoding.ASCII.GetBytes("Whohoo!!") );


            byte[] response = _client.GetAttributeSingle(CLASS_REGISTER_STRING, 1, 1);
            string asString = Encoding.ASCII.GetString(response);

        }


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
