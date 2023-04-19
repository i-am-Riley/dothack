#include "pch.h"
#include "CppUnitTest.h"
#include "CnfFile.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;
using namespace Rileysoft::DotHack::FileFormats::CNF;

namespace RileysoftDotHackTests
{
	TEST_CLASS(CnfFileTests)
	{
	public:
		TEST_METHOD(SetData_WhenReadonly_Throws)
		{
			Assert::ExpectException<std::logic_error>([] {
				CnfFile* cnfFile = new CnfFile();
				cnfFile->MakeReadonly();

				cnfFile->GetData()->SetBOOT2("test");
			});
		}

		TEST_METHOD(SetData_WhenNotReadonly_Works)
		{
			CnfFile* cnfFile = new CnfFile();
			cnfFile->GetData()->SetBOOT2("test");
		}
	};
}
