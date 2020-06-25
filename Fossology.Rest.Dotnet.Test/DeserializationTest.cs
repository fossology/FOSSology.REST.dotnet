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
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;
    using Newtonsoft.Json;

    /// <summary>
    /// Unit test class.
    /// </summary>
    [TestClass]
    public class DeserializationTest
    {
        public class StringListClass
        {
            public List<string> MyList { get; }

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
        public void TestDeserializeComplex1()
        {
            const string JsonText = 
                "{"
                + "\"filePath\":\"fetch-retry-master.zip/fetch-retry-master/package.json\","
                + "\"agentFindings\":[\"MIT\"],"
                + "\"conclusions\":null"
                + "}";

            var actual = JsonConvert.DeserializeObject<UploadLicenses>(JsonText);
            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.AgentFindings);
            Assert.AreEqual(1, actual.AgentFindings.Count);

            // ==> no correct deserialization without constructor...
        }
    }
}
