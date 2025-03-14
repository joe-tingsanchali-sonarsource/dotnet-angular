using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

public class SecurityIssues
{
    public void VulnerableCode(string userInput, string filePath, string remoteUrl)
    {
        // 1. SQL Injection (SCS0006) - String concatenation used directly in SQL query.
        string sqlQuery = "SELECT * FROM Users WHERE Username = '" + userInput + "'"; // VULNERABLE

        // 2. Path Traversal (SCS0018) - User-supplied input used to construct file path without validation.
        string fullPath = @"C:\data\" + filePath;
        File.ReadAllText(fullPath); // VULNERABLE

        // 3. Command Injection (SCS0029) - User-supplied input used directly in a system command.
        System.Diagnostics.Process.Start("cmd.exe", "/c ping " + userInput); // VULNERABLE

        // 4. Insecure Deserialization (SCS0025) - Deserialization of untrusted data without validation.
        byte[] serializedData = Convert.FromBase64String(userInput);
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        formatter.Deserialize(new MemoryStream(serializedData)); // VULNERABLE

        // 5. Hardcoded Cryptographic Key (SCS0007) - Hardcoded cryptographic key.
        string hardcodedKey = "MySecretKey123";
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(hardcodedKey); // VULNERABLE
            // ... (encryption/decryption logic using the hardcoded key)
        }

        // 6. open redirect
        try
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadString(remoteUrl); //VULNERABLE if remoteURL is user controlled.
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }
}
