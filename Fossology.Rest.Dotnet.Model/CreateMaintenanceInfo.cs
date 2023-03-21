// ---------------------------------------------------------------------------
// <copyright file="CreateMaintenanceInfo.cs" company="Tethys">
//   Copyright (C) 2023 T. Graf
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

namespace Fossology.Rest.Dotnet.Model
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Information to start maintenance.
    /// </summary>
    public class CreateMaintenanceInfo
    {
        /// <summary>
        /// No documentation on option 'a' available yet.
        /// </summary>
        public const string Optiona = "a";

        /// <summary>
        /// No documentation on option 'A' available yet.
        /// </summary>
        public const string OptionA = "A";

        /// <summary>
        /// No documentation on option 'F' available yet.
        /// </summary>
        public const string OptionF = "F";

        /// <summary>
        /// No documentation on option 'g' available yet.
        /// </summary>
        public const string Optiong = "g";

        /// <summary>
        /// No documentation on option 'E' available yet.
        /// </summary>
        public const string OptionE = "E";

        /// <summary>
        /// No documentation on option 'L' available yet.
        /// </summary>
        public const string OptionL = "L";

        /// <summary>
        /// No documentation on option 'N' available yet.
        /// </summary>
        public const string OptionN = "N";

        /// <summary>
        /// No documentation on option 'R' available yet.
        /// </summary>
        public const string OptionR = "R";

        /// <summary>
        /// No documentation on option 't' available yet.
        /// </summary>
        public const string Optiont = "t";

        /// <summary>
        /// No documentation on option 'T' available yet.
        /// </summary>
        public const string OptionT = "T";

        /// <summary>
        /// No documentation on option 'D' available yet.
        /// </summary>
        public const string OptionD = "D";

        /// <summary>
        /// No documentation on option 'Z' available yet.
        /// </summary>
        public const string OptionZ = "Z";

        /// <summary>
        /// No documentation on option 'I' available yet.
        /// </summary>
        public const string OptionI = "I";

        /// <summary>
        /// No documentation on option 'v' available yet.
        /// </summary>
        public const string Optionv = "v";

        /// <summary>
        /// No documentation on option 'o' available yet.
        /// </summary>
        public const string Optiono = "o";

        /// <summary>
        /// No documentation on option 'l' available yet.
        /// </summary>
        public const string Optionl = "l";

        /// <summary>
        /// Gets or sets the maintenance options.
        /// </summary>
        [JsonProperty("options")]
        public List<string> Options { get; set; }

        /// <summary>
        /// Gets or sets the date from which to remove older log files from repository.
        /// </summary>
        [JsonProperty("logsDate")]
        public string LogsDate { get; set; }

        /// <summary>
        /// Gets or sets date from which to remove older gold files from repository.
        /// </summary>
        [JsonProperty("goldDate")]
        public string GoldDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMaintenanceInfo"/> class.
        /// </summary>
        public CreateMaintenanceInfo()
        {
            this.Options = new List<string>();
        } // CreateMaintenance()

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Options}: {this.LogsDate}, {this.GoldDate}";
        } // ToString()
    } // CreateMaintenance
}
