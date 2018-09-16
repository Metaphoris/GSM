using System;
using System.Collections;

namespace GSM
{
    public static class Program
    {
        // GSM-алфавит
        static string alphabetString = "@£$¥èéùìòÇ\nØø\rÅåΔ_ΦΓΛΩΠΨΣΘΞ\x1bÆæßÉ !\"#¤%&`()*+,-./0123456789:;<=>?"
                                     + "¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà";

        static void Main(string[] args)
        {
            
        }

        //Кодирование
        public static byte[] Encode(string message)
        {
            //Определяем индексы каждого символа сообщения в GSM-алфавите
            char[] message_array = message.ToCharArray();
            byte[] code_array = new byte[message.Length];

            for (Byte i = 0; i < message_array.Length; i++)
            {
                code_array[i] = Convert.ToByte(alphabetString.IndexOf(message_array[i]));
            }

            //Конвертируем в массив бит
            BitArray unpacked = new BitArray(code_array);

            //Определяем число бит, достаточное для хранения в сжатом виде
            int packedSize = code_array.Length * 7;

            //При необходимости, выравниваем размер будущего массива,
            //делая его кратным 8
            bool b = false;
            while (b == false)
            {
                if (packedSize % 8 == 0)
                    b = true;
                else
                    packedSize++;
            }
            BitArray packed = new BitArray(packedSize);

            //Упаковка
            int j = 0;
            for (int i = 0; i < unpacked.Count; i++)
            {
                if ((i + 1) % 8 != 0)
                {
                    packed[j] = unpacked[i];
                    j++;
                }
            }

            //Конвертируем упаковыннй массив бит в массив байт 
            byte[] encryptedMessage = new byte[packed.Length / 8];
            packed.CopyTo(encryptedMessage, 0);

            //Возвращаем результат
            return encryptedMessage;
        }

        //Декодирование
        public static string Decode(byte[] paylod)
        {
            //Конвертируем массив байт в массив бит
            BitArray packed = new BitArray(paylod);

            //Определяем размер массива бит после распаковки
            int unpackedSize = ((paylod.Length * 8) - (paylod.Length * 8 % 7)) * 8 / 7;
            BitArray unpacked = new BitArray(unpackedSize);

            //Распаковка
            int j = 0;
            for (int i = 0; i < (packed.Count - (packed.Count % 7)); i++)
            {
                if ((j + 1) % 8 == 0)
                {
                    unpacked[j] = false;
                    j++;
                }
                unpacked[j] = packed[i];
                j++;
            }

            //Конвертируем массив бит в массив индексов
            byte[] code_array = new byte[unpacked.Length / 8];
            unpacked.CopyTo(code_array, 0);

            //Восстанавливаем сообщение по индексу
            string message = "";
            for (int i = 0; i < code_array.Length; i++)
            {
                message += alphabetString[code_array[i]];
            }

            return message;
        }
    }
}