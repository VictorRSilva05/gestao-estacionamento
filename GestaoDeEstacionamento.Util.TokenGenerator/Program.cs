﻿using System.Security.Cryptography;

namespace GestaoDeEstacionamento.Util.TokenGenerator;

internal class Program
{
    static void Main(string[] args)
    {
       var chave = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        Console.WriteLine("Chave: " + chave);

        Console.Read();
    }
}
