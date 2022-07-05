using SevenZip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Wrap
{
    public class WrapArchiveManager
    {
        // For extractors
        public event EventHandler<FileInfoEventArgs>? FileExtractionStartedEvent;

        public event EventHandler<FileInfoEventArgs>? FileExtractionFinishedEvent;

        public event EventHandler<FileOverwriteEventArgs>? FileAlreadyExistsEvent;

        public event EventHandler<ProgressEventArgs>? ExtractingEvent;

        public event EventHandler<EventArgs>? ExtractionFinishedEvent;

        // For compressors
        public event EventHandler<FileNameEventArgs>? FileCompressionStartedEvent;

        public event EventHandler<EventArgs>? FileCompressionFinishedEvent;

        public event EventHandler<IntEventArgs>? FilesFoundEvent;

        public event EventHandler<ProgressEventArgs>? CompressingEvent;

        public event EventHandler<EventArgs>? CompressionFinishedEvent;

        public WrapArchiveManager()
        {
            EnsureSevenZipLibrary();
        }

        private static void EnsureSevenZipLibrary()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "7z.dll");

            if (!File.Exists(filePath))
            {
                Assembly currentAssembly = Assembly.GetExecutingAssembly();

                if (Environment.Is64BitOperatingSystem)
                {
                    using (FileStream stream = new FileStream(filePath, FileMode.CreateNew))
                    {
                        currentAssembly.GetManifestResourceStream("Wrap.Assets.x64.7z.dll")?.CopyTo(stream);
                    }
                }
                else
                {
                    using (FileStream stream = new FileStream(filePath, FileMode.CreateNew))
                    {
                        currentAssembly.GetManifestResourceStream("Wrap.Assets.x86.7z.dll")?.CopyTo(stream);
                    }
                }
            }

            SevenZipBase.SetLibraryPath(filePath);
        }

        public InArchiveFormat GetArchiveType(string filePath)
        {
            using (SevenZipExtractor extractor = new SevenZipExtractor(filePath))
            {
                return extractor.Format;
            }
        }

        public List<string> GetFiles(string filePath, string? password = null, List<string>? targetExtensions = null)
        {
            try
            {
                using (SevenZipExtractor extractor = string.IsNullOrEmpty(password) ? new SevenZipExtractor(filePath) : new SevenZipExtractor(filePath, password))
                {
                    IEnumerable<string> result = extractor.ArchiveFileNames;

                    if (targetExtensions == null || targetExtensions.Count == 0)
                    {
                        return result.ToList();
                    }
                    else
                    {
                        result = result.Where(p => targetExtensions.Contains(Path.GetExtension(p), StringComparer.OrdinalIgnoreCase));
                        return result.ToList();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<string>> GetFilesAsync(string filePath, string? password = null, List<string>? targetExtensions = null)
        {
            var task = Task.Factory.StartNew(() =>
            {
                return GetFiles(filePath, password, targetExtensions);
            });

            return await task.ConfigureAwait(false);
        }

        public bool IsEncrypted(string filePath)
        {
            try
            {
                using (SevenZipExtractor extractor = new SevenZipExtractor(filePath))
                {
                    string? fileName = extractor.ArchiveFileNames.FirstOrDefault();

                    if (string.IsNullOrEmpty(fileName))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SevenZipArchiveException)
            {
                return true;
            }
        }

        public async Task<bool> IsEncryptedAsync(string filePath)
        {
            var task = Task.Factory.StartNew(() =>
            {
                return IsEncrypted(filePath);
            });

            return await task.ConfigureAwait(false);
        }

        public bool IsValidPassword(string filePath, string password)
        {
            try
            {
                using (SevenZipExtractor extractor = new SevenZipExtractor(filePath, password))
                {
                    string? fileName = extractor.ArchiveFileNames.FirstOrDefault();

                    if (string.IsNullOrEmpty(fileName))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (SevenZipArchiveException)
            {
                return false;
            }
        }

        public async Task<bool> IsValidPasswordAsync(string filePath, string password)
        {
            var task = Task.Factory.StartNew(() =>
            {
                return IsValidPassword(filePath, password);
            });

            return await task.ConfigureAwait(false);
        }

        public void Decompress(string targetDirectory, string filePath, string? password = null)
        {
            try
            {
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                using (SevenZipExtractor extractor = string.IsNullOrEmpty(password) ? new SevenZipExtractor(filePath) : new SevenZipExtractor(filePath, password))
                {
                    extractor.PreserveDirectoryStructure = true;

                    extractor.EventSynchronization = EventSynchronizationStrategy.AlwaysSynchronous;
                    extractor.FileExtractionStarted += FileExtractionStartedEvent;
                    extractor.FileExtractionFinished += FileExtractionFinishedEvent;
                    extractor.FileExists += FileAlreadyExistsEvent;
                    extractor.Extracting += ExtractingEvent;
                    extractor.ExtractionFinished += ExtractionFinishedEvent;

                    extractor.ExtractArchive(targetDirectory);
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task DecompressAsync(string targetDirectory, string filePath, string? password = null)
        {
            try
            {
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                using (SevenZipExtractor extractor = string.IsNullOrEmpty(password) ? new SevenZipExtractor(filePath) : new SevenZipExtractor(filePath, password))
                {
                    extractor.PreserveDirectoryStructure = true;

                    extractor.EventSynchronization = EventSynchronizationStrategy.AlwaysAsynchronous;
                    extractor.FileExtractionStarted += FileExtractionStartedEvent;
                    extractor.FileExtractionFinished += FileExtractionFinishedEvent;
                    extractor.FileExists += FileAlreadyExistsEvent;
                    extractor.Extracting += ExtractingEvent;
                    extractor.ExtractionFinished += ExtractionFinishedEvent;

                    await extractor.ExtractArchiveAsync(targetDirectory);
                }
            }
            catch
            {
                throw;
            }
        }

        public void CompressDirectory(string directory, string savePath, string? password = null)
        {
            try
            {
                SevenZipCompressor compressor = new SevenZipCompressor();
                compressor.DirectoryStructure = true;

                compressor.EventSynchronization = EventSynchronizationStrategy.AlwaysSynchronous;
                compressor.FileCompressionStarted += FileCompressionStartedEvent;
                compressor.FileCompressionFinished += FileCompressionFinishedEvent;
                compressor.FilesFound += FilesFoundEvent;
                compressor.Compressing += CompressingEvent;
                compressor.CompressionFinished += CompressionFinishedEvent;

                compressor.CompressionLevel = CompressionLevel.Normal;
                compressor.CompressionMethod = CompressionMethod.Lzma2;
                compressor.CompressionMode = CompressionMode.Create;

                compressor.CompressDirectory(directory, savePath, string.IsNullOrEmpty(password) ? "" : password);
            }
            catch
            {
                throw;
            }
        }

        public async Task CompressDirectoryAsync(string directory, string savePath, string? password = null)
        {
            try
            {
                SevenZipCompressor compressor = new SevenZipCompressor();
                compressor.DirectoryStructure = true;

                compressor.EventSynchronization = EventSynchronizationStrategy.AlwaysAsynchronous;
                compressor.FileCompressionStarted += FileCompressionStartedEvent;
                compressor.FileCompressionFinished += FileCompressionFinishedEvent;
                compressor.FilesFound += FilesFoundEvent;
                compressor.Compressing += CompressingEvent;
                compressor.CompressionFinished += CompressionFinishedEvent;

                compressor.CompressionLevel = CompressionLevel.Normal;
                compressor.CompressionMethod = CompressionMethod.Lzma2;
                compressor.CompressionMode = CompressionMode.Create;

                await compressor.CompressDirectoryAsync(directory, savePath, string.IsNullOrEmpty(password) ? "" : password);
            }
            catch
            {
                throw;
            }
        }

        public void CompressFiles(List<string> files, string savePath, string? password = null)
        {
            try
            {
                SevenZipCompressor compressor = new SevenZipCompressor();
                compressor.DirectoryStructure = true;

                compressor.EventSynchronization = EventSynchronizationStrategy.AlwaysSynchronous;
                compressor.FileCompressionStarted += FileCompressionStartedEvent;
                compressor.FileCompressionFinished += FileCompressionFinishedEvent;
                compressor.FilesFound += FilesFoundEvent;
                compressor.Compressing += CompressingEvent;
                compressor.CompressionFinished += CompressionFinishedEvent;

                compressor.CompressionLevel = CompressionLevel.Normal;
                compressor.CompressionMethod = CompressionMethod.Lzma2;
                compressor.CompressionMode = CompressionMode.Create;

                if (string.IsNullOrEmpty(password))
                {
                    compressor.CompressFiles(savePath, files.ToArray());
                }
                else
                {
                    compressor.CompressFilesEncrypted(savePath, password, files.ToArray());
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task CompressFilesAsync(List<string> files, string savePath, string? password = null)
        {
            try
            {
                SevenZipCompressor compressor = new SevenZipCompressor();
                compressor.DirectoryStructure = true;

                compressor.EventSynchronization = EventSynchronizationStrategy.AlwaysAsynchronous;
                compressor.FileCompressionStarted += FileCompressionStartedEvent;
                compressor.FileCompressionFinished += FileCompressionFinishedEvent;
                compressor.FilesFound += FilesFoundEvent;
                compressor.Compressing += CompressingEvent;
                compressor.CompressionFinished += CompressionFinishedEvent;

                compressor.CompressionLevel = CompressionLevel.Normal;
                compressor.CompressionMethod = CompressionMethod.Lzma2;
                compressor.CompressionMode = CompressionMode.Create;

                if (string.IsNullOrEmpty(password))
                {
                    await compressor.CompressFilesAsync(savePath, files.ToArray());
                }
                else
                {
                    await compressor.CompressFilesEncryptedAsync(savePath, password, files.ToArray());
                }
            }
            catch
            {
                throw;
            }
        }

        private void CategorizeItems(List<string> items, out List<string> files, out List<string> directories)
        {
            files = new List<string>();
            directories = new List<string>();

            foreach(string item in items)
            {
                FileAttributes attr = File.GetAttributes(item);
                
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    directories.Add(item);
                }
                else
                {
                    files.Add(item);
                }
            }
        }

        public void Compress(List<string> items, string savePath, string? password = null)
        {
            try
            {
                // Initialize a compressor.
                SevenZipCompressor compressor = new SevenZipCompressor();
                compressor.EventSynchronization = EventSynchronizationStrategy.AlwaysSynchronous;
                compressor.FileCompressionStarted += FileCompressionStartedEvent;
                compressor.FileCompressionFinished += FileCompressionFinishedEvent;
                compressor.FilesFound += FilesFoundEvent;
                compressor.Compressing += CompressingEvent;
                compressor.CompressionFinished += CompressionFinishedEvent;

                compressor.CompressionLevel = CompressionLevel.Normal;
                compressor.CompressionMethod = CompressionMethod.Lzma2;

                // Categorize items.
                List<string>? files = null;
                List<string>? directories = null;
                CategorizeItems(items, out files, out directories);

                // Compress files.
                bool isNeedToCreate = true;

                compressor.DirectoryStructure = false;
                compressor.CompressionMode = CompressionMode.Create;

                if (string.IsNullOrEmpty(password))
                {
                    compressor.CompressFiles(savePath, files.ToArray());
                }
                else
                {
                    compressor.CompressFilesEncrypted(savePath, password, files.ToArray());
                }

                if (files.Count != 0)
                {
                    isNeedToCreate = false;
                }

                // Compress directories.
                compressor.DirectoryStructure = true;

                foreach (string directory in directories)
                {
                    if (!isNeedToCreate)
                    {
                        compressor.CompressionMode = CompressionMode.Append;
                    }

                    compressor.CompressDirectory(directory, savePath, string.IsNullOrEmpty(password) ? "" : password);
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task CompressAsync(List<string> items, string savePath, string? password = null)
        {
            try
            {
                // Initialize a compressor.
                SevenZipCompressor compressor = new SevenZipCompressor();
                compressor.EventSynchronization = EventSynchronizationStrategy.AlwaysAsynchronous;
                compressor.FileCompressionStarted += FileCompressionStartedEvent;
                compressor.FileCompressionFinished += FileCompressionFinishedEvent;
                compressor.FilesFound += FilesFoundEvent;
                compressor.Compressing += CompressingEvent;
                compressor.CompressionFinished += CompressionFinishedEvent;

                compressor.CompressionLevel = CompressionLevel.Normal;
                compressor.CompressionMethod = CompressionMethod.Lzma2;

                // Categorize items.
                List<string>? files = null;
                List<string>? directories = null;
                CategorizeItems(items, out files, out directories);

                // Compress files.
                bool isNeedToCreate = true;

                compressor.DirectoryStructure = false;
                compressor.CompressionMode = CompressionMode.Create;

                if (string.IsNullOrEmpty(password))
                {
                    await compressor.CompressFilesAsync(savePath, files.ToArray());
                }
                else
                {
                    await compressor.CompressFilesEncryptedAsync(savePath, password, files.ToArray());
                }

                if (files.Count != 0)
                {
                    isNeedToCreate = false;
                }

                // Compress directories.
                compressor.DirectoryStructure = true;

                foreach (string directory in directories)
                {
                    if (!isNeedToCreate)
                    {
                        compressor.CompressionMode = CompressionMode.Append;
                    }

                    await compressor.CompressDirectoryAsync(directory, savePath, string.IsNullOrEmpty(password) ? "" : password);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
