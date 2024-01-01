// using RaythosAerospace.Keys;
using RaythosAerospace.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
namespace RaythosAerospace.CustomServices
{
    public class CustomService : ICustomService
    {

        // private KeyModel _key;
        private KeyModel _key;

        public CustomService()
        {
            string filePath = "Keys/keys.json"; // Update this with your file path
            string jsonString = System.IO.File.ReadAllText(filePath);

            // Deserialize JSON to an object
            _key = JsonSerializer.Deserialize<KeyModel>(jsonString);

        }

        public JWTSettingsModel GetJWTSettings()
        {
           return _key.JWTsettings;
        }

        public string GetPublicKey()
        {
            return _key.publicKey;
        }

        public string GetSecretKey()
        {
            return _key.secretKey;
        }

        public void ReadKeys()
        {
            //reading key for stripe

        }
    }
}
