// ---------------------------------------------------------------------------
// <copyright file="DeserializationTest.cs" company="Tethys">
//   Copyright (C) 2020 T. Graf
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

namespace Fossology.Rest.Dotnet.Test
{
    using System.Collections.Generic;
    using Fossology.Rest.Dotnet.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;

    /// <summary>
    /// Unit test class.
    /// </summary>
    [TestClass]
    public class DeserializationTest
    {
        /// <summary>
        /// Class StringListClass.
        /// </summary>
        public class StringListClass
        {
            /// <summary>
            /// Gets my list.
            /// </summary>
            /// <value>My list.</value>
            public List<string> MyList { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="StringListClass"/> class.
            /// </summary>
            public StringListClass()
            {
                this.MyList = new List<string>();
            }
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestDeserializeStringList()
        {
            const string JsonText = "['A', 'B', 'C']";

            var actual = JsonConvert.DeserializeObject<List<string>>(JsonText);
            Assert.AreEqual(3, actual.Count);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestDeserializeStringList2()
        {
            const string JsonText = "{'MyList': ['A', 'B', 'C']}";

            var actual = JsonConvert.DeserializeObject<StringListClass>(JsonText);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestSerializeStringList()
        {
            var x = new StringListClass();
            x.MyList.Add("X");
            x.MyList.Add("Y");
            x.MyList.Add("Z");

            var actual = JsonConvert.SerializeObject(x);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestDeserializeFindings()
        {
            const string JsonText =
                "{" +
                  "\"scanner\":[" +
                "\"No_license_found\"" +
                "]," +
                "\"conclusion\":null," +
                "\"copyright\":null" +
                "}";

            var actual = JsonConvert.DeserializeObject<Findings>(JsonText);
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Scanner.Count);
            Assert.AreEqual("No_license_found", actual.Scanner[0]);
            Assert.AreEqual(0, actual.Copyrights.Count);
            Assert.AreEqual(0, actual.Copyrights.Count);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestDeserializeUploadLicenses()
        {
            const string JsonText =
                "[" +
                  "{" +
                    "\"filePath\":\"Tethys.xml_v1.0.0.zip/Tethys.Xml-1.0.0/Tethys.Xml.sln\"," +
                    "\"findings\":" +
                    "{" +
                      "\"scanner\":[" +
                        "\"No_license_found\"" +
                      "]," +
                      "\"conclusion\":null," +
                      "\"copyright\":null" +
                    "}" +
                  "}" +
                "]";

            var actual = JsonConvert.DeserializeObject<List<UploadLicenses>>(JsonText);
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count);
        }
    }
}
