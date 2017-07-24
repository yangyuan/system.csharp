using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Hash
{
    class ClassHash
    {
        public byte[] hash_ed2k(Stream s)
        {
            s.Position = 0;
            // this MD4 is pretty slow, you may want to use HashLib, both working
            // HashLib.IHash hash = HashLib.HashFactory.Crypto.CreateMD4();
            
            HashAlgorithm hash = new MD4();

            long length = s.Length;
            int bucks = (int)(length / 9728000) + 1;
            MemoryStream ms = new MemoryStream();

            byte[] buffer = new byte[9728000];
            byte[] hash_cache = new byte[1];
            for (int i = 0; i < bucks; i++)
            {
                int length_data = 0;
                while (true)
                {
                    int realsize = s.Read(buffer, 0, 9728000 - length_data);
                    if (realsize == 0) break;
                    length_data += realsize;
                }

                byte[] buffer_temp = buffer;
                if (length_data != 9728000)
                {
                    buffer_temp = new byte[length_data];
                    Array.Copy(buffer, buffer_temp, length_data);
                }
                // hash_cache = hash.ComputeBytes(buffer_temp).GetBytes(); 
                hash_cache = hash.ComputeHash(buffer, 0, length_data);
                //Console.WriteLine(ByteArrayToString(hash_cache));
                ms.Write(hash_cache, 0, hash_cache.Length);
            }
            ms.Position = 0;
            if (bucks == 1)
            {
                return hash_cache;
            }
            else
            {
                byte[] hash_result = hash.ComputeHash(ms); // hash.ComputeHash(ms); hash.ComputeStream(ms)
                return hash_result;
            }
        }

        public byte[] hash_md5(Stream s)
        {
            s.Position = 0;
            HashAlgorithm hash = MD5.Create();

            byte[] hash_result = hash.ComputeHash(s);
            return hash_result;
        }
        public byte[] hash_baidu_md5_slice(Stream s)
        {
            s.Position = 0;
            HashAlgorithm hash = MD5.Create();
            BinaryReader br = new BinaryReader(s);
            byte[] slice = br.ReadBytes(256 * 1024);

            byte[] hash_result = hash.ComputeHash(slice, 0, slice.Length);
            return hash_result;
        }

        public byte[] hash_sha1(Stream s)
        {
            s.Position = 0;
            HashAlgorithm hash = SHA1.Create();

            byte[] hash_result = hash.ComputeHash(s);
            return hash_result;
        }
        public byte[] hash_sha256(Stream s)
        {
            s.Position = 0;
            HashAlgorithm hash = SHA256.Create();

            byte[] hash_result = hash.ComputeHash(s);
            return hash_result;
        }
        public byte[] hash_sha512(Stream s)
        {
            s.Position = 0;
            HashAlgorithm hash = SHA512.Create();

            byte[] hash_result = hash.ComputeHash(s);
            return hash_result;
        }
        public byte[] hash_crc32(Stream s)
        {
            s.Position = 0;
            HashAlgorithm hash = CRC32.Create();

            byte[] hash_result = hash.ComputeHash(s);
            return hash_result;
        }
        public byte[] hash_xunlei_cid(Stream s)
        {
            s.Position = 0;

            HashAlgorithm hash = SHA1.Create();

            MemoryStream ms = new MemoryStream();
            long length = s.Length;
            if (length < 0xF000)
            {
                s.CopyTo(ms);
            }
            else
            {
                byte[] buffer = new byte[0x5000];
                BinaryReader br = new BinaryReader(s);
                buffer = br.ReadBytes(0x5000);
                ms.Write(buffer, 0, 0x5000);
                s.Position = length / 3;
                br = new BinaryReader(s);
                buffer = br.ReadBytes(0x5000);
                ms.Write(buffer, 0, 0x5000);
                s.Position = length - 0x5000;
                br = new BinaryReader(s);
                buffer = br.ReadBytes(0x5000);
                ms.Write(buffer, 0, 0x5000);
            }

            ms.Position = 0;
            byte[] hash_result = hash.ComputeHash(ms);
            return hash_result;
        }
        public byte[] hash_xunlei_gcid(Stream s)
        {
            s.Position = 0;

            HashAlgorithm hash = SHA1.Create();

            MemoryStream ms = new MemoryStream();
            long length = s.Length;
            int psize = 0x40000;
            while (length / psize > 0x200 && psize < 0x200000)
            {
                psize = psize * 2;
            }
            BinaryReader br = new BinaryReader(s);
            byte[] buffer = br.ReadBytes(psize);
            while (buffer.Length != 0)
            {
                byte[] hash_cache = hash.ComputeHash(buffer, 0, buffer.Length);
                ms.Write(hash_cache, 0, hash_cache.Length);
                buffer = br.ReadBytes(psize);
            }

            ms.Position = 0;
            byte[] hash_result = hash.ComputeHash(ms);
            return hash_result;
        }
    }

    public class CRC32 : HashAlgorithm
    {
        private UInt32[] crc32Table = new UInt32[256];
        private UInt32 crc32Result;

        public CRC32()
            : this(0xEDB88320)
        {
            this.HashSizeValue = 32;
        }

        public CRC32(UInt32 polynomial)
            : base()
        {
            for (UInt32 i = 0; i < 256; i++)
            {
                UInt32 crc32 = i;
                for (int j = 8; j > 0; j--)
                {
                    if ((crc32 & 1) == 1)
                    {
                        crc32 = (crc32 >> 1) ^ polynomial;
                    }
                    else
                    {
                        crc32 >>= 1;
                    }
                }
                crc32Table[i] = crc32;
            }

            Initialize();
        }

        public override void Initialize()
        {
            this.crc32Result = 0xFFFFFFFF;
        }

        public override bool CanReuseTransform { get { return true; } }

        public override bool CanTransformMultipleBlocks { get { return true; } }

        public UInt32 CRC32Hash { get; protected set; }

        new public static CRC32 Create()
        {
            return new CRC32();
        }

        public static CRC32 Create(UInt32 polynomial)
        {
            return new CRC32(polynomial);
        }

        new public static CRC32 Create(String hashName)
        {
            throw new NotImplementedException();
        }

        protected override void HashCore(Byte[] array, int start, int size)
        {
            int end = start + size;
            for (int i = start; i < end; i++)
            {
                this.crc32Result = (this.crc32Result >> 8) ^ this.crc32Table[array[i] ^ (this.crc32Result & 0x000000FF)];
            }
        }

        protected override Byte[] HashFinal()
        {
            this.crc32Result = ~this.crc32Result;

            this.CRC32Hash = this.crc32Result;

            this.HashValue = BitConverter.GetBytes(this.crc32Result);

            Array.Reverse(this.HashValue, 0, this.HashValue.Length);
            return this.HashValue;
        }
    }

    // copyed from internet, need a better performance one
    public class MD4 : HashAlgorithm
    {
        private uint _a;
        private uint _b;
        private uint _c;
        private uint _d;
        private uint[] _x;
        private int _bytesProcessed;

        public MD4()
        {
            _x = new uint[16];

            Initialize();
        }

        public override void Initialize()
        {
            _a = 0x67452301;
            _b = 0xefcdab89;
            _c = 0x98badcfe;
            _d = 0x10325476;

            _bytesProcessed = 0;
        }

        protected override void HashCore(byte[] array, int offset, int length)
        {
            ProcessMessage(Bytes(array, offset, length));
        }

        protected override byte[] HashFinal()
        {
            try
            {
                ProcessMessage(Padding());

                return new[] { _a, _b, _c, _d }.SelectMany(word => Bytes(word)).ToArray();
            }
            finally
            {
                Initialize();
            }
        }

        private void ProcessMessage(IEnumerable<byte> bytes)
        {
            foreach (byte b in bytes)
            {
                int c = _bytesProcessed & 63;
                int i = c >> 2;
                int s = (c & 3) << 3;

                _x[i] = (_x[i] & ~((uint)255 << s)) | ((uint)b << s);

                if (c == 63)
                {
                    Process16WordBlock();
                }

                _bytesProcessed++;
            }
        }

        private static IEnumerable<byte> Bytes(byte[] bytes, int offset, int length)
        {
            for (int i = offset; i < length; i++)
            {
                yield return bytes[i];
            }
        }

        private IEnumerable<byte> Bytes(uint word)
        {
            yield return (byte)(word & 255);
            yield return (byte)((word >> 8) & 255);
            yield return (byte)((word >> 16) & 255);
            yield return (byte)((word >> 24) & 255);
        }

        private IEnumerable<byte> Repeat(byte value, int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return value;
            }
        }

        private IEnumerable<byte> Padding()
        {
            return Repeat(128, 1)
               .Concat(Repeat(0, ((_bytesProcessed + 8) & 0x7fffffc0) + 55 - _bytesProcessed))
               .Concat(Bytes((uint)_bytesProcessed << 3))
               .Concat(Repeat(0, 4));
        }

        private void Process16WordBlock()
        {
            uint aa = _a;
            uint bb = _b;
            uint cc = _c;
            uint dd = _d;

            foreach (int k in new[] { 0, 4, 8, 12 })
            {
                aa = Round1Operation(aa, bb, cc, dd, _x[k], 3);
                dd = Round1Operation(dd, aa, bb, cc, _x[k + 1], 7);
                cc = Round1Operation(cc, dd, aa, bb, _x[k + 2], 11);
                bb = Round1Operation(bb, cc, dd, aa, _x[k + 3], 19);
            }

            foreach (int k in new[] { 0, 1, 2, 3 })
            {
                aa = Round2Operation(aa, bb, cc, dd, _x[k], 3);
                dd = Round2Operation(dd, aa, bb, cc, _x[k + 4], 5);
                cc = Round2Operation(cc, dd, aa, bb, _x[k + 8], 9);
                bb = Round2Operation(bb, cc, dd, aa, _x[k + 12], 13);
            }

            foreach (int k in new[] { 0, 2, 1, 3 })
            {
                aa = Round3Operation(aa, bb, cc, dd, _x[k], 3);
                dd = Round3Operation(dd, aa, bb, cc, _x[k + 8], 9);
                cc = Round3Operation(cc, dd, aa, bb, _x[k + 4], 11);
                bb = Round3Operation(bb, cc, dd, aa, _x[k + 12], 15);
            }

            unchecked
            {
                _a += aa;
                _b += bb;
                _c += cc;
                _d += dd;
            }
        }

        private static uint ROL(uint value, int numberOfBits)
        {
            return (value << numberOfBits) | (value >> (32 - numberOfBits));
        }

        private static uint Round1Operation(uint a, uint b, uint c, uint d, uint xk, int s)
        {
            unchecked
            {
                return ROL(a + ((b & c) | (~b & d)) + xk, s);
            }
        }

        private static uint Round2Operation(uint a, uint b, uint c, uint d, uint xk, int s)
        {
            unchecked
            {
                return ROL(a + ((b & c) | (b & d) | (c & d)) + xk + 0x5a827999, s);
            }
        }

        private static uint Round3Operation(uint a, uint b, uint c, uint d, uint xk, int s)
        {
            unchecked
            {
                return ROL(a + (b ^ c ^ d) + xk + 0x6ed9eba1, s);
            }
        }
    }

}
