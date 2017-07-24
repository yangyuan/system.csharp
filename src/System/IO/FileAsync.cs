// <copyright file="FileAsync.cs" company="https://github.com/yangyuan">
//     Copyright (c) The System Async Project. All rights reserved.
// </copyright>
namespace System.IO
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Asynchronous extension for <see cref="File" /> class.
    /// </summary>
    public static class FileAsync
    {
        /// <summary>
        /// Asynchronous extension for <see cref="File.Delete" /> method.
        /// </summary>
        /// <param name="path">Same with <see cref="File.Delete" /> path.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> that should be used to cancel the work.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute in the ThreadPool.</returns>
        public static async Task DeleteAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Run(() => File.Delete(path), cancellationToken);
        }

        /// <summary>
        /// Asynchronous extension for <see cref="File.Exists" /> method.
        /// </summary>
        /// <param name="path">Same with <see cref="File.Exists" /> path.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> that should be used to cancel the work.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute in the ThreadPool.</returns>
        public static async Task<bool> ExistsAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.Run(() => File.Exists(path), cancellationToken);
        }

        /// <summary>
        /// Asynchronous extension for <see cref="File.Move" /> method.
        /// </summary>
        /// <param name="sourceFileName">Same with <see cref="File.Move" /> sourceFileName.</param>
        /// <param name="destFileName">Same with <see cref="File.Move" /> destFileName.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> that should be used to cancel the work.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute in the ThreadPool.</returns>
        public static async Task MoveAsync(string sourceFileName, string destFileName, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Run(() => File.Move(sourceFileName, destFileName), cancellationToken);
        }

        /// <summary>
        /// Asynchronous extension for <see cref="File.WriteAllText" /> method.
        /// </summary>
        /// <param name="path">Same with <see cref="File.WriteAllText" /> path.</param>
        /// <param name="contents">Same with <see cref="File.WriteAllText" /> contents.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> that should be used to cancel the work.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute in the ThreadPool.</returns>
        public static async Task WriteAllTextAsync(string path, string contents, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Run(() => File.WriteAllText(path, contents), cancellationToken);
        }

        /// <summary>
        /// Asynchronous extension for <see cref="File.WriteAllText" /> method.
        /// </summary>
        /// <param name="path">Same with <see cref="File.WriteAllText" /> path.</param>
        /// <param name="contents">Same with <see cref="File.WriteAllText" /> contents.</param>
        /// <param name="encoding">Same with <see cref="File.WriteAllText" /> encoding.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> that should be used to cancel the work.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute in the ThreadPool.</returns>
        public static async Task WriteAllTextAsync(string path, string contents, Text.Encoding encoding, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Run(() => File.WriteAllText(path, contents, encoding), cancellationToken);
        }

        /// <summary>
        /// Asynchronous extension for <see cref="File.ReadAllText" /> method.
        /// </summary>
        /// <param name="path">Same with <see cref="File.ReadAllText" /> path.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> that should be used to cancel the work.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute in the ThreadPool.</returns>
        public static async Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.Run(() => File.ReadAllText(path), cancellationToken);
        }

        /// <summary>
        /// Asynchronous extension for <see cref="File.ReadAllText" /> method.
        /// </summary>
        /// <param name="path">Same with <see cref="File.ReadAllText" /> path.</param>
        /// <param name="encoding">Same with <see cref="File.ReadAllText" /> encoding.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> that should be used to cancel the work.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute in the ThreadPool.</returns>
        public static async Task<string> ReadAllTextAsync(string path, Text.Encoding encoding, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.Run(() => File.ReadAllText(path, encoding), cancellationToken);
        }
    }
}