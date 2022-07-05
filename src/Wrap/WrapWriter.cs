using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wrap.Types;

namespace Wrap
{
    public class WrapWriter
    {

        /*
        public void Write(WrapHeader header, WrapMetadata metadata, Stream archiveStream, bool overwrite = false)
        {
            // Verify that it is a valid EXE file.
            byte[] exeSignatureBytes = new byte[2];

            Stream.Position = 0;
            Stream.Read(exeSignatureBytes, 0, 2);

            if (exeSignatureBytes[0] == 0x4D && exeSignatureBytes[1] == 0x5A)
            {
                throw new InvalidDataException("The file is not a valid executable file.");
            }

            // Check if a WRAP header exists.
            bool headerAlreadyExistsFlag = false;

            byte[] wrapSignatureBytes = new byte[4];
            Stream.Position = Stream.Length - 24;
            Stream.Read(wrapSignatureBytes, 0, 4);

            uint wrapSignature = BitConverter.ToUInt32(wrapSignatureBytes, 0);

            if (wrapSignature == WrapConstants.WrapSignature)
            {
                headerAlreadyExistsFlag = true;
            }

            // Write a header.
            if (headerAlreadyExistsFlag)
            {
                if (overwrite)
                {
                    byte[] compressedSizeBytes = new byte[8];
                    Stream.Position = Stream.Length - 24;
                    Stream.Read(compressedSizeBytes, 12, 8);
                    ulong compressedSize = BitConverter.ToUInt64(compressedSizeBytes, 0);

                    byte[] metadataSizeBytes = new byte[4];
                    Stream.Position = Stream.Length - 24;
                    Stream.Read(metadataSizeBytes, 20, 4);
                    uint metadataSize = BitConverter.ToUInt32(metadataSizeBytes, 0);

                    ulong wrapDataSize = 24 + compressedSize + metadataSize;
                    Stream.SetLength(Stream.Length - (long)wrapDataSize);
                }
                else
                {
                    throw new InvalidOperationException("The header already exists.");
                }
            }
            else
            {
                using (MemoryStream metadataStream = new MemoryStream())
                {
                    JsonSerializer serializer = new JsonSerializer();
                    BsonWriter bsonWriter = new BsonWriter(metadataStream);
                    serializer.Serialize(bsonWriter, metadata);
                    metadataStream.Seek(0, SeekOrigin.Begin);
                }

                
            }
        }
        */


        public async Task WriteAsync(WrapHeader header, WrapMetadata metadata, bool overwrite = false)
        {

        }
    }
}
