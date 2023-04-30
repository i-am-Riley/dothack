using Rileysoft.DotHack.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rileysoft.DotHack.Tests.Extensions
{
    [TestClass]
    public class ByteArrayExtensionsTests
    {
        [DataTestMethod]
        [DataRow(new byte[] { 0x00, 0x00, 0x00, 0x00 }, 0)]
        [DataRow(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }, -1)]
        [DataRow(new byte[] { 0xFF, 0xFF, 0xFF, 0x7F }, 2147483647)]
        [DataRow(new byte[] { 0x00, 0x00, 0x00, 0x80 }, -2147483648)]
        public void ReadInt_Inputs_ReturnCorrectResults(byte[] bytes, int expected)
        {
            int result = bytes.ReadInt();

            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow(new byte[] { 0x00, 0x00 }, (short)0)]
        [DataRow(new byte[] { 0xFF, 0xFF }, (short)-1)]
        [DataRow(new byte[] { 0xFF, 0x7F }, (short)short.MaxValue)]
        [DataRow(new byte[] { 0x00, 0x80 }, (short)short.MinValue)]
        public void ReadShort_Inputs_ReturnCorrectResults(byte[] bytes, short expected)
        {
            short result = bytes.ReadShort();

            Assert.AreEqual(expected, result);
        }
    }
}
