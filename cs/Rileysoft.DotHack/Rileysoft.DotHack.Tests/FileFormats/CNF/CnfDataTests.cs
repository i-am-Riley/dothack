using Rileysoft.DotHack.FileFormats.CNF;

#pragma warning disable IDE0017 // initialize can be simplified

namespace Rileysoft.DotHack.Tests.FileFormats.CNF
{
    [TestClass]
    public class CnfDataTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SetBOOT2_WhenReadonly_Throws()
        {
            CnfData cnfData = new();
            cnfData.MakeReadonly();

            cnfData.BOOT2 = "";
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SetVER_WhenReadonly_Throws()
        {
            CnfData cnfData = new();
            cnfData.MakeReadonly();

            cnfData.VER = "";
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SetVMODE_WhenReadonly_Throws()
        {
            CnfData cnfData = new();
            cnfData.MakeReadonly();

            cnfData.VMODE = "";
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SetPARAM2_WhenReadonly_Throws()
        {
            CnfData cnfData = new();
            cnfData.MakeReadonly();

            cnfData.PARAM2 = "";
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SetPARAM4_WhenReadonly_Throws()
        {
            CnfData cnfData = new();
            cnfData.MakeReadonly();

            cnfData.PARAM4 = "";
        }

        [TestMethod]
        public void SetBOOT2_WhenNotReadonly_Works()
        {
            CnfData cnfData = new();
            cnfData.BOOT2 = "";
        }

        [TestMethod]
        public void SetVER_WhenNotReadonly_Works()
        {
            CnfData cnfData = new();
            cnfData.VER = "";
        }

        [TestMethod]
        public void SetVMODE_WhenNotReadonly_Works()
        {
            CnfData cnfData = new();
            cnfData.VMODE = "";
        }

        [TestMethod]
        public void SetPARAM2_WhenNotReadonly_Works()
        {
            CnfData cnfData = new();
            cnfData.PARAM2 = "";
        }

        [TestMethod]
        public void SetPARAM4_WhenNotReadonly_Works()
        {
            CnfData cnfData = new();
            cnfData.PARAM4 = "";
        }
    }
}

#pragma warning restore IDE0017
