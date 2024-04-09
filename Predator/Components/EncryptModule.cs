using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Predator
{
    internal class EncryptModule
    {



        static byte[] GenerateBytes(int length)
        {
            byte[] data = new byte[length];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(data);
            }
            return data;
        }

        public static void EncryptFile(string fileNameIn, string fileNameOut, string fileNameKey)
        {
            //Para leer la clave publica base64 y convertirla en string
            StreamReader sr = new StreamReader(fileNameKey);
            String aesKeyString = sr.ReadToEnd();
            Console.WriteLine(aesKeyString);
            byte[] aesIV = GenerateBytes(16);
            byte[] aesKey = Convert.FromBase64String(aesKeyString);
            //Crear el vector de inicializacion
            using(AesManaged aes = new AesManaged())
            {
                aes.Key = aesKey;
                aes.IV = aesIV;
                //crea el archivo donde se va a cifrar los datos
                using (FileStream fsCrypt = new FileStream(fileNameOut, FileMode.Create))
                {
                    fsCrypt.Write(aesIV, 0, aesIV.Length);
                    //escribe los datos cifrados
                    using(CryptoStream cs = new CryptoStream(fsCrypt, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (FileStream fsIn = new FileStream(fileNameIn, FileMode.Open))
                        {
                            int data;
                            while ((data = fsIn.ReadByte()) != -1)
                                cs.WriteByte((byte)data);

                            Console.WriteLine("se ha cifrado correctamente");
                        }
                    }
                }
            }


        }

        //El input sera para la lectura del archivo, el out para crear el archivo descifrado
        public static void DecryptFile(string fileNameIn, string fileNameOut, string fileNameKey)
        {
            //Para leer la clave publica base64 y convertirla en string
            StreamReader sr = new StreamReader(fileNameKey);
            String aesKeyString = sr.ReadToEnd();
            Console.WriteLine(aesKeyString);
            byte[] aesIV = GenerateBytes(16);
            byte[] aesKey = Convert.FromBase64String(aesKeyString);
            //Crear el vector de inicializacion
            using (AesManaged aes = new AesManaged())
            {
                aes.Key = aesKey;
                //abre el archivo donde se va a cifrar los datos
                using (FileStream fsDecrypt = new FileStream(fileNameIn, FileMode.Open))
                {
                    fsDecrypt.Read(aesIV, 0, aesIV.Length);
                    aes.IV = aesIV;
                    //lee los datos cifrados
                    using (CryptoStream cs = new CryptoStream(fsDecrypt, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (FileStream fsOut = new FileStream(fileNameOut, FileMode.Create))
                        {
                            int data;
                            while ((data = cs.ReadByte()) != -1)
                                fsOut.WriteByte((byte)data);

                            Console.WriteLine("se ha descifrado correctamente");
                        }
                    }
                }
            }
        }
    }
}
