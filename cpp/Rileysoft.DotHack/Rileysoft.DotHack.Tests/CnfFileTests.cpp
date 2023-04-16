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

		TEST_METHOD(Test_Dummy_Works)
		{
			CnfFile* file = new CnfFile();
			file->Dummy();
		}
	};
}
