using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters;

namespace textReplacer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Extract(args[0]);
        }
        public static void Extract(string file)
        {
            var reader = new BinaryReader(File.OpenRead(file));
            int size = reader.ReadInt32();
            int[] pointers = new int[size / 4];
            reader.BaseStream.Position = 0;
            for (int i = 0; i < size / 4; i++)
            {
                pointers[i] = reader.ReadInt32();
            }
            int h = 0;
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                if (h == (size / 3) - 1)
                {
                    byte[] bytes = reader.ReadBytes((int)reader.BaseStream.Length - pointers[h]);
                    File.WriteAllBytes(h + ".raw", bytes);
                }
                else
                {
                    byte[] bytes = reader.ReadBytes(pointers[h + 1] - pointers[h]);
                    File.WriteAllBytes(h + ".raw", bytes);
                }
                h++;
            }
            }
        }
    }
