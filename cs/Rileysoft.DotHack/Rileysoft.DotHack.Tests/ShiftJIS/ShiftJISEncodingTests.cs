using Rileysoft.DotHack.ShiftJIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Tests.ShiftJIS
{
    [TestClass]
    public class ShiftJISEncodingTests
    {
        [DataTestMethod]
        [DataRow(new byte[] { 0x7A }, "z")]
        [DataRow(new byte[] { 0xCE }, "\uFF8E" )]
        [DataRow(new byte[] { 0x81, 0x91 }, "\u00A2" )]
        [DataRow(new byte[] { 0x8A, 0x65 }, "各" )]
        public void GetString_Inputs_ReturnCorrectResults(byte[] b, string expected)
        {
            string result = ShiftJISEncoding.GetString(b);
            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(new byte[] { 0x7A }, "z")]
        [DataRow(new byte[] { 0xCE }, "\uFF8E")]
        [DataRow(new byte[] { 0x81, 0x91 }, "\u00A2")]
        [DataRow(new byte[] { 0x8A, 0x65 }, "各")]
        public void GetBytes_Inputs_ReturnCorrectResults(byte[] expected, string input)
        {
            byte[] bytes = ShiftJISEncoding.GetBytes(input);
            Assert.AreEqual (expected.Length, bytes.Length);
            for (int i=0; i<bytes.Length; i++)
            {
                Assert.AreEqual(expected[i], bytes[i]);
            }
        }
    }
}
