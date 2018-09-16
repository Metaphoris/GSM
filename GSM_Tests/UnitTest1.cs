using System;
using Xunit;
using GSM;

namespace GSM_Tests
{
    public class UnitTest1
    {
        byte[] alphabet = {128, 128,  96,  64,  40,  24,  14, 136, 132,  98, 193, 104,  56,  30, 144, 136,
                           100,  66, 169,  88,  46, 152, 140, 102, 195, 233, 120,  62, 160, 144, 104,  68,
                            42, 153,  78, 168, 148, 106, 197, 106, 185,  94, 176, 152, 108,  70, 171, 217,
                           110, 184, 156, 110, 199, 235, 249, 126, 192, 160, 112,  72,  44,  26, 143, 200,
                           164, 114, 201, 108,  58, 159, 208, 168, 116,  74, 173,  90, 175, 216, 172, 118,
                           203, 237, 122, 191, 224, 176, 120,  76,  46, 155, 207, 232, 180, 122, 205, 110,
                           187, 223, 240, 184, 124,  78, 175, 219, 239, 248, 188, 126, 207, 239, 251, 255};

        string alphabetString = "@£$¥èéùìòÇ\nØø\rÅåΔ_ΦΓΛΩΠΨΣΘΞ\x1bÆæßÉ !\"#¤%&`()*+,-./0123456789:;<=>?"
                              + "¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà";


        [Fact]
        public void EncodeAlphabetTest()
        {
            Assert.Equal(Program.Encode(alphabetString), alphabet);
        }

        [Fact]
        public void DecodeAlphabetTest()
        {
            Assert.Equal(Program.Decode(alphabet), alphabetString);
        }

        [Fact]
        public void CheckRandomMessage()
        {
            Random rnd = new Random();
            byte messageLength = Convert.ToByte(rnd.Next(0, 119));
            string message = "";

            for (byte i = 0; i < messageLength; i++)
            {
                message += alphabetString[rnd.Next(0, 119)];
            }

            Assert.Equal(message, Program.Decode(Program.Encode(message)));
        }
    }
}
