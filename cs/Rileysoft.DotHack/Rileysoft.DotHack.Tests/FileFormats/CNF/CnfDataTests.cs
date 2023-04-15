using Rileysoft.DotHack.FileFormats.CNF;

#pragma warning disable IDE0017 // initialize can be simplified

namespace Rileysoft.DotHack.Tests.FileFormats.CNF
{
    [TestClass]
    public class CnfDataTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BOOT2Set_WhenReadonly_Throws ()
        {
            CnfData cnfData = new();
            cnfData.MakeReadonly();

            cnfData.BOOT2 = "";
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void VERSet_WhenReadonly_Throws()
        {
            CnfData cnfData = new();
            cnfData.MakeReadonly();

            cnfData.VER = "";
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void VMODESet_WhenReadonly_Throws()
        {
            CnfData cnfData = new();
            cnfData.MakeReadonly();

            cnfData.VMODE = "";
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PARAM2Set_WhenReadonly_Throws()
        {
            CnfData cnfData = new();
            cnfData.MakeReadonly();

            cnfData.PARAM2 = "";
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PARAM4Set_WhenReadonly_Throws()
        {
            CnfData cnfData = new();
            cnfData.MakeReadonly();

            cnfData.PARAM4 = "";
        }

        [TestMethod]
        public void BOOT2Set_WhenNotReadonly_Works ()
        {
            CnfData cnfData = new();
            cnfData.BOOT2 = "";
        }

        [TestMethod]
        public void VERSet_WhenNotReadonly_Works()
        {
            CnfData cnfData = new();
            cnfData.VER = "";
        }

        [TestMethod]
        public void VMODESet_WhenNotReadonly_Works()
        {
            CnfData cnfData = new();
            cnfData.VMODE = "";
        }

        [TestMethod]
        public void PARAM2Set_WhenNotReadonly_Works()
        {
            CnfData cnfData = new();
            cnfData.PARAM2 = "";
        }

        [TestMethod]
        public void PARAM4Set_WhenNotReadonly_Works()
        {
            CnfData cnfData = new();
            cnfData.PARAM4 = "";
        }
    }
}

#pragma warning restore IDE0017
