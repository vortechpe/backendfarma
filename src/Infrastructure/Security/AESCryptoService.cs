using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Domain.Entities;

namespace Infrastructure.Security
{
    public class AESCryptoService : ISecurityService
    {
        public (string EncryptedText, string Key, string IV) Encrypt(string username, string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Generar clave y IV aleatorios para el usuario
                aesAlg.GenerateKey();
                aesAlg.GenerateIV();

                // Convertir el texto plano a bytes
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                // Encriptar los datos
                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                {
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            csEncrypt.Write(plainTextBytes, 0, plainTextBytes.Length);
                            csEncrypt.FlushFinalBlock();
                        }

                        // Obtener el texto encriptado como un arreglo de bytes
                        byte[] encrypted = msEncrypt.ToArray();

                        // Devolver los valores: texto encriptado, clave y IV en Base64
                        return (Convert.ToBase64String(encrypted), Convert.ToBase64String(aesAlg.Key), Convert.ToBase64String(aesAlg.IV));
                    }
                }
            }
        }

        public string Decrypt(string encryptedText, string key, string iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(key);
                aesAlg.IV = Convert.FromBase64String(iv);

                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                {
                    using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }

        // Método para obtener usuario de la base de datos (esto es solo un ejemplo)
        public bool VerifyPassword(string providedPassword, string storedPasswordHash, string key, string iv)
        {
            // Desencriptamos la contraseña almacenada usando la clave y IV
            string decryptedPassword = Decrypt(storedPasswordHash, key, iv);

            // Comparamos la contraseña proporcionada con la desencriptada
            return providedPassword == decryptedPassword;
        }

        
    }
}
