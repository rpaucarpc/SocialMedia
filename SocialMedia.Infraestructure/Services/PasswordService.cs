﻿using Microsoft.Extensions.Options;
using SocialMedia.Infraestructure.Interfaces;
using SocialMedia.Infraestructure.Options;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace SocialMedia.Infraestructure.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordOptions _options;
        public PasswordService(IOptions<PasswordOptions> options )
        {
            _options = options.Value;
        }
        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.',3);
            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format");
            }

            var iterations = Convert.ToInt32( parts[0] );
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algorithm = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA512))
            {
                var keyToCheck = algorithm.GetBytes(_options.KeySize);
                return keyToCheck.SequenceEqual(key);
            }
        }

        public string Hash(string password)
        {
            // PBKDF2 Implementation
            using (var algorithm = new Rfc2898DeriveBytes(password, _options.SaltSize,_options.Iterations, HashAlgorithmName.SHA512))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(_options.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);
                return $"{_options.Iterations}.{salt}.{key}";
            }
        }
    }
}
