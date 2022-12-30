// ---------------------------------------------------------------------------
// <copyright file="FileStreamWithProgress.cs" company="Tethys">
//   Copyright (C) 2019-2022 T. Graf
// </copyright>
//
// Licensed under the MIT License.
// SPDX-License-Identifier: MIT
//
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied.
// ---------------------------------------------------------------------------

namespace Fossology.Rest.Dotnet
{
    using System;
    using System.IO;
    using Tethys.Logging;

    /// <summary>
    /// Implements a file stream that can report the read progress.
    /// </summary>
    /// <seealso cref="System.IO.Stream" />
    internal class FileStreamWithProgress : Stream
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The logger for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(FileStreamWithProgress));

        /// <summary>
        /// The file stream.
        /// </summary>
        private readonly FileStream fileStream;

        /// <summary>
        /// The upload finished callback.
        /// </summary>
        private readonly Action uploadFinished;

        /// <summary>
        /// The upload progress callback.
        /// </summary>
        private readonly Action<float> uploadProgress;

        /// <summary>
        /// The current position.
        /// </summary>
        private long sofar;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <inheritdoc />
        public override bool CanRead
        {
            get { return this.fileStream != null && this.fileStream.CanRead; }
        }

        /// <inheritdoc />
        public override bool CanSeek
        {
            get { return this.fileStream != null && this.fileStream.CanSeek; }
        }

        /// <inheritdoc />
        public override bool CanWrite
        {
            get { return this.fileStream != null && this.fileStream.CanWrite; }
        }

        /// <inheritdoc />
        public override long Length
        {
            get
            {
                return this.fileStream?.Length ?? 0;
            }
        }

        /// <inheritdoc />
        public override long Position
        {
            get { return this.fileStream?.Position ?? 0; }
            set { }
        }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="FileStreamWithProgress"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="uploadFinished">The optional upload finished callback.</param>
        /// <param name="uploadProgress">The optional upload progress callback.</param>
        public FileStreamWithProgress(
            string fileName,
            Action uploadFinished = null,
            Action<float> uploadProgress = null)
        {
            this.fileStream = new FileStream(fileName, FileMode.Open);
            this.uploadFinished = uploadFinished;
            this.uploadProgress = uploadProgress;
            this.sofar = 0;
        } // FileStreamWithProgress()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <inheritdoc />
        public override void Flush()
        {
            this.fileStream?.Flush();
        }

        /// <inheritdoc />
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (this.fileStream == null)
            {
                return 0;
            } // if

            var result = this.fileStream.Read(buffer, offset, count);

            this.sofar = this.fileStream.Position;

            var percentage = this.sofar * 100 / this.fileStream.Length;
            var progress = percentage / 100f;

            Log.Debug($"Upload progress = {progress}");
            this.uploadProgress?.Invoke(progress);

            if (this.sofar >= this.fileStream.Length)
            {
                this.uploadFinished?.Invoke();
            } // if

            return result;
        }

        /// <inheritdoc />
        public override long Seek(long offset, SeekOrigin origin)
        {
            if (this.fileStream == null)
            {
                return 0;
            } // if

            var result = this.fileStream.Seek(offset, origin);
            this.sofar = result;
            return result;
        }

        /// <inheritdoc />
        public override void SetLength(long value)
        {
            this.fileStream?.SetLength(value);
        }

        /// <inheritdoc />
        public override void Write(byte[] buffer, int offset, int count)
        {
            this.fileStream?.Write(buffer, offset, count);
        }
        #endregion // PUBLIC METHODS
    } // FileStreamWithProgress
}
