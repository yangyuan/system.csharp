// <copyright file="DirectoryAsync.cs" company="https://github.com/yangyuan">
//     Copyright (c) The System Async Project. All rights reserved.
// </copyright>
namespace System.IO
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Asynchronous extension for <see cref="Directory" /> class.
    /// </summary>
    public static class DirectoryAsync
    {
        /// <summary>
        /// Asynchronous extension for <see cref="Directory.Delete" /> method.
        /// </summary>
        /// <param name="path">Same with <see cref="Directory.Delete" /> path.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> that should be used to cancel the work.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute in the ThreadPool.</returns>
        public static async Task DeleteAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Run(() => Directory.Delete(path), cancellationToken);
        }

        /// <summary>
        /// Asynchronous extension for <see cref="Directory.Delete" /> method.
        /// </summary>
        /// <param name="path">Same with <see cref="Directory.Delete" /> path.</param>
        /// <param name="recursive">Same with <see cref="Directory.Delete" /> recursive.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> that should be used to cancel the work.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute in the ThreadPool.</returns>
        public static async Task DeleteAsync(string path, bool recursive, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Run(() => Directory.Delete(path, recursive), cancellationToken);
        }

        /// <summary>
        /// Asynchronous extension for <see cref="Directory.Exists" /> method.
        /// </summary>
        /// <param name="path">Same with <see cref="Directory.Exists" /> path.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> that should be used to cancel the work.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute in the ThreadPool.</returns>
        public static async Task<bool> ExistsAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.Run(() => Directory.Exists(path), cancellationToken);
        }

        /// <summary>
        /// Asynchronous extension for <see cref="Directory.CreateDirectory" /> method.
        /// </summary>
        /// <param name="path">Same with <see cref="Directory.CreateDirectory" /> path.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> that should be used to cancel the work.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute in the ThreadPool.</returns>
        public static async Task<DirectoryInfo> CreateDirectoryAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.Run(() => Directory.CreateDirectory(path), cancellationToken);
        }

        /// <summary>
        /// Asynchronous extension for <see cref="Directory.Move" /> method.
        /// </summary>
        /// <param name="sourceDirName">Same with <see cref="Directory.Move" /> sourceDirName.</param>
        /// <param name="destDirName">Same with <see cref="Directory.Move" /> destDirName.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> that should be used to cancel the work.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute in the ThreadPool.</returns>
        public static async Task MoveAsync(string sourceDirName, string destDirName, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Run(() => Directory.Move(sourceDirName, destDirName), cancellationToken);
        }
    }
}
